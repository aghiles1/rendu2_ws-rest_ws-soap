using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class CalServiceCallbackSink : ServiceEvent.ICalServiceCallback
    {
        
        public void Calculated(int nbvelo, string town, string station)
        {
            Console.WriteLine("le nombre de velibs disponible dans la ville "+town+" et sur la station "+station+" :\n");
            if(nbvelo == -1)
            {
                Console.WriteLine("Merci de bien vérifier que la station et la ville sont bien disponible");
            }
            else
            {
                Console.WriteLine(nbvelo+" velibs libres");
            }
        }

        public void CalculationFinished()
        {
            Console.WriteLine("Calculation completed");
        }
    }
}
