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
            CalServiceCallbackSink objsink = new CalServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
            ServiceEvent.CalServiceClient objClient = new ServiceEvent.CalServiceClient(iCntxt);
            objClient.SubscribeCalculatedEvent();
            objClient.SubscribeCalculationFinishedEvent();
            objClient.Calculate("nantes", "alger",0);
            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }

    }
}
