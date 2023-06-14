# Core Concept #

## Provide a synopsis for your game. ## 

2D RPG game where the goal is to become a ruler of the island. You start off with very limited resources, and choose an approach to generate money/power in order to improve your title or standing in the society. The player creates a character, assigns their starting stats for Stamina, Strength, and Intelligence, and improves them over the course of the game. 
The island will be divided into different regions, each having its own unique challenges, NPCs, resources, and opportunities for the player to explore. Players can choose from multiple approaches to generate money and power, shaping their path to rulership. Players can engage in trade, participate in thrilling battles and small adventures, or even become early businessmen to generate passive income. The world will be filled with some NPCs that might have small questlines available for the player to take on. These quests can lead to rewards for the player or potential power/wealth growth opportunities. 
As players play and generate some wealth and power, they will be able to invest their resources in upgrading their character’s abilities and unlocking new skills. Stamina is the character’s overall endurance and energy in physical tasks. Strength is the combat prowess and carry weight determiner. Intelligence is used for diplomacy, job opportunities, or even dwelling into the world of Arcana. 


## Describe the core gameplay system of your game. ##

### Movement System ###

Precise 2D player controls, with character lock tracking camera.
Movement in directions up, down, left, and right, diagonally similar to the movement system in games like Stardew Valley or Link’s Awakening. 
Speed and movement abilities may be influenced by the player’s Stamina statistic. 

### Economy system ###

The economy system is a core part of the game which players interact with to generate wealth, power, and upgrade their standing in the society.
Core system that allows players to generate wealth, power, and skill upgrades.


### Building Progression ###

As players earn more resources and wealth, they can invest in purchasing and upgrading various buildings.

### Stat Improvement: ###
Offer opportunities for improving the player's stats, such as libraries or training centers.

## Stats System ##
Stats system gives the player the ability to do a task for a longer amount of time. There are 3 stats in the game: stamina, intelligence, strength. These stats will determine the tasks that are available for you to do. The higher the stats, the better the job.

### Stamina: ###
Character's endurance and energy for physical tasks. It influences how long the player can sprint while in combat.

### Strength: ###
Character's combat abilities and attack power. It could influence which jobs the character can take, how effective they are in combat, and how much they can harvest from resource-gathering activities.

### Intelligence: ###
Character's abilities in diplomacy, learning, and arcane arts. It influence the ability to learn new skills or magic.

## Explain your implementation plan to develop your core gameplay system. ##
PlayerController: This class will be responsible for the player character's movement and interaction with the game world. This includes gathering resources, trading, and combat. This class will use Unity's Rigidbody2D and Collider2D components to handle movement and collision detection, respectively. The controller will communicate with other components such as the StatSystem and InventorySystem to determine the character's capabilities.

StatSystem: This class will manage the player's stats - Stamina, Strength, and Intelligence. It will provide methods to increase and decrease these stats and provide their current values to other systems in the game. This class will also play a key role in governing the actions the player can perform, and their success rate in tasks.

InventorySystem: This class will manage the player's inventory of items. It will provide methods to add and remove items from the inventory, check if an item is in the inventory, and return the quantity of a specific item in the inventory. It will also communicate with the UI to update the inventory display.

Each scene in the game will represent a unique area of the island. The main player character and other persistent data will be kept in a persisting game manager object that does not unload between scene changes. The scenes include Main Island Outside (like the commonwealth that unites all the other areas together), and caves (where the player can go on monster hunting adventures or mining). 

Assets needed for the game will include 2D sprites for the player character, NPCs, resources, and the game world. Sound effects and music tracks will also be necessary.

Each of these systems will be developed and tested separately before they are integrated together. Once all the core systems are implemented and working together, we'll move on to creating the various quests, locations, and other content for the game. We will also ensure that we have a flexible system to add, modify or balance gameplay elements as we get feedback from testing.

## Why do you want to make this game? ##
We wanted to go down the route of creating a game with the objective of ascending to the status of an island ruler as it embodies our passion for immersive world-building, strategic gameplay, and engaging narratives. We felt that this was an interesting concept due to its multifaceted nature that allows for a diverse array of strategies to achieve power and wealth, since this will allow unique challenges to be presented to the player. 
The choice to develop this idea came from our desire to combine classic RPG elements with our own innovative spin to the genre. We saw the opportunity to add a fresh spin to the 2D RPG genre by introducing a layer of socio-political dynamics on top of a traditional RPG game. We strongly believe that this will allow for an intricate and engaging game that stays true to its roots while still providing our own unique ideas. 
With our ideas, we hope that this will allow for players to carve their own narrative within the world we created.
Explain your game and the major systems it is composed of in terms of other games and genres.
Many of the systems that are integrated into this game come from most 2D RPG games. Some of these major systems include but are not limited to:
Health System
This will be taken care of in a PlayerInfo class
Separate from PlayerController movement class
Inventory System
This will be built with an overall InventoryyManager.cs
Items that can be part of  an Inventory will have an InventoryItem interface attached to them
Action Input System (to interact with the 2D world)
Part of PlayerController movement system
Action Button will will only have an event occur when player is in vicinity of an ActionableItem interfaced gameobject (an inventory item is an example of this in order to pick up the gameobject and add it to the player’s inventory)
Stats System (Stamina, Strength, Intelligence)
This will be built with an overarching StatsManager.cs
StatsManager will listen to changes regarding player stats from outside scripts and update accordingly
The primary objective of this game is to conquer all parts of the island. Players do this by engaging in quests and increasing stats, money, and resources.
List your non-core gameplay systems and features.
Non-core gameplay systems include the additional tasks that players can do during the day to kill time. The main progression in this game can be achieved through simply waiting. However, if the user wants to maximize their efficiency, then they can choose to do “side” quests too. These are daily tasks that are re-doable every day that will generate some resources or stat points. If we have time at the end, we can implement customization for the character but our plan for now is to use a default generic character. 

## Your Team. ##
### Member: [Jared Pugh, jpugh@ucdavis.edu] ###
Role: Movement and physics (confidence rating: 4). I have experience working with various physics systems, although these are primarily with 2D platformer games. I have not dived deeply into physics systems in games where the movement more closely resembles gameplay from Stardew Valley and The Legend of Zelda: A Link to the Past. This knowledge will help in building a customizable physics system that players are expected to enjoy.

Subrole: Gameplay testing (confidence rating: 3). As I have found glitches in retro video games that were previously undiscovered, I believe I am suitable for this role. This knowledge will help to ensure that our finished product comes with limited defects.

Hope to learn and accomplish: I am hoping to gain greater familiarity with Unity in working on this project. I have experience programming games with other libraries and tools (such as GTK for C++), but this is expected to be much more practical for industry purposes. This is also a solid way of branching out into a type of game genre I am less familiar with and learning to understand the internals of non-platformer style games.

Anticipated challenges: I am not as familiar with Unity as some others, and I have a tendency to try to program stuff manually even when Unity provides a lot of features to guide less experienced users. I will have to be cognizant of this while working on this project.

### Member: [Mohamed Ali Boutaleb, maboutaleb@ucdavis.edu] ###
Role, confidence, explanation: My role is the UI. I give myself a 4. I have worked with user interfaces before, more specifically with iOS applications, so I know my way around creating a good user experience. I have not worked with games before though, so I don’t know how exactly those skills will translate to a video game, although I am confident I can figure it out.

Subrole, confidence, explanation: My subrole is gameplay testing. I give myself a 4. I chose this role so that I can enhance my understanding of the user’s perspective. I believe that actively being involved with the gameplay testing will help inform my UI design process, making it more attuned to the player needs and responses.

Hope to learn or accomplish: I aim to design a UI that will seamlessly integrate with the game’s mechanics and narrative, enhancing player engagement and satisfaction. Through gameplay testing, I also aspire to deepen my understanding of player behavior and game mechanics, which will in turn help with my UI design work.

Anticipated challenges: I would imagine that a challenge I will end up facing has to do with keeping a balance between aesthetic appeal and functional simplicity in my UI designs. When it comes to UI design, it is important to keep the UI engaging, but also easy to navigate. Since RPGs can be complex, it can be difficult to ensure that all systems are effectively represented in the UI in a simple and intuitive way. 

### Member: [Georgy Zaets, gzaets@ucdavis.edu] ###
Role – Producer, 4. I believe I would be a great fit for this role based on my experience, skills, and passion for game dev. I have a strong background in project management; I am skilled at establishing clear timelines, identifying critical issues, and good at managing available resources and time for the team. I understand the importance of effective communication and collaboration within the team to integrate their work smoothly into the project. I have a good understanding of git-based version control software, which is what we decided to use as a team. I understand the importance of quality assurance during the development of the game, and will actively oversee the testing and bug tracking in order to ensure that any issues are addressed and resolved. As a team lead, I am great at fostering a positive, welcoming, and collaborative work environment that encourages innovation and creativity for team members. 
	
Subrole – Game Feel, 4, Working for game feel means creating an immersive and engaging game experience for the players of our game. I have a good understanding of what makes games enjoyable and captivating due to the amount of games I played and the amount of time I spent playing them. I am able to empathize and put myself in the shoes of the player to make sure that they are having fun while playing. I have a keen eye for detail, understanding how small elements can contribute to the overall feel of the game. From visual effects to gameplay mechanics, I pay attention to every aspect of the game to ensure that it aligns with the desired experience. I have experience with the idea of balancing challenges and rewards in games. I played and hosted plenty of tabletop RPG games, and I can easily adapt my stories and encounters dynamically based on the performance/needs of my players. Picking the role of the Producer also helps me with working on Game Feel because both require plenty of team collaboration with the members. Given the nature of the game concept we got going so far, my enthusiasm and knowledge of RPGs would be a significant asset. I understand the tropes and mechanics that make RPGs enjoyable and can leverage that knowledge to create a game feel that resonates with RPG fans.

Hope to learn or accomplish – I hope to gain experience and skills in managing a game dev project from start to finish. I aim to use my skills in RPG mechanics and game feel to create a new and captivating video game. I would also like to further develop my quality assurance skills and see how they can help with the presentation of the final product. I want to make a game that I actually would want to play myself.


Anticipated challenges – I think balancing the scope will be an issue since we are so limited on time for the project. That further goes to another issue of creating an immersive experience that captivates players. Managing different team dynamics might prove to be very challenging. 

### Member: [Fred Dang, fdang@ucdavis.edu] ###
Role - Animation and Visuals. I am somewhat confident in my abilities to create visuals or animations pertaining to the character and NPCs that are needed. This is a new area for me and I am confident that I can meet the necessary goals for this project. 

Subrole - Press Kit and Trailer. I am confident in this area because I have created videos in the past and I believe that I do have the skillset to showcase what our game will be about. 

Hope to learn or accomplish - I do hope to learn more about creating animations through this assignment since I am responsible for creating animations such as player movement or attacking. In addition to that, I will have to create some of the UI so it will be an interesting experience. 

Anticipated challenges - I have some slight experience with animations through ROBLOX but nothing to the point where I can create quality animations. I am hoping to create better animations through this project. In addition to that, I am not really an artistic person so it will be challenging for me to create a UI for this assignment but it will be done. 

### Member: [Navjot Singh, navsing@ucdavis.edu] ###
Role - Game Logic. Confidence Level 4/5. I am mostly confident in my abilities to develop sound game logic for the game due to my past experience in developing games from scratch. I have come across a plethora of development hurdles when it comes to game logic,  and am now able to quickly recognize sneaky faults or errors in game logic.

Subrole - Narrative Design. Confidence Level: 3/5. I am moderately confident in my abilities to write a compelling narrative for the game. Much of my game development experience stems from games driven primarily by a casual game loop. Adventure RPG games like we are developing tend to be heavier in narrative, so I hope to practice my skills when it comes to this area of development.

Hope to learn or accomplish - I hope to learn more about developing compelling narrative for RPG games. I believe a good story can set a game apart from other similar games in the genre. I hope to practice how to develop a game together in a team, as all of the games I have developed have been entirely on my own.

Anticipated challenges - I expect to face challenges in regards to coordinating development aspects with the rest of the team. It is important to communicate early on what each member of the team will be working on and when in order to ensure we meet the project deadline. More importantly, we want to make sure that everyone in the team shares the same vision for the game so that we are working towards a common outcome!

### Member: [Dylan Sun, dfsun@ucdavis.edu] ###
Role - Input - I have past experience working with Xcode and iOS, and a big part of working on iOS projects came with user input (sliders, buttons, toggles, etc.), and I believe that my experience working with different kinds of input will transition well to Unity development. Although this course is my first time working with Unity (and even creating games), I have the confidence to take on the learning challenges while not being afraid to learn new concepts and ask for help when necessary. Overall my confidence level would be around a 3.5/5. As stated above, I do have moderate experience when it comes to working with Input, however I have never applied those concepts to Unity. 

Subrole - Audio - My confidence level for the Audio Subrole to be a ⅗. I do not anticipate the audio assetss to be difficult to implement at all. However, the main difficulty would come from choosing audio that properly fits with the feel of our game (will the audio feel pleasing and be an important contribution for the game). That being said, I plan to confirm audio assets with all my team members before adding them to our game.
 
The main thing I hope to learn is how to work with multiple different parts of the game that are out of my scope (IE other main/sub roles that are not mines) and be able to properly integrate my own scope of the project. I also hope to become a better communicator and team player, making sure that I am always keeping myself up to date with the project and keeping my team members up to date about my role. Lastly, I hope to create a pleasing and FUN first ever game. 

The biggest challenge I will anticipate will be keeping track of all the moving pieces of the project and properly integrating my own role. I think it is crucial that all members of the team are on the same page, because even one small mistake could drown the entire project. Other challenges I anticipate would be finding the audio assets that best fit the game and giving realistic, feel good input by the player. 
Scheduling
Provide a timeline for completing your project.
Have each team member break down their major tasks into speculative durations and start dates. Do the same for game-wide tasks. As this is an initial plan, its main goal is to allow your team to think through the steps of developing your game. A Gantt chart would serve you well. 

### Fred - ###
5/20 - Compile a list of animations and visuals that would be required for the game. These will include all the actions that the character does, or UI pertaining to the core gameplay. 
5/27 - Create all animations relating to the character such as movement or combat. Hopefully, create the UI. 
5/29 - Hard deadline to have all the required UI and animations required for the project. 

### Jared - ###
5/20 - Create classes relating to movement and physics.
5/21 - Investigate Unity physics system to determine usefulness.
Incrementally implement different movement and physics based on player state (possibly something with a state pattern) and command pattern (rehash of exercise 1).
Incrementally test features (completing tasks associated with subrole for the project).
5/29 - Hard deadline for components as well as testing own features.

### Dylan - ###
By 5/22 - create a list of all possible inputs that the player can use
Starting 5/28 - animations and visuals should be finished, I’ll be able to begin integrating the list of inputs into actual game code, using the animations and visuals scripts 
6/1 - input should be completed, will begin looking for audio assets that properly fits events and input that occur within the game 

### Mohamed - ###
5/20 - Create UI mockups and experiment with different ideas to figure out what would look good while being intuitive for the user.
5/25 - Ideally be done with implementing all the UI elements needed for the game.
5/29 - All UI elements are finalized and production-ready.

### Nav - ###
5/22 - Draft game logic design document
5/28 - Draft game narrative outline
5/29 - Start Incorporating logic and narrative into the game

### Georgy – ###
5/21 	–	Add the first map using acquired assets for the main game island where the team can develop the game. 
5/23 	–	Work on adding key buildings for the game that can be used for passive income, stat improvements, or power generation. Start working on cave scenes if time allows
5/28  	–	Work on adding side activities for wealth generation, such as monster hunting, farming, passive income. Adding NPC movement around the map. Finishing the inventory system.
6/1	–	Stat system incorporating into the job field. Getting jobs based on a player's stats. Adding buildings that improve player’s stats.

## How would your game be if everything went as planned? ##
We expect the game to be an immersive roleplay experience where the user can become a ruler of their respective area. We want the user to work toward this goal by slowly building up his town to provide him or her with resources necessary to get other buildings. While the player is waiting for these buildings to generate the required resources, they will also have the option to train themselves in order to do mini-games that would give them resources more efficiently. As the player progresses, it will be easier to obtain resources, leading to faster progression. 
What would the results be if you lost significant time (e.g. you lost two weeks due to unforeseen circumstances)?
Additionally, some features that we originally intended to incorporate may not be present in the product if time is lost. Examples could include aspects of game physics that are based on proposed gameplay systems such as the stats system or combat. However, we believe it is unlikely that this would happen.
If your progress is faster than expected, how would your game change?
There is potential to expand on the synopsis if progress is faster than expected; however, this would come with ensuring that the underlying storyline is still coherent. Expanding the scope of various aspects of the game (for example, the overworld), would also not be out of the question. For instance, we can expand on combat should we have time for that and it will function as an additional task that players could do during the day to kill time. 
