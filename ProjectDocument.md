# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**


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

*Player Stat Display* - On the top left of the screen, I implemented a UI panel that shows the player's stats. This includes health, stamina, intelligence, strength, and money. This information is critical for players as they navigate the island, strategize their growth, and make decisions on resource allocation and skill upgrades. [Image of the UI element.](https://imgur.com/a/l4uHYS5) [Code for the implementation.](example.com)

*Building Purchases Popups* - Whenever a player wants to make building purchases, there will be a pop-up UI that provides only the necessary details related to purchasing the building. These pop-ups were designed to be informative yet unobtrusive, ensuring that players can easily continue their gameplay after making their decisions. [Image of the UI element.](https://imgur.com/a/QpFgE2e) [Code for the implementation.](example.com)

*Game Over/Main Menu* - I also designed the game over menu, and main menu, providing players with options to exit or enter the game without disrupting the flow of the game. [Images of the UI elements.](https://imgur.com/a/8BAxONR) [Code for the implementation.](example.com)

## Movement/Physics (@j-p-ecs, Personal Name Hidden for Privacy Reasons)

The following has been written by @j-p-ecs (GitHub username):

Physics has been implemented in part manually and in part using Unity’s physics engine.

Unity’s physics engine is used to handle collisions in situations where trying to handle it manually would be nontrivial or otherwise extremely tedious, especially dealing with irregular polygons such as consecutive tiles of water. 

Building collision physics were, on the other hand, programmed manually and seek to optimize the player experience with no slowdowns resulting from rubbing against walls, etc. This is contrary to water tiles, which incur a slowdown if the player rubs against them because those rely solely on Unity’s physics engine.

Physics were primarily implemented by attaching Collider and Rigidbody components to the desired objects. In cases where we want to control the behavior more precisely, we added tags to some objects to determine whether special behavior should occur. For example, cave doors have a tag that is checked for during a collision and if the tag matches, it will determine which portion of the dungeon the player should be transported to.

Another aspect of physics which I incorporated was knockback with regards to collision with slime objects. My approach here basically involved setting a knockback animation time, which I calibrated to be about a third of a second. During that third of a second, if a collision occurred, and depending on how forceful the collision is, the slime would move back at some multiple of the collision speed. In our testing, it was a decent and smooth implementation. It may not be absolutely perfect by all standards, but I am decently satisfied with the result.

Regarding collisions with cliffs, I had challenges figuring out how to implement this with Unity without defacing a beautiful map design. The strategy I decided to go with was duplicating the design with another attachment, then setting the layer to be a very low number so that the layer would not be seen during gameplay (i.e. layer -99). I then attached a tilemap collider to that component and removed the appropriate tiles from that layer as necessary. If this was not done, this would cause the player to be unable to traverse through significant portions of the map. I did investigate working with Edge Colliders, but I was unable to figure out how to get those working.

The following are not directly related to physics but were implemented by @j-p-ecs:

I utilized the base code that was used to implement building purchases (which was done by other teammates, including @payday2021) to implement magic combat ability purchases. This was a relatively trivial implementation that involved creating a script to display the appropriate purchasing UI upon colliding with an NPC in the overworld. After a purchase has occurred with the NPC, then I use the Destroy function to remove the object.

I also created the game over screen, which uses the template created by @mohalibou for the title screen. The game over screen is equipped with the proper transitions. In addition, when a game over has occurred, I made sure to disable character movement. As a result, pressing up, down, left, or right will result in no changes to the player position.

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

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


## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**
