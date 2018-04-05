using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public interface ICalServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Calculated(int nbvelo, string town, string station);

        [OperationContract(IsOneWay = true)]
        void CalculationFinished();
    }
}
