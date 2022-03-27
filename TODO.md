
- 5 Todo : 
- mieux séprarer chaque services plein de facilités ont été utilisées
- faire une vrai DAl pour les objets persistants   
- APi dockerisée contenant business / au moins spliter en libs
- clean les rethrow d'exceptions  


suggestion pour la suite ameliorer l'application 


  -  fct:  gestion de collision, gestion des terrains non carrés 
  -  tech:    
    -  API Devant la couche business
      - *terrain bien préparé avec injection de dependances / contracts*/   
      -  perf, persistance, docker, un vrai contrat avec swagger en json ... ( dev, perf, lts, prix, fiabilité ... ) 
      -  liveness, metriques, logs, sécurité ( cors, encryption, auth: selon l'exposition ) 
  -  cicd: versioning, mise en place de build analyseurs de qualités ( sonar, pact, test charge auto, trivy, ...)   
  -  archi:  split en lib / en micro services des différentes fonctionnalités 
  -  *Moyens à mettre en oeuvre doivent être en rapport avec le besoin* 


