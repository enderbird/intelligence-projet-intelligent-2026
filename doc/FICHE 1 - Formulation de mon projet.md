# FICHE 1 — Formulation de mon projet

---

## RÉSUMÉ

### Nom du projet

**fIAh (Fish AI) — Apprentissage autonome de la nage et de la recherche de nourriture**

### Projet inspirant de départ

- Teach your AI! Imitation Learning with Unity ML-Agents — Code Monkey
- [Lien vers la vidéo YouTube](https://www.youtube.com/watch?v=supqT7kqpEI)

### Ce que je dérive, remplace ou modifie pour mon propre projet

Je m'inspire du principe d'entraînement d'un agent intelligent dans Unity avec un environnement physique et des récompenses. Cependant, au lieu d'apprendre à un personnage à se déplacer sur un terrain, je souhaite entraîner des poissons virtuels à nager dans un environnement aquatique. Je vais utiliser la physique de Unity, notamment la gravité, la friction, l'inertie et les forces appliquées au corps du poisson. L'objectif sera que chaque poisson apprenne à contrôler ses mouvements de façon autonome afin de rester en suspension dans l'eau, d'éviter le fond et de trouver de la nourriture qui change constamment de position.

### Paragraphe 1 — Objectif et problème d'IA résolu

Mon projet consiste à créer une intelligence artificielle capable d'apprendre à nager dans un environnement 3D sous-marin à l'aide de Unity. L'objectif est d'entraîner des poissons virtuels à se déplacer de manière autonome en tenant compte des contraintes physiques de leur environnement, comme la gravité, la friction et leur inertie. Chaque poisson devra apprendre à contrôler les forces appliquées à son corps pour se maintenir dans l'eau, éviter de toucher le fond et atteindre une cible représentant de la nourriture. Il s'agit d'un problème d'apprentissage par renforcement (Reinforcement Learning), où l'agent apprend progressivement les meilleures actions à effectuer grâce aux récompenses et aux pénalités reçues pendant ses tentatives.

### Paragraphe 2 — Fonctionnement concret, catégories / actions et cas d'usage

Chaque poisson sera représenté par un corps physique dans Unity, contrôlé par un réseau neuronal. L'agent observera son environnement, notamment sa position, son orientation, sa vitesse, sa rotation, la distance entre son corps et le fond ainsi que la direction et la distance de la nourriture. Il pourra appliquer des forces dans les six directions de l'espace (haut, bas, gauche, droite, avant et arrière), avec différentes intensités. Ces forces pourront être combinées afin de produire des déplacements dans plusieurs directions. L'orientation du corps influencera également les déplacements puisque les forces pourront être appliquées selon les axes locaux du poisson. Lorsqu'il atteindra la nourriture, il recevra une récompense et une nouvelle nourriture apparaîtra à un emplacement aléatoire dans un rayon défini autour de lui. Le poisson continuera donc son apprentissage sans nécessairement retourner à sa position de départ. Le but est d'obtenir un poisson capable de contrôler naturellement son corps, de rester en suspension dans l'eau et de retrouver sa nourriture même lorsque celle-ci change constamment de position.

### Illustrer le projet

- Illustration prévue : un poisson ou un cube représentant son corps dans un environnement aquatique 3D, avec des flèches montrant les six directions dans lesquelles il peut appliquer des forces.
- L'environnement devrait comprendre un fond dangereux, une nourriture qui change de position et un espace dans lequel le poisson peut se déplacer librement.

---

## DONNÉES OU UNIVERS VIRTUELS

### Sources de données ou univers virtuels

Les données proviendront entièrement d'un environnement virtuel créé dans Unity 3D, plutôt que d'une banque de données externe. L'environnement contiendra des poissons contrôlés par une intelligence artificielle, un fond représentant le plancher sous-marin et des objets représentant la nourriture.

Les positions de la nourriture seront générées aléatoirement dans une zone définie autour du poisson. Cette variation permettra à l'agent de rencontrer différentes situations et d'apprendre à s'adapter à plusieurs positions de cible.

Les informations utilisées pendant l'entraînement seront notamment :

- La position du poisson dans l'espace (X, Y, Z).
- Son orientation et sa rotation.
- Sa vitesse de déplacement.
- Sa vitesse de rotation.
- La direction et la distance de la nourriture par rapport au poisson.
- La distance entre le corps du poisson et le fond.
- Les forces et les actions effectuées par le poisson.
- Les récompenses et les pénalités obtenues pendant l'entraînement.

### Préparation

Les observations de l'environnement seront transformées en valeurs numériques que le réseau neuronal pourra traiter. Les distances, les positions, les vitesses et les informations directionnelles seront représentées sous forme de nombres flottants (`float`).

Les positions de nourriture seront générées aléatoirement dans un rayon déterminé autour du poisson. Lorsqu'il atteindra sa nourriture, une nouvelle cible apparaîtra à une autre position autour de lui. Je souhaite conserver la position actuelle du poisson après une réussite afin qu'il puisse continuer à apprendre à partir de différents endroits, plutôt que de recommencer systématiquement au même point de départ.

Les données seront directement collectées pendant les simulations Unity grâce au système Unity ML-Agents. Il ne sera donc pas nécessaire de préparer des images ou un dataset traditionnel.

---

## ALGORITHME INTELLIGENT

### Langages, librairies et run-time utilisés

- **Langage d'entraînement de l'IA :** Python, avec Unity ML-Agents.
- **Langage d'affichage / visualisation :** C# avec Unity.
- **Librairies d'intelligence artificielle :** Unity ML-Agents et PyTorch, utilisé par le processus d'entraînement ML-Agents.
- **Librairies mathématiques et de manipulation de données :** NumPy.
- **Librairies graphiques ou de connectivité :** Unity 3D et son système physique (`Rigidbody`, `Collider`).
- **Run-time prévu :** Unity 6000.3.28f1 pour la simulation 3D et Python pour l'entraînement de l'agent.

### Algorithme utilisé et type

- **Famille d'algorithme choisie :** Réseau neuronal dense (MLP) utilisé avec l'apprentissage par renforcement.
- **Type précis de tâche :** Apprentissage du déplacement autonome et du contrôle physique d'un corps dans un environnement 3D avec recherche de nourriture.
- **Justification :** Le projet inspirant utilise également Unity ML-Agents pour entraîner un agent dans un environnement simulé. Je reprends ce principe, mais je remplace les déplacements d'un personnage par le contrôle physique d'un poisson qui doit apprendre à nager, à se stabiliser et à atteindre une cible mobile.

### Structure et paramètres

- **Format d'entrée du modèle :** Vecteur numérique contenant les observations du poisson : position, orientation, vitesse, vitesse de rotation, distance par rapport au fond et direction et distance de la nourriture.
- **Architecture interne prévue :** Réseau neuronal dense (MLP) qui reçoit les observations et apprend à sélectionner les forces à appliquer au corps du poisson.
- **Format de sortie :** Actions continues représentant l'intensité des forces à appliquer dans les six directions de l'espace. Les forces pourront être combinées pour produire des déplacements diagonaux ou dans n'importe quelle direction intermédiaire.
- **Fonction d'évaluation et récompenses :**
  - Récompense positive lorsque le poisson atteint la nourriture.
  - Pénalité importante lorsqu'il touche le fond, ce qui provoque sa mort.
  - Petite récompense lorsqu'il se rapproche de la nourriture.
  - Petite pénalité pour le temps écoulé ou les mouvements inutiles, afin de favoriser une recherche efficace.
  - Récompense éventuelle pour le maintien d'une orientation stable et d'une position sécuritaire dans l'eau.

### Esquisse de la structure prévue

1. Unity génère un environnement aquatique avec un poisson et une nourriture.
2. Le poisson observe son environnement et transmet ses valeurs d'observation à son réseau neuronal.
3. Le réseau neuronal choisit les forces à appliquer dans les différentes directions.
4. Unity applique les forces au corps du poisson à l'aide de son système physique (`Rigidbody`).
5. Le corps réagit à la gravité, à la friction, à l'inertie et aux forces appliquées.
6. Le poisson se rapproche de la nourriture et reçoit des récompenses selon ses résultats.
7. Lorsqu'il atteint la nourriture, une nouvelle cible apparaît dans un rayon défini autour de sa position actuelle.
8. Si le poisson touche le fond, il meurt et reçoit une pénalité.
9. L'agent continue son apprentissage afin d'améliorer progressivement son déplacement et son contrôle physique.

L'entraînement sera effectué avec Unity ML-Agents. L'algorithme exact, l'architecture du réseau et les paramètres pourront être ajustés selon les résultats obtenus.

---

## VISUALISATION

### Technologies de visualisation

La visualisation principale sera réalisée dans Unity 3D, avec un environnement sous-marin comprenant le poisson, le fond et les objets de nourriture.

Je souhaite pouvoir observer directement le comportement du poisson pendant son entraînement :

- Ses déplacements dans les trois axes.
- Ses rotations et changements d'orientation.
- Les forces qu'il applique à son corps.
- Sa façon de se maintenir en suspension dans l'eau.
- Ses collisions avec le fond.
- Sa façon de se diriger vers la nourriture.

Je prévois également d'utiliser TensorBoard, fourni avec le processus d'entraînement ML-Agents, pour observer l'évolution des récompenses et mesurer les progrès de l'agent au fil des épisodes.

### Exemple de visualisation

Le résultat final devrait montrer un poisson flottant dans un environnement aquatique 3D, capable d'appliquer différentes forces à son corps, de se déplacer dans toutes les directions et de rechercher automatiquement de la nourriture qui apparaît à différents endroits autour de lui.

L'objectif visuel est d'obtenir un poisson qui apprend progressivement à contrôler son corps, à maintenir sa position dans l'eau, à corriger son orientation et à atteindre sa nourriture sans intervention humaine.

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

---

# SECTION SUPPLÉMENTAIRE — CONCEPT ET IDÉES PHYSIQUES DU PROJET

Cette section regroupe les idées de fonctionnement physique que je souhaite expérimenter dans fIAh. Elle sert de référence pour le développement dans Unity et pourra évoluer pendant la réalisation du projet.

## 1. Représentation physique du poisson

- Au départ, le poisson pourrait être représenté par un cube servant de corps physique central.
- Le centre du cube représentera le centre du corps du poisson.
- Le corps utilisera un `Rigidbody` de Unity pour être soumis aux forces physiques.
- Il pourra flotter et se déplacer librement dans un environnement 3D.
- Le corps sera influencé par :
  - La gravité.
  - La friction ou résistance au mouvement.
  - L'inertie.
  - Les collisions.
  - Les forces appliquées par l'intelligence artificielle.
- Le poisson ne sera pas déplacé directement avec des changements de position arbitraires : il devra apprendre à contrôler son déplacement grâce aux forces physiques.

## 2. Les six directions de déplacement

Le poisson pourra appliquer une force dans les six directions correspondant aux faces d'un cube :

- Haut : +Y.
- Bas : -Y.
- Droite : +X.
- Gauche : -X.
- Avant : +Z.
- Arrière : -Z.

Les six directions pourront être combinées.

Par exemple :

- Une force vers le haut et vers la gauche produira un déplacement diagonal.
- Une force vers l'avant et vers le haut permettra au poisson de monter tout en avançant.
- Une force vers le bas et vers l'arrière permettra de descendre tout en reculant.

Les forces pourront avoir différentes intensités. L'IA ne sera donc pas limitée à un simple choix entre six boutons : elle pourra apprendre à doser ses forces.

**Important :** je souhaite que ces directions puissent être relatives à l'orientation du poisson. Ainsi, si son corps est renversé, sa direction « haut » locale ne correspondra pas nécessairement au haut du monde Unity.

## 3. Rotation et orientation du corps

Le poisson ne sera pas toujours orienté correctement.

- Il pourra tourner sur lui-même.
- Il pourra se retrouver incliné sur le côté.
- Il pourra se retrouver complètement à l'envers.
- Son orientation modifiera la direction réelle des forces si elles sont appliquées selon ses axes locaux.
- L'IA devra apprendre à reconnaître son orientation et à corriger ses mouvements.

Les observations pourraient inclure :

- La rotation du corps.
- Son orientation par rapport à la verticale.
- Sa vitesse angulaire, c'est-à-dire sa vitesse de rotation.

**Idée physique :** si une force est appliquée directement au centre de masse, elle ne crée normalement pas de couple de rotation. Pour obtenir des rotations naturelles, je pourrais appliquer certaines forces à des points décalés du centre du corps ou ajouter des actions de couple (`AddTorque`). Cela permettra éventuellement au poisson d'apprendre à contrôler son orientation.

## 4. Gravité et maintien en suspension

Le poisson devra apprendre à ne pas tomber au fond de l'environnement.

- La gravité attirera naturellement le corps vers le bas.
- Le poisson devra appliquer suffisamment de force vers le haut pour compenser cette gravité.
- S'il n'applique aucune force vers le haut, il finira par descendre.
- S'il applique trop de force vers le haut, il pourra monter trop rapidement.
- Il devra donc apprendre à doser ses forces pour rester à une hauteur sécuritaire.

L'objectif est qu'il puisse se maintenir en suspension dans l'eau sans avoir besoin d'être replacé constamment à une position prédéfinie.

## 5. Le fond de l'environnement et la mort

Le fond représentera une zone dangereuse.

- Si le poisson touche le sol, il meurt.
- Une collision avec le fond déclenchera une pénalité importante.
- L'agent devra apprendre à reconnaître sa distance par rapport au fond.
- Il devra anticiper les risques de collision plutôt que simplement réagir après avoir touché le sol.

La distance entre le corps du poisson et le fond sera donc une observation importante pour son intelligence artificielle.

## 6. Nourriture et génération aléatoire

La nourriture représentera l'objectif que le poisson doit atteindre.

- La nourriture apparaîtra à différentes positions dans l'environnement.
- Sa position sera générée aléatoirement dans un rayon défini autour du poisson.
- L'IA devra connaître la direction et la distance qui la séparent de la nourriture.
- Lorsqu'il atteindra la nourriture, il recevra une récompense.
- Une nouvelle nourriture apparaîtra ensuite à une autre position.
- Le poisson conservera sa position actuelle après avoir mangé.

L'objectif est de permettre au poisson d'enchaîner plusieurs recherches de nourriture sans recommencer chaque fois au même point de départ.

## 7. Observations reçues par l'intelligence artificielle

L'IA devra connaître suffisamment d'informations pour comprendre son état physique.

Les observations envisagées sont :

- **Position :** coordonnées X, Y et Z du corps.
- **Orientation :** rotation ou orientation du poisson dans l'espace.
- **Vitesse linéaire :** vitesse de déplacement sur les trois axes.
- **Vitesse angulaire :** vitesse de rotation du corps.
- **Distance du fond :** distance entre le poisson et le plancher.
- **Direction de la nourriture :** direction relative de la cible par rapport au poisson.
- **Distance de la nourriture :** proximité de la cible.

Ces informations seront transformées en valeurs numériques pour être utilisées par le réseau neuronal.

## 8. Actions produites par l'intelligence artificielle

Les actions de l'agent seront les forces appliquées au corps physique.

- Force vers le haut.
- Force vers le bas.
- Force vers la gauche.
- Force vers la droite.
- Force vers l'avant.
- Force vers l'arrière.

Chaque action pourra avoir une intensité différente.

Une autre possibilité serait d'ajouter des actions de rotation avec des couples sur les trois axes, si le contrôle de l'orientation devient nécessaire.

## 9. Système de récompenses envisagé

| Situation                                |       Récompense envisagée |
| ---------------------------------------- | -------------------------: |
| Atteindre la nourriture                  |  Forte récompense positive |
| Se rapprocher de la nourriture           | Petite récompense positive |
| Toucher le fond                          |     Forte pénalité et mort |
| Rester trop longtemps sans progresser    |            Petite pénalité |
| Maintenir une orientation stable         |      Récompense éventuelle |
| Réussir plusieurs recherches successives |       Récompenses répétées |

Les valeurs exactes seront déterminées pendant les tests. L'objectif sera d'encourager le poisson à atteindre sa nourriture tout en apprenant à contrôler son corps.

## 10. Gestion des épisodes et continuité de l'apprentissage

Je souhaite éviter que le poisson retourne systématiquement à sa position de départ après avoir atteint sa nourriture.

Le fonctionnement envisagé est le suivant :

1. Le poisson commence dans une position initiale.
2. Il apprend à se déplacer vers une nourriture.
3. Il atteint la nourriture et reçoit une récompense.
4. La nourriture disparaît et une nouvelle apparaît dans un rayon défini autour du poisson.
5. Le poisson conserve sa position et continue à nager.
6. Le processus recommence jusqu'à ce que le poisson meure ou que la durée de simulation soit atteinte.

Il faudra distinguer le renouvellement de la nourriture de la fin d'un épisode ML-Agents : atteindre une nourriture ne signifie pas nécessairement que l'épisode doit être terminé. Une mort ou une limite de temps pourra, elle, déclencher une réinitialisation complète de l'épisode.

## 11. Objectif final

L'objectif final de fIAh est de créer un poisson virtuel capable d'apprendre à contrôler son propre corps dans un environnement physique 3D.

Il devra apprendre à :

- Se déplacer dans les six directions.
- Combiner différentes forces.
- Contrôler sa vitesse et son inertie.
- Comprendre son orientation et ses rotations.
- Se maintenir en suspension malgré la gravité.
- Éviter le fond pour survivre.
- Repérer et atteindre de la nourriture qui change constamment de position.
- Continuer à chercher de la nourriture sans revenir systématiquement à son point de départ.

À long terme, je souhaite obtenir un comportement de nage autonome qui semble naturel, plutôt qu'un simple objet qui se déplace directement vers une cible.
