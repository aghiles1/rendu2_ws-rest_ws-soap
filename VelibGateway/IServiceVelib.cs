using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibGateway
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceVelib
    {
        [OperationContract]
        IList<string> getStations(string city);

        [OperationContract]
        int getAvailableBikes(string city, string station,int time);

        [OperationContract]
        IList<string> getTowns();

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetStations(string City, AsyncCallback callback, object state );
        IList<string> EndgetStations(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetAvailableBikes(string town, string station, int time, AsyncCallback callback, object state);
        int EndgetAvailableBikes(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetTowns( AsyncCallback callback, object state);
        IList<string> EndgetTowns(IAsyncResult asyncResult);
    }
}