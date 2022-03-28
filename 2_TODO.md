
- 5 Todo : 
- mieux séprarer chaque services 
  - object ( request /  Response )  
  - fonctions étoffer et utiliser chaque fonction de services ( GET / UPDATE )  + fct utiles genre IsInLawn()
- faire une vrai DAl pour les objets persistants/ séparer le stockage des couches business
- APi dockerisée contenant business / au moins spliter en libs
- clean les rethrow d'exceptions  
- clean warning de build :/  


suggestion pour la suite ameliorer l'application 


  -  fct:  gestion de collision, gestion des terrains non rectangles, run simultatné des mowers 
  -  tech:    
    -  API Devant la couche business
      - *terrain bien préparé avec injection de dependances / contracts*/   
      -  perf, persistance, docker, un vrai contrat avec swagger en json ... ( dev, perf, lts, prix, fiabilité ... ) 
      -  liveness, metriques, logs, sécurité ( cors, encryption, auth: selon l'exposition ) 
  -  cicd: versioning, mise en place de build analyseurs de qualités ( sonar, pact, test charge auto, trivy, ...)   
  -  archi:  split en lib / en micro services des différentes fonctionnalités 
  
  *Moyens à mettre en oeuvre doivent être en rapport avec le besoin* 


