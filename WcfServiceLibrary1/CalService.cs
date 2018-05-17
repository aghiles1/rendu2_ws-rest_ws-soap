﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json.Linq;

namespace WcfServiceLibrary
{
    public class CalService : ICalService
    {
        private static readonly string API_KEY = "3262b38e08b9a4da636a6611bf24c9de0fb93069";
        public const int DelayMilliseconds = 10000;
        static Action<int, string, string> m_Event1 = delegate { };//
        static string town,  station;
        static int time;
        static Action m_Event2 = delegate { };
        public void Calculate(string town1, string station1,int time1)
        {
            town = town1;
            station = station1;
            time1 = time1;
            int veloLibre;
            veloLibre = getAvailableBikesService(town, station);
            m_Event1(veloLibre, town, station);
            m_Event2();
        }

        public void SubscribeCalculatedEvent()
        {
            ICalServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalServiceEvents>();
            m_Event1 += subscriber.Calculated;
        }

        public void SubscribeCalculationFinishedEvent()
        {
            ICalServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalServiceEvents>();
            m_Event2 += subscriber.CalculationFinished;
            init_timer();
        }

        public void init_timer()
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(TimeEvent);
            timer.Interval = 2000;
            timer.Enabled = true;
        }
        private void TimeEvent(object source, ElapsedEventArgs e)
        {
            int veloLibre;
            veloLibre = getAvailableBikesService(town, station);
            m_Event1(veloLibre, town, station);
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
                if (((string)item["name"]).ToLower().Contains(station.ToLower()))
                {
                    int veloLibre = Convert.ToInt32(item["available_bikes"]);
                    return veloLibre;
                }
            }

            return -1;
        }
    }
}
