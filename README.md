# rendu2_ws-rest_ws-soap
# SI4 rendu2_ws-rest_ws-soap
## Nom : DZIRI Aghiles


### Client console:
  les commandes et les pramètres doivent être séparés par un espace, pas plus.

    les commandes:
      - help: donne toute les commandes présantes avec les explications qui vont avec.
      - exit & quit: qui permet le programme.
      - towns: donne la liste des villes.
      - stations [city]: la liste des stations qui se trouvent dans la ville donnée en parametre.
      - availablevelib [city] [station]: les velos libres d'une station dans une ville données. Le parametre station peut etre une sous chaine donc la premiere station qui contient la sous chaine est selectionné, le nombre de velos libres de la station selectionée est retourné.

### Client GUI:
    Une interface qui permet de selectioner une ville et une station pour voire le nombre de velos libre. On peut donner un temps qui précise le delais des données qui dvoivent etre rendues.
    (le delais == l'ancienneté des données au max)
### Service:
    - Permet de donner les donner aux clients et utilise aussi un cache dynamique qui permet d'avoir les informations déja demandées plus rapidement.
## Les axes:
  * Graphical User Interface for the client (Mark Scale : 2 points)
  *  Replace all the accesses to WS (beetween Velib WS and IWS, between IWS and WS Clients) with asynchronous ones. Some indications can be find just below. (Mark Scale : 3 points)
  *  Add a cache in IWS, to reduce communications between Velib WS and IWS (Mark Scale : 4 points)




# SOURCES:
[mikehadlow](http://mikehadlow.blogspot.fr/2011/03/7000-concurrent-connections-with.html)  
[stackoverflow](https://stackoverflow.com)  
[msdn](https://msdn.microsoft.com)  
