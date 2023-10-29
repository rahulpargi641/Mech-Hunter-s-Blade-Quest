# Mech-Hunters-Blade-Quest
 
### Introduction

    A thrilling 3D action game where you step into the role of the Mech Ninja, tasked with hunting down 
    rogue mechs and their formidable bosses This project prioritizes maintaining code quality and ensuring 
    the game's adaptability for future enhancements.
    
### Features
    Visual and Audio Quality:
    - Crafted with high-quality graphics and exceptional sound effects.
    
    Core Gameplay:
    - Player actions include running, rolling, and attakcking with swords.
    - Three types of attacks:
       - Normal Attack (one sword attack)
       - Combo Attack (two swords attack)
       - Dash Attack (slide forward and attack from a distance).

    Healing Orbs:
    - Defeating enemies causes them to drop healing orbs that restore the player's health.

    Boss Fight:
    - The boss enemy launches a barrage of fire orbs that players must skillfully evade to minimize damage.
    
    Smooth Camera Follow:
    - A smooth camera system ensures seamless tracking of the player's movements.
    
    User Interface (UI):
    - Visually appealing Main Menu, Pause Menu, Game Over, and Level Complete screens.
    - The Pause screen offers convenient options for resuming the game or quitting.
    
### Screenshots

   (Insert screenshots)
  
### Code Structure and Game Design
#### Code Structure

    MVC-S (Model-View-Controller-Service):
        - The codebase is organized using the Model-View-Controller-Service (MVC-S) architectural pattern.
        - This approach maintains a clear separation of concerns:
           - The Model manages data.
           - The View is responsible for UI-related tasks and input handling.
           - The Controller is responsible for updating both the View and Model.
        - Classes for Player, Enemy, Pickups(Healing Orb), DamageOrb(FireOrb), GameManager, GameUI, Level, 
          Score have been implemented following the MVC pattern.

    Singletons:
        - Centralized control is ensured through the implementation of Singleton patterns.
        - Essential services such as PlayerService, EnemyService, PickupsService(Healing Orb), 
          DamageOrbService(FireOrb), GameService, GameUIService, LevelService, are designed as Singletons.
       
    Observer Pattern:
        - Employed the observer pattern to decouple classes and facilitate event handling for events such as 
          player death, enemy hit, enemy death, and enemy group death.
        - This facilitates the activation of the game over screen and the opening of the gate when the enemy group is defeated.

    Scriptable Objects:
       - Food Types:
       - Two distinct food types are available:
          - Mass Gainer - Egg, Meat, Fish
          - Mass Burner - Green Strawberry, Pumpkin
       - Powerup Types:
          - Three distinct food types are available:
         - Speed Boost, Score Boost, Shield
    - Snake Scriptable Object:
       - Used for configuring snake-related data.
       
#### Performance Optimization:

    - To optimize performance, object pooling is implemented for arrows, coins, and enemies, 
      which helps manage memory and CPU usage efficiently.

#### Input Management:

    - Integrated Unity's new Input System to simplify the mapping of inputs to in-game actions. 
    - Custom input actions like OnJump() and OnFire() have been defined for jumping and shooting.

#### Level Design:

    - For rapid level design, Unity's Tilemap was utilized, enabling expedited level creation by 
      painting tiles and specifying tilemap rules.
    - Created multiple layers, including platform, Enemy, Obstacle, Water, Bouncy, and Player, to 
      distinguish between different types of tiles.

#### Enchanced Camera Tracking

    - Employed Cinemachine to smoothly follow the player. 
    - Utilized state-driven cameras like the run camera, climb camera, and idle camera for seamless 
      transitions between different camera views.

#### Animations:

    - The Animator manages a range of animations, including Idle, Run, Climb, Jump, Shooting, and Hurt.

### Focus
    - Code Quality and Organization.
    - Architecture Design for Flexibility and Scalability.
    - Visually appealing, with soothing music and sound effects. 

### Gameplay Demonstration
