using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.VelibSoap;

namespace WindowsFormsApp1
{
    static class IntermediateAsync
    {
       
        public static async  Task getStationsAsync(ServiceVelibClient client, string city, System.Windows.Forms.ComboBox cb1)
        {
            //comme j'avais un probleme de cast avec un retun le plus facile été de modifier directement la combobox dans cette fonction
            cb1.Items.AddRange(await client.getStationsAsync(city));
        }
        public static async Task getAvailableBikesAsync(ServiceVelibClient client, string town, string station, int seconds, System.Windows.Forms.TextBox tb1)
        {
            //comme j'avais un probleme de cast avec un retun le plus facile été de modifier directement la combobox dans cette fonction
            tb1.Text = (await client.getAvailableBikesAsync(town,station,seconds)).ToString();
        }

        public static async Task getTownsAsync(ServiceVelibClient client, System.Windows.Forms.ComboBox cb2)
        {
            //comme j'avais un probleme de cast avec un retun le plus facile été de modifier directement la combobox dans cette fonction
            cb2.Items.AddRange(await client.getTownsAsync());
        }
    }
}
