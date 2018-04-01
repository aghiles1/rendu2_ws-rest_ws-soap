using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VelibGateway
{
    public class ServiceVelib : IServiceVelib
    {
        //la clé pour l'utilisation de l'API
        private static readonly string API_KEY = "3262b38e08b9a4da636a6611bf24c9de0fb93069";
        private static Dictionary<string, Dictionary<string, Tuple<int,int>>> cache = new Dictionary<string, Dictionary<string, Tuple<int,int>>>();
        public const int DelayMilliseconds = 10000;
        /**
         * toutes les stations de l'API
         **/

        public IList<string> getStations(string town)
        {
            if (cache.ContainsKey(town) == false)
            {
                return null;
            }
            if (cache[town].Count == 0)
            {
                return getStationsService(town);
            }
            else
            {
                if (cache[town] == null)
                    return null;
                return cache[town].Keys.ToList();
            }
        }
        /**
         *Toutes les villes dans l'API despose
         * 
         **/
        public IList<string> getTowns()
        {
            if (cache.Count == 0)
            {
                return getTownsService();
            }
            return cache.Keys.ToList();
        }
        /**
         *Les vélo déspo dans une station donnée qui se trouve dans une ville donnée
         **/
        public int getAvailableBikes(string town, string station, int time)
        {
            string sta = null;
            int veloLibre = -1;
            if (town == null || cache.ContainsKey(town) == false)
            {
                return -1;
            }
            if (station != null) {
                sta = cache[town].Keys.FirstOrDefault(k => k.Contains(station));
                if (sta != null)
                {
                    veloLibre = cache[town][sta].Item1;
                    if (DateTime.Now.Second - cache[town][sta].Item2 <= time && veloLibre != -1)
                    {
                        return veloLibre;
                    }
                }
                else { sta = station; }
            } else return -1;
            veloLibre = getAvailableBikesService(town, sta);
            if (veloLibre != -1)
            {
                sta = cache[town].Keys.FirstOrDefault(k => k.Contains(station));
                cache[town][sta] = Tuple.Create(veloLibre, DateTime.Now.Second);
            }
            return veloLibre;
        }

        private int getAvailableBikesService(string town, string station)
        {
        
                WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=" + API_KEY);
                WebResponse reponse = requete.GetResponse();
                Stream stream = reponse.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string restr = reader.ReadToEnd();
                if (reponse == null || station == null)
                {
                    return -1;
                }
                reponse.Close();
                reader.Close();

                JArray jsonArray = JArray.Parse(restr);
                int size = jsonArray.Count;
                foreach (JObject item in jsonArray)
                {
                    string name = ((string)item["name"]).ToLower();
             
                    if (item["name"] == null)
                        return -1;
                    if (!cache[town].ContainsKey(name))
                    {
                        cache[town].Add(name, Tuple.Create(-1, -1));
                    }
                    if (((string)item["name"]).ToLower().Contains(station.ToLower()))
                    {
                        int veloLibre = Convert.ToInt32(item["available_bikes"]);
                        cache[town][name] = Tuple.Create(veloLibre, cache[town][name].Item2);
                        return veloLibre;
                    }
                }
         
            return -1;
        }

        private IList<string> getTownsService()
        {
            try
            {
                IList<string> towns = new List<string>();

                WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY);


                WebResponse reponse = requete.GetResponse();
                if (reponse == null)
                {
                    return null;
                }
                Stream stream = reponse.GetResponseStream();


                StreamReader reader = new StreamReader(stream);
                string restr = reader.ReadToEnd();

                reponse.Close();
                reader.Close();

                JArray jsonArray = JArray.Parse(restr);

                foreach (JObject item in jsonArray)
                {

                    cache.Add(item["name"].ToString().ToLower(), new Dictionary<string, Tuple<int, int>>());
                    towns.Add(item["name"].ToString().ToLower());
                }
                return towns;
            }
            catch(Exception e)
            {
                return null;
            }

           
        }

        private IList<string> getStationsService(string town)
        {
            try
            {
                IList<string> stationsOfTown = new List<string>();

                WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=" + API_KEY);

                WebResponse reponse = requete.GetResponse();
                if (reponse == null)
                {
                    return null;
                }
                Stream stream = reponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string restr = reader.ReadToEnd();

                reponse.Close();
                reader.Close();

                JArray jsonArray = JArray.Parse(restr);

                foreach (JObject item in jsonArray)
                {
                    cache[town].Add(item["name"].ToString().ToLower(), Tuple.Create(-1, -1));
                    stationsOfTown.Add(item["name"].ToString().ToLower());
                }
                return stationsOfTown;
            }
            catch (Exception e)
            {
                return null;
            }
         
        }

        public IAsyncResult BegingetStations(string City, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<IList<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getStations(City);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public IList<string> EndgetStations(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<IList<string>>)asyncResult).Result;
        }

        public IAsyncResult BegingetAvailableBikes(string town, string station, int time, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<int>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getAvailableBikes(town, station,time);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public int EndgetAvailableBikes(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<int>)asyncResult).Result;
        }

        public IAsyncResult BegingetTowns(AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<IList<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getTowns();
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public IList<string> EndgetTowns(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<IList<string>>)asyncResult).Result;
        }
    }
}
