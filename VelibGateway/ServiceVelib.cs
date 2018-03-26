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
        private Dictionary<string, Dictionary<string, Tuple<int,int>>> cache = new Dictionary<string, Dictionary<string, Tuple<int,int>>>();
        public const int DelayMilliseconds = 10000;
        /**
         * toutes les stations de l'API
         **/

        public IList<string> getStations(string town)
        {

            if (cache[town].Count == 0)
            {
                return getStationsService(town);
            }
            else
            {
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
            int veloLibre = cache[town][station].Item1;
        
            if (DateTime.Now.Second - cache[town][station].Item2 <= time  && veloLibre != -1)
            {
                return veloLibre;
            }
            veloLibre = getAvailableBikesService(town, station);
            cache[town][station] = Tuple.Create(veloLibre, DateTime.Now.Second);
            return veloLibre;
        }

        private int getAvailableBikesService(string town, string station)
        {
            WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=" + API_KEY);

            WebResponse reponse = requete.GetResponse();
            Stream stream = reponse.GetResponseStream();


            StreamReader reader = new StreamReader(stream);
            string restr = reader.ReadToEnd();

            reponse.Close();
            reader.Close();

            JArray jsonArray = JArray.Parse(restr);
            int size = jsonArray.Count;

            foreach (JObject item in jsonArray)
            {
                if (((string)item["name"]).ToLower().Contains(station.ToLower()))
                {
                    int veloLibre = Convert.ToInt32(item["available_bikes"]);
                    cache[town][station] = Tuple.Create(veloLibre, cache[town][station].Item2);
                    return veloLibre;
                }
            }

            return -1;
        }

        private IList<string> getTownsService()
        {
            IList<string> towns = new List<string>();

            WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY);


            WebResponse reponse = requete.GetResponse();
            Stream stream = reponse.GetResponseStream();


            StreamReader reader = new StreamReader(stream);
            string restr = reader.ReadToEnd();

            reponse.Close();
            reader.Close();

            JArray jsonArray = JArray.Parse(restr);

            foreach (JObject item in jsonArray)
            {
                cache.Add(item["name"].ToString() , new Dictionary<string,Tuple<int,int>>());
                towns.Add(item["name"].ToString());
            }

            return towns;
        }

        private IList<string> getStationsService(string town)
        {
            IList<string> stationsOfTown = new List<string>();

            WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=" + API_KEY);

            WebResponse reponse = requete.GetResponse();
            Stream stream = reponse.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string restr = reader.ReadToEnd();

            reponse.Close();
            reader.Close();

            JArray jsonArray = JArray.Parse(restr);

            foreach (JObject item in jsonArray)
            {
                cache[town].Add(item["name"].ToString(), Tuple.Create(-1,-1));
                stationsOfTown.Add(item["name"].ToString());
            }

            return stationsOfTown;
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
