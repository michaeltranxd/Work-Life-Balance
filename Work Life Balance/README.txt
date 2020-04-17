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

UI Design
	The changes we made reflected the principles in the Interface Design lecture. We made sure colors weren't too saturated (and additionally texture smoothness made sense).
	We added a map and minimap for the player to help navigate themself around the map. We also tried to minimize the unnecessary times a menu would pop up.
	A lot of clarity issues were addressed (including color as mentioned above) with how Time was viewed and the Status Bars.
	A lot of sound effects to help indicate feedback of menu navigation to the player.

Sound Design
	Sound Effects: We spoke extensively about which sounds fit the town of what action they were attached to. For example, something loud and cheery, though exciting to hear, may not be appropriate for constantly accessed actions like
	hovering over a menu button. And so we would go towards a more pleasant short sound.

	Background Music/Sounds: In regards to keeping a realistic level design, we found that music may not fit as a background audio for the whole day. We added natural sounds around that will change between night and day.
	Additionally you hear frogs and still water, but only when near the pond in the park.

UI Design
	Justin
		- UI Principle Broken: Clarity - BUILDING COLORS: The buildings follow a saturated color palette. We need to find something easier on the eyes but still appropriately look like colors a building may take. 
		- How it was fixed: Used an online color palette database to have the buildings have a palette that makes more sense and is easier to look at.

		- UI Principle Broken: Minimizing Error Possibilities - DOING PLAYER TASKS: Options (i.e. Gym interaction) should not even come up when there is not enough time to do them at night.
		- How it was fixed: Upon time reaching 8:00PM, box colliders are disabled until the next day has started.

		- UI Principle Broken: Clarity - GROUND COLORS: Too bright green saturation. May hurt the eyes with prolonged exposure.
		- How it was fixed: Changed the color to a green that was easier to look at but still looked appropriately like grass.


	Spancer
		- UI Principle Broken: Clarity - TIME: Saturated red, hard to look at for a constantly viewed status. Red is not an appropriate color as it signifies danger/warning. Can keep red color for when dangerously low on time.
		- How it was fixed: Changed the color to gray it’s easier on the eyes

		- UI Principle Broken: Clarity - GUI WINDOW BARS: Some colors are saturated for stat bars
		- How it was fixed: Change the color of the stats bars so it’s easier to look at. Also, changed the layout of the colors

		- UI Principle Broken: Clarity - MESSAGE: Too small. Hard to easily read.
		- How it was fixed: Change the font size so it matches with other texts.


	Michael
		- UI Principle Broken: Providing Feedback - PLAYER FOOTSTEPS: There are no footsteps to indicate that the player is walking.
		- How it was fixed: Added footstep sound effects so that the game reacts to the player’s movement and then the player will feel more immersed in our game world.

		- UI Principle Broken: Minimizing Error Possibilities - MOUSE STILL ON SCREEN: Due to a bug, the mouse stays on screen when you walk away to cancel an interaction. There is no need to have the mouse out when there are no buttons to click.
		- How it was fixed: We fixed the bug in a script by deactivating the cursor when the player leaves the collider

		- UI Principle Broken: Learnability - MAP NAVIGATION: It is hard for the user to know and remember current location. There is also no way to know what is interactable.
		- How it was fixed: We added a minimap + map with icons that represent that important buildings that the player can interact with.

		- UI Principle Broken: Providing Feedback - DIALOGUE BUTTONS: Hover color does not pop out enough
		- How it was fixed: Changed the button highlighted color to give visual feedback to the player and panel to house the buttons


Sound Design Critique
	GTA 5 (Commercial game) - Around 20+ sound effects
		Justin
		Punching Sound Effects
		- Where and/or When?: Anywhere with other people
		- When the player throws a punch and it hits someone, you hear a good thwack which makes sense. Punches can be pretty loud in real life, but I believe it’s purposefully made to come out loudly in-game to indicate to the player that a good punch was received by the target.
		- Among other sound effects in the background the punch is clearly heard, which is important as mentioned in the earlier point. As far as being repetitive, you do hear it lots 

		Engine starting up (+loud motor)
		- Where and/or When?: When the player enters a ground vehicle that was not on previously.
		- When getting into a new car that isn’t on, you hear the player you start it up and hear a good engine rumble. In the video, the player entered an old car, so I think in general it was loud because of that fact. A nice detail I think. This car’s loud motor is is heard throughout driving as well, over many other sounds. While it may be annoying to some, I consider it appropriate for how old the car looked (and maybe what type of car it is. I’m no car expert). 
		
		Grunting when doing effort or getting hurt 
		- Where and/or When?: Whenever a player gets hurt or does some sort of effort like jumping.
		- The grunting is actually subtly added to the environment and quiet. You hear it just enough to in some ways humanize the character to the person playing the game. It does its part in immersing you that this is a person in a ‘real world’, though the actions made might not, such as purposely ragdolling to get hurt. There are several different ‘effort’ sounds so it doesn’t get so repetitive, especially since one might get hurt a lot in this type of game.
		

		Spancer
		Nearby conversation
		- Where and/or When?: Where there are people nearby.
		- The sound is subtly added to the game, it’s not the same every time, just like in the real world you wouldn’t hear the same conversation from different people over and over again. The sound provides feedback for the player that there are people nearby by. The volume of the sound is depending on the distance between the player and the conversation which makes the game more lively.
		
		Car open door/close sound
		- Where and/or When?: Whenever the player opens or closes a car door.
		- The volume of the sound is low but enough for the player to hear. It provides feedback for the player that the car door is opened/closed. This sound helps the player immerse in the game, and makes the game feel more real.
		
		Kicking metal sound
		- Where and/or When?:  When the player kicks metal
		- The sound varies based on what metal the player kicks, just like in the real world, not all kicking metal sound will be the same. I think this sound is very subtle. It provides feedback for the player that they just kicked mental, on the other hand, it makes the game more realistic.
		

		Michael
		Opening and displaying UI on screen
		- Where and/or When?: The sound plays whenever the player wants to display a UI element on the screen.
		- The sound is subtle, but provides enough feedback for the player that a UI element will be displaying. There are two different sounds when opening and closing the UI so that prevents the sound to be repetitive. This sound effect is not too loud compared to the other sounds occurring in the game. It sounds like the sound effect was made using a switch. When the UI appears, the switch is flipped on and then flipped off when the UI disappears.
		
		Car honking sound effect
		- Where and/or When?: The sound plays whenever the player is driving in front of other cars at high speeds, cutting them off.
		- I believe the sound is not too strong in the context that it occured because it depicts the real life situation of people honking at other drivers. This sound effect seems to vary in pitches so it does not become repetitive. I would think that the sound designer team used different cars for the varying pitches of honking sounds.
		
		Footsteps sound effect		
		- Where and/or When?: Whenever the player moves around, we can hear footsteps
		- The sound effects for footsteps vary based on which foot is stepping and builds upon each other. It sounds as if the second footstep completes the first footstep. I believe this sound effect helps the player immerse in the game and feel as if they are the character walking around and interacting with the environment. It seems that the footsteps are drowned out by other sounds, but I think that is to restrict the repetition.
		

************************************************************************
Three background music/sounds:
	Spancer - Morning Sounds: Birds sound, wind sound etc. The volume of the sound is low and it varies in pitch so it’s not annoying for the player after hearing it over and over again.
		
	Justin - Evening Sounds: summer night sounds. Similar as the morning sounds but no birds sound since birds don’t come out at night.
		
	Michael - Frog Sounds at Pond in the Park: Wave sound, and frog sound. When the player gets to the range of the pond in the park, they will hear this sound. And the closer they get the louder the sound is just like in the real world which makes the game more lively.
		
		
Sound Effects
	Action Dialog
		Accepted:
			- Menu Opens: The pop sound effect is for whenever the player interacts with the buildings where they can do certain events. It provides feedback for the player that there are options of events listed and they can choose from it.
			- Button Hover(Click): This sound effect is played when the player hovers over an event. It’s added an additional feedback for the player that they are about to choose the events button they are hovering over.
			- Button Click(Confirm Click sound effect): Confirm click sound effect is played when the player clicks on a certain event button. It’s informing the player that they have chosen to do an event.
		
		Rejected: 
			- Mouse Clicking sound: The sound effect of mouse clicking is too simple. It’s not enough feedback for the player that they just selected something, so we replaced it by confirming the click sound effect.
			

	Sleep
		Accepted:
			- Text Proceeds(Typewriter): The typewriter sound effect is a clack whenever a new character is ‘written’ on the page. It keeps a good tone of the seriousness of this game while being enjoyable to listen to.
			- Speed Up Sound: This sound effect plays when the player clicks the button to skip the typing and show full text in the recap of the day. It provides feedback for the player that the button is clicked and the text is displayed.
		
		Rejected: 
			- Lullaby sound when in the sleep event. The lullaby did not match the tone of our new ‘recap’ event when the player goes to sleep.
			- Snoring sounds in the sleep event. The snoring sound effects we found and pitched to each other did not match our character and/or hurt our ears.
		
		
	Timmy (Random Events)
		Accepted: 
			- Enjoying the food(Boy voice):  This sound effect gives the player feedback, informing the player that they just ate a cookie that’s given by the random NPC. While keeping the player more “human”.
			- Message Showing: This sound effect is used to alert the player there is a message displayed on the screen since the message will disappear after 3 seconds, it is important that the player knows when it shows up.
		Rejected: 
			- Eating sound before the boy's voice. There is already a sound effect of a boy’s voice, after adding this sound it’s a little too much going on for just one event.
	
