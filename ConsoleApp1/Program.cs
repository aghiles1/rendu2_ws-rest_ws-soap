using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static VelibSoapConsole.ServiceVelibClient service = new VelibSoapConsole.ServiceVelibClient();
        private static string help = "Les commande sont suppoter en miniscule, masescule ou un melange des deux\n" +
            "towns : rend la liste des ville qui sont disponible (qui ont des station)\n" +
            "stations [town] : rend les stations de la ville demandé" +
            "availablevelib [town] [station]: rend le nombre de vélo disponible dans une station données qui se trouve dans une ville.";
        private static string error = "cette commande n'existe pas";
        static void Main(string[] args)
        {
            service.getTowns();
            Console.WriteLine("Bonjour ! Client Console");
            Console.WriteLine("Help : pour voir les commande et à quoi elles servent");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine().ToLower().Trim();
                if (command.Split().Length != 1) {
                    checkCommand(command);
                }
                else checkCommandUnaire(command);

            } while (command != "exit" && command!= "quit");
        }

        private  static void checkCommandUnaire(string command)
        {
            switch (command)
            {
                case "help":
                    Console.WriteLine(help);
                    break;
                case "towns":
                    Console.Write(Towns());
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                case "quit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(command +":"+error);
                    break;
            }
            
        }
        private static void checkCommand(string command)
        {
            string[] tab = command.Split(' ');
            if (tab.Length == 3 && tab[0] == "availablevelib")
            {
                Console.WriteLine(AvailableVelib(tab[1].Trim(),tab[2].Trim()));
            }
            else if (tab.Length == 2 && tab[0].Trim() == "stations")
            {
                Console.Write(Stations(tab[1].Trim()));
            }
            else
            {
                string err = (tab.Length == 0) ? "merci de donner une commande vous avez entrer une ligne vide\n" : command + ": " + error;
                Console.WriteLine(err);
            }

        }

        private static string Stations(string town)
        {
            string print = "Les stations de la ville "+town+" :\n";
            string[] station = service.getStations(town);
            if(station == null)
            {
                return "La ville indiqué n'existe pas\n";
            }
            foreach (string str in station)
            {
                print += str + "\n";
            }
            return print;
        }
        private static string Towns()
        {
            string print = "Les villes :\n";
            string[] town = service.getTowns();
            if (town == null)
            {
                return "Error!!!!";
            }
            foreach (string str in town ){
                print += str + "\n";
            }
            return print;
        }
        private static string AvailableVelib(string town,string station)
        {
            string print = "le nombre de velibs disponible est dans la station " + station + " qui se trouve dans la ville " + town + ": ";
            int v = service.getAvailableBikes(town,station,0);
            if (v == -1)
            {
                return "Assurez vous du nom de la ville et de la stations";
            }
            return print + v;
        }
       
    }
    
}
