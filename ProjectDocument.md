# Game Basic Information #

## Summary ##

Immerse yourself in the breathtaking world of Sovereign Saga, an immersive 2D RPG ripe with adventure. Venture through the challenges and opportunities found within a diverse island, each step taking you closer to becoming its ruler. Make your way through the diverse terrains, where your tactical prowess will be tested against an army of slimes. Employ combat strategies and economic ventures, using your wealth to ascend in power. In Sovereign Saga, your destiny is in your hands. Do you have the mettle to conquer and rule?

## Gameplay Explanation ##

2D RPG game where the goal is to become a ruler of the island. You start off with very limited money, and choose an approach to generate income in order to improve your title or standing in the society. The player has starting stats for Stamina, Strength, and Intelligence, and improves them over the course of the game.

The island will be divided into different regions. Players can choose from multiple approaches to generate money, shaping their path to rulership. Players can participate in thrilling battles in the caves, or even become early businessmen to generate passive income by obtaining all the land property on the islands. The world will be filled with varieties of magic that can be acquired by the player to use in combat. As players play and generate some wealth, they will be able to invest their resources in upgrading their character’s abilities and unlocking new skills. Stamina is the character’s overall endurance and energy in physical tasks that allows the player to dash in combat. Strength is combat prowess. Intelligence is used for dwelling into the world of Arcana.

CONTROL SCHEME:

MOVEMENT: WASD OR ARROW KEYS

COMBAT: LEFT MOUSE CLICK

DASH: SPACE

MAGIC: Z = fireball; X = rock; C = tornado; Q = Vortex


**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer

**Describe the steps you took in your role as producer. Typical items include group scheduling mechanism, links to meeting notes, descriptions of team logistics problems with their resolution, project organization tools (e.g., timelines, depedency/task tracking, Gantt charts, etc.), and repository management methodology.**

## User Interface (Mohamed Ali Boutaleb @mohalibou)

When designing the user interface, I wanted to follow a design pattern where the UI design aesthetic is simple, clean, and intuitive. The aim is to make all crucial information readily available to the player without overwhelming the game screen. The UI is designed to be glanceable, ensuring that important information is accessible but does not obstruct the player's immersion in the game world.

I utilized a combination of unity UI elements, custom made images, custom fonts, and a collection of [SF Symbols](https://developer.apple.com/sf-symbols/) provided by Apple in order to make the user interface.

*Player Stat Display* - On the top left of the screen, I implemented a UI panel that shows the player's stats. This includes health, stamina, intelligence, strength, and money. This information is critical for players as they navigate the island, strategize their growth, and make decisions on resource allocation and skill upgrades. [Image of the UI element.](https://imgur.com/a/l4uHYS5) [Code for the implementation.](https://github.com/gzaets/ECS189LGroupProject/blob/127c1085e9f14134a3a231ccaae2a65f347e3172/Sovereign%20Saga/Assets/Scripts/PlayerController.cs#L265-L276)

*Building Purchases Popups* - Whenever a player wants to make building purchases, there will be a pop-up UI that provides only the necessary details related to purchasing the building. These pop-ups were designed to be informative yet unobtrusive, ensuring that players can easily continue their gameplay after making their decisions. [Image of the UI element.](https://imgur.com/a/QpFgE2e) [Code for the implementation.](https://github.com/gzaets/ECS189LGroupProject/blob/95fb360e1d161850f159c9be5be817f74850d85c/Sovereign%20Saga/Assets/Scripts/BuildingItem.cs#L99-L100)

*Game Over/Main Menu* - I also designed the game over menu, and main menu, providing players with options to exit or enter the game without disrupting the flow of the game. [Images of the UI elements.](https://imgur.com/a/8BAxONR) [Code for swapping scenes between the main menu and the game world.](https://github.com/gzaets/ECS189LGroupProject/blob/8064c282ef477012e9b929734c1cb52955400d0c/Sovereign%20Saga/Assets/Scripts/SceneSwitcher.cs#L1-L14)

## Movement/Physics (@j-p-ecs, Personal Name Hidden for Privacy Reasons)

The following has been written by @j-p-ecs (GitHub username):

Physics has been implemented in part manually and in part using Unity’s physics engine.

Unity’s physics engine is used to handle collisions in situations where trying to handle it manually would be nontrivial or otherwise extremely tedious, especially dealing with irregular polygons such as consecutive tiles of water. 

Building collision physics were, on the other hand, programmed manually and seek to optimize the player experience with no slowdowns resulting from rubbing against walls, etc. This is contrary to water tiles, which incur a slowdown if the player rubs against them because those rely solely on Unity’s physics engine.

Physics were primarily implemented by attaching Collider and Rigidbody components to the desired objects. In cases where we want to control the behavior more precisely, we added tags to some objects to determine whether special behavior should occur. For example, cave doors have a tag that is checked for during a collision and if the tag matches, it will determine which portion of the dungeon the player should be transported to.

Another aspect of physics which I incorporated was knockback with regards to collision with slime objects. My approach here basically involved setting a knockback animation time, which I calibrated to be about a third of a second. During that third of a second, if a collision occurred, and depending on how forceful the collision is, the slime would move back at some multiple of the collision speed. In our testing, it was a decent and smooth implementation. It may not be absolutely perfect by all standards, but I am decently satisfied with the result.

Regarding collisions with cliffs, I had challenges figuring out how to implement this with Unity without defacing a beautiful map design. The strategy I decided to go with was duplicating the design with another attachment, then setting the layer to be a very low number so that the layer would not be seen during gameplay (i.e. layer -99). I then attached a tilemap collider to that component and removed the appropriate tiles from that layer as necessary. If this was not done, this would cause the player to be unable to traverse through significant portions of the map. I did investigate working with Edge Colliders, but I was unable to figure out how to get those working.

Physics has different modes, which was a feature we intended to implement during the beginning of this project. When health is high, the player will move at a relatively high speed compared to when the health is low. When the health is low, the player will move slower and may subsequently struggle to move away from slime enemies. In addition, there is a dash effect that gives the player a short boost for a given period of time, and an effect (created by @fdang2000) will appear when that movement has occurred.

The following are not directly related to physics but were implemented by @j-p-ecs:

I utilized the base code that was used to implement building purchases (which was done by other teammates, including @payday2021) to implement magic combat ability purchases. This was a relatively trivial implementation that involved creating a script to display the appropriate purchasing UI upon colliding with an NPC in the overworld. After a purchase has occurred with the NPC, then I use the Destroy function to remove the object.

I also created the game win screen, which uses the template created by @mohalibou for the game over screen. The game win screen is equipped with the proper transitions.

I also worked with the logic relating to the game over. In particular, the game has been programmed so the player does not move when a game over has been reached, regardless of any input given.

## Animation and Visuals (Fred Dang @fdang2000)

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

**Fred Dang (Animations and Visuals 1)**

*   **Animations**
    * For hero movement, I did not want the player to just simply slide across the screen as this would break from immersion and game feel, so I created animations for the cardinal directions and implemented them. In order to further improve game feel, I wanted the player to idle at the direction that they were last facing. To do so, I created blend trees in the Unity Animator for movement and idle states and switched between the two. Blend trees for movement/idle states take into account horizontal and vertical input to determine which animation should be played. These two attributes are parameters that are set in the PlayerController.cs. I also have transitions from any of these states to a "dead" state when the player dies which triggers the death animation. All of these implementations are to improve game feel to make it more realistic in this context. 

    * I also created animations for all magic skills and the sword slash using borrowed sprites. 
        * For magic skills, each magic sprite came with a sprite sheet that shows it in different states. For example, the fireball as it throughout time. This makes the fireball more realistic since it is not simply one sprite but rather multiple sprites of the fireball in different states to bring out realism which improves game feel. This can be seen for the other three skills which follow identical principles: the suck, rock, and tornado. Animating these skills were simply adjusting changing the sprites of the game object.

        * The sword animation is a bit more complex. It uses a sprite that was borrowed in the credits below. I had to create an idle animation for the sword where it would just be one sprite since nothing is occurring. However, when the sword slashed, I animated the sword rotating as well as toggling its hitbox on/off and making it appear and disappear. To make sync these changes with the combat system, I had to use animation events to tell the WeaponController.cs when the hitbox was on or off. In doing so, I created a sword that slashes around the player and disappears when the slash is finished because a floating sword would look weird. As such, all these changes improve game feel. 
            * While most animations had no fade time or exit time, the slash animation had to have one to make it slowly fade back so it did not look weird when the sword instantly returned to its original position. Again, this is for the sake of game feel. 

        * I also created an animation to simulate a "after-image" effect when dashing. I thought that this would improve game feel since increasing the player's movement speed temporarily is boring. With this, the player can "feel" the increased speed as seen by the after images. Animation-wise, the after image is a copy of the hero that plays an animation where it changes colors and slowly fades out. It also would not loop since it deletes after finishing the animation. 

* **Visuals**
    * For visuals, I chose these assets: 
        * [10 Pixel Magic Spell Effects with Icons](https://foozlecc.itch.io/pixel-magic-sprite-effects) by Foolze licensed under Creative Commons Zero, CC0.
        * [20 Evolving Fantasy RPG Weapons](https://assetstore.unity.com/packages/2d/textures-materials/20-evolving-fantasy-rpg-weapons-61204) by Landon Grant licensed under Standard Unity Asset Store EULA.
    * I chose these visuals because I wanted to match the art style that George chose as closely as possible. I was not aware that we could use copyrighted assets so I tried to avoid those as much as possible and chose the ones above. These assets relate to game feel in that they are closely related to the art style of the game in that they are "pixel-y" so they fit in. When dealing with a fantasy world, you would think of swords and magic, and thus, those types of visuals were chosen above for the sake of world building. All other visuals were chosen by George and as such, he has his own section here. 

* **Resources I Used**
    * [Create Melee Attack / Combat in Unity - P1 - Attack Animation
](https://www.youtube.com/watch?v=7vMHTUwtyNs)
    * [2D Melee in Unity Tutorial
](https://www.youtube.com/watch?v=giJKCl-GVrU)
    * [Make a 2D Ghost Effect: How to with Unity and C#
](https://www.youtube.com/watch?v=y982Gb00dho)
    * [TOP DOWN MOVEMENT in Unity!
](https://www.youtube.com/watch?v=whzomFgjT50)
    * [How to make an object follow the mouse in Unity (in 2D)](https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/)

* **Images**

    * Hero Animator
    ![HeroAnimator](Images/HeroMovement.png)

    * Hero BlendTree
    ![HeroAnimator](Images/HeroMovement2.png)

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

**Fred Dang (Weapon/Magic Skills)**
* In conjunction with creating the animations and choosing the visuals for the weapon and magic skills, I coded their interaction with enemy mobs and data in WeaponController.cs, WeaponEventHandler.cs, MagicController.cs, Fireball.cs, Tornado.cs, Suck.cs, Rock.cs. For MagicSkills, I used the Command pattern for the magic skill input. 
    * WeaponController.cs handles pointing the sword at the direction of the mouse cursor and slashing. It has private data affiliated with it to determine cooldown for slashes. It also prevents the sword from moving with the mouse when slashing to avoid breaking realism (as you would not swing 360). It borrows some logic from a [YouTube video](https://www.youtube.com/watch?v=7vMHTUwtyNs) and [here](https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/) to make the mouse following possible. Citations are provided below and in my section in Animations and Visuals. 

    * WeaponEventHandler.cs is a helper script that alerts WeaponController.cs when it should be prevent the sword from following the mouse or when it should. The illusion here is that when the sword is not swung, it is always following the mouse, but since it is invisible, this cannot be perceived. However, when you do slash, the sword appears so it should not following the cursor while slashing. 

    *  MagicController.cs handles magic input from PlayerController.cs. It handles all the cooldowns affiliated with magic skills and spawns them into the game as well as sets its velocity to move toward where the mouse is aimed at. Again, this uses the same math logic as seen in WeaponController.cs. 

    * Fireball.cs, Tornado.cs, Rock.cs, Suck.cs handle the deletion of the the magic skills from the game when they have exceeded their duration length. Fireball is a projectile that disappears on impact, Rock spawns three rock projectiles that are slower but deal more damage, Tornado spawns one tornado projectile that can damage multiple targets, and Suck pulls slimes toward it. 

    * I also created a dash that increases the player's speed to escape mobs. 

    * I also modified SlimeController.cs to handle interactions with the skills such as applying damage to the slimes, deleting the projectiles on impact, or changing their following behavior as seen with Suck. 

* **Resources I Used**
    * [Create Melee Attack / Combat in Unity - P1 - Attack Animation
](https://www.youtube.com/watch?v=7vMHTUwtyNs)
    * [2D Melee in Unity Tutorial
](https://www.youtube.com/watch?v=giJKCl-GVrU)
    * [Make a 2D Ghost Effect: How to with Unity and C#
](https://www.youtube.com/watch?v=y982Gb00dho)
    * [TOP DOWN MOVEMENT in Unity!
](https://www.youtube.com/watch?v=whzomFgjT50)
    * [How to make an object follow the mouse in Unity (in 2D)](https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/)


# Sub-Roles

## Cross-Platform

**Describe the platforms you targeted for your game release. For each, describe the process and unique actions taken for each platform. What obstacles did you overcome? What was easier than expected?**

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing (@j-p-ecs)

The following portion has been written by @j-p-ecs (GitHub username):

During gameplay testing, I discovered many issues that needed improvement. When working on the physics, I discovered a glitch that allowed the player to clip through buildings by doing light taps to clip into the wall without the physics system ejecting the player back. This took a while to patch.

During testing, I also noticed a flaw with hitboxes that prevented the player from crossing over a bridge that was one tile wide. I fixed this by adjusting the size of the player hitbox, which enabled the player to cross over all bridges effortlessly. Furthermore, testing also allowed me to discover some issues with relation to bush and tree placement which caused traversing the map in some areas to be more complicated than we intended. This was fixed by removing some items from the map with consultation from @gzaets.

Part of my tasks also involved porting our logic from a Demo unity scene we were experimenting with to the actual game. Due to lack of availability from other team members, I had to figure out how they implemented the UI logic as well as their logic for purchasing buildings, and after that was figured out, I was able to port it to our actual game. This was an unfortunate event, which happened in part because of merging conflicts.

Some glitches I also found during testing included being able to purchase buildings more than once. I fixed this by ensuring that building item scripts were not unnecessarily attached to certain objects. Furthermore, testing also allowed me to uncover a glitch where some parts of the map were unable to be traversed.

Another less trivial bug that I discovered during testing was a glitch that enabled forgoing the purchase of one building, then walking to another building and choosing to purchase that, and both buildings would end up being purchased. I relayed the issue to other members of the team, who realized that by removing all event listeners before doing anything else when a collision occurs, the problem can be fixed.

I also discovered a bug that was preventing damage from affecting certain slime objects. Upon further investigation, I realized that original slime objects and slime object clones have different gameObject names. One just has the ordinary name, while another has the name with Clone appended to the end. By checking for both of these, I was able to resolve a bug where players would otherwise be tediously trying to use a sword against slime enemies with no effect.

During class testing, some people noticed issues that needed resolution. One involved a mysterious collider situated next to a building. @gzaets and I tried to fix it on the fly for the demonstration, but we were unable to pinpoint the issue immediately. I eventually determined where the issue was, and it was an errant 1 tile box collider located just above a building. By removing it, I was able to fix that bug and create a quality of life improvement for players.

Class testing also pointed us to flaws in our implementation with regards to movement during game over. In other words, movement would sometimes be disabled, but other times, it would be possible to break free from the movement. Additionally, it was discovered that income would continue to generate even after a game over had reached, and this was resolved accordingly after it was noticed.

**This part is written by Mohamed Ali Boutaleb (@mohalibou):**

For gameplay testing, I have done various different playthroughs, focusing on the game mechanics, user interface, and overall game performance. Since I was working on the user interface as my main role, I wanted to understand how the UI design influenced the gameplay experience and looking for areas to improve upon.

*UI Elements* - The player stats and building purchase pop-ups were designed to be clear and intuitive. For the most part, I feel as though this goal was accomplished. Everything is laid out in a simple manner.  However, during gameplay, I felt that it could be of benefit to add a minimap to the game. Unfortunately, due to time constraints, this was not possible. Although it is definitely something that could be added later on.

*Gameplay Balance* - We wanted to implement progression in a non-linear fashion. With that being said, that also meant we had to ensure that all the regions were challenging, but in a way that wasn't disproportionally difficult. This involved a lot of tweaking to the game mechanics, such as prices for building purchases, obtaining magic abilities, etc. 

*Technical Performance* - The game was able to run smoothly. There were no crashes or major bugs. I did run into a few technical glitches, such as improperly displayed stats and screen size related issues. However, the UI now works as expected.

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer (Fred Dang @fdang2000)

[Trailer Link](https://www.youtube.com/watch?v=81yyx-yEx38)

[Screenshots Link And Trailer File](https://drive.google.com/drive/folders/11SvAtqizQHyJuJR4-BfdkMQkD0snPDYe?usp=sharing)

* Trailer Choices 
    * For audio choice, I chose to use the Terraria music because I feel like it had a "8bit" feel to the music which matches the style of the game that we chose. 
    * I tried to keep a consistent theme through the trailer by having text followed by examples of it in the game. I feel like this is very direct and achieves the purpose of emphasizing what I want the audience to focus on throughout the trailer. 
    * For the trailer, I chose to preview the core aspects of our game: exploration, combat, and building purchasing. 
        * I chose exploration because of the time George put into this map and wanted to show players the beauty of it. 
        * I chose building purchasing because that is essentially what the player will do throughout the whole game. Players have the option to progress through the game by waiting passively for income from buildings to accumulate, engage in combat with slimes for additional gold, or both! I wanted to showcase these options that players have and let them decide what they want to do. 
        * I chose combat because I wanted to showcase the magic that players could use in conjunction with the default sword slash to give players some variety regarding what they can do in combat. I wanted to show the interactions that these skills had with the mobs to show that there can be some strategies employed, for instance, the "Suck" skill moves mobs toward it which gives players the chance to combo other magic skills toward it. 

* Screenshot Choices
    * For three of the screenshots, I wanted to showcase the world that we created because we could not go over all of it in the trailer. As such, I chose these four images to show the audience the world. 
    * I also wanted to show case other angles of abilities so I have screenshots where players use magic or slash. 




## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**