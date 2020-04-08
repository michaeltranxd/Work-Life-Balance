Level Design: We will have a realistic level design choice analogous to what they represent. The player’s home will resemble a home and the overworld Town you are in will also have a realistic layout of a real town.
Character Design: A generic Man/Woman will be implemented as the player model. NPC Models are not a big functional requirement for our game and there will not be many. If there are, they will also follow a generic boy, girl, man, woman, etc. design.

Physics: 
	3 forms of 3D physics
		Collider
		Hinged sign (home sign)
			A decoration to the house, also it can inform the player this is their house.
		Cloth 
			Having a cloth as your doorway is a thing in some households.
Three lights, three sounds, and three textures:
	Light
		Sun/Moon lighting
			We chose sun/moon lighting because it simulates real world lighting and gives the player the feeling of being in the real world
		Light around the house
			Since our player starts and ends within a house, we thought light around the house will make it more realistic
		Car lights
			Cars need to provide light especially at night and adding light to cars makes the game more similar to real life
Sounds
	Character encounters action (Waterdrop sound)
		The reason there needs to be a sound when an action is encountered is that it helps to have feedback given to the player. They will realize quickly that they’re attention is needed as well as knowing quickly that they have encountered an action as the waterdrop sound will be played every time an action is encountered.
	Character sleeps (music box lullaby)
		This sound effect is for feedback to the player, alerting that the player is in the sleep state. 
	Eat the cookie which was given by a random kid on the street (voice)
		The cookie was given by a kid on the street. This sound effect gives the player a feedback, informing the player that they just ate something.
Textures
	Home sign
		Displays “Home” for players to know where their house is.
	Flag
		The reason we chose the American Flag is because we’re in America, and most of the ideas and concepts will be more American.
	Medical bill
		Displayed on the wall and acts like a billboard describing that medical healthcare is expensive. This also reiterates the main goal: to not get readmitted into the hospital because that would lead to more medical bills
Least three AI techniques
	Car randomly spawning and pathfinding to a particular destination using Unity NavMesh- Michael
	Dog Patrol w/ interaction - Justin
		Dog patrols back and forth between to points. Once the Player has entered a close enough vicinity, the Dog halts and will stare at the player.
	NPC giving cookie - Spancer
		The kid will just walk around on the street. When the player gets in the range of 1 distance from the kid, a plane will pop up showing a message of “Timmy gave you a cookie”. The plane will disappear after 3 seconds, and Nutrition stat will increase 10 points.

The project must use at least three (or two) examples of Mecanim, one of which must be for a rigged character model   
	Rigged character model for the Player Character with walking and idle animations - Michael
	NPC walking around that gives the Player a cookie. - Spancer
	Dog that patrols two points with walk animation. When you are near the dog, he will stop and stare at you in idle. Player leaving the area will make the dog continue walk animation and patrol. - Justin