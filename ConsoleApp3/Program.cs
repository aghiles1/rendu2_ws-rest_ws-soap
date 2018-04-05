using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Merci de donner une ville pour le subscribe");
            string town = Console.ReadLine();
            Console.WriteLine("Merci de donner une stations avec le nom complet pour le subscribe");
            string station = Console.ReadLine();
            CalServiceCallbackSink objsink = new CalServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
            ServiceEvent.CalServiceClient objClient = new ServiceEvent.CalServiceClient(iCntxt);
            objClient.SubscribeCalculatedEvent();
            objClient.SubscribeCalculationFinishedEvent();
            objClient.Calculate(town, station, 0);
            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }

    }
}
