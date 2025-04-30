# TODO

- 
A faire
config du db context 
ajustement des injextions
verrificarion des entitées
génération de la bas qslite locale
remplire la base avec des données fake


### **Initialisation du projet**

**Structure de base** :

* [X] Initialisation du projet d'API avec les extensions nesessaires
* [X] Préparation de l'arboressance / Structure du projet

  **Controllers**, **Models**, **DTOs**, **Services**, **Repositories**, **Configurations, ...**

Init des projets test :

* [X] Initialisation des projets **xUnit** d'Unitest avec ces composants de base (moq, Sqlite, ...).
* [X] Initialisation des projets **xUnit** d'Integration Tests avec ces composants de base (moq, Sqlite, ...).

**Gestion des dépendances** :

* [X] Intégrer classe d'extension pour enregistrer les dépendances et l'intégrer dans le fichier `Program`.

**Configurations** :

* [X] Intégrer des fichiers appsettings par environnement (Identifier les valeurs des paramètres par environnement).
* [X] Configuration du launchSettings pour visual studio.
* [X] Configurer les paramètres des fichiers d'environnement : `appsettings.Development.yml`, `appsettings.Production.yml`, ...

**Documentation** :

* [ ] Créer le fichier `README.md` et Initier la doccumentation du projet.

**Versionnement avec Git** :

* [X] Initialiser Git dans le projet
* [X] Ajouter un fichier `.gitignore` pour exclure les fichiers non pertinents (`bin/`, `obj/`, `secrets.json`, etc.).

---

### **Gestion des logs**

* [X] Installer et configurer **Serilog** ou une autre bibliothèque de logs. `Définir le repertoir cible pour les fichiers logs`
* [X] Ajouter une configuration pour la gestion des logs dans `appsettings.yml` (Console, Fichiers, ElasticSearch, etc.).
* [X] Ajouter un middleware pour loguer les requêtes entrantes et sortantes.

---

### **Configuration de Swagger**

* [X] Installer **Swashbuckle.AspNetCore** pour la documentation Swagger.
* [X] Configurer Swagger pour documenter les endpoints de l’API.
* [X] Ajouter des descriptions et des exemples pour les paramètres des endpoints.

---

### **Configuration des routes API**

* [X] Configuration des Controllers REST.

---

### **Versionning projet**

* [X] Implémentation & Configuration du versionning des routes API REST.  `Définir la stratégie de versionning à adopter`

---

### **Sécurité**

* [X] Implémentation & Configuration du Cors. `Identifier les Urls des clients cible`.
* [X] Implémentation & Configuration du Https.

---

### **Connection vers la base de données**

* [ ] Dév & Configuration d'entity framwork (dbcontext) pour la base cible.
* [ ] Dév & Configuration des entités de base.
* [ ] Dév & Configuration du mapping Model Entités.

---

### **Documentation**

* [ ] Enrichir le fichier `README.md` avec des instructions sur la configuration et l'utilisation.
* [X] Ajouter des commentaires XML pour chaque méthode publique (configurable avec Swagger).
