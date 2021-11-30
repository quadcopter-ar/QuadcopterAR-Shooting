
# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**
As most people might have known, VR is the future of games; with wearing convenient equipments, such as Oculus headset and controllers, players can be granted with wonderful graphic environment and close-to-reality game feel. In this game, players can sit in the spaceship (quadcopter) and start their journey in any reality environment by controlling the quadcopter. 

This project is developed with the framework introduced by professor Nelson Max and his team in UC Davis. For development, we mainly use C# and unity game engine for development. After the game is developed, it is tested in the lab with the quadcopter, oculus headset/controller, unity version 2020.1.15f1 on Windows system, and other hardware/software to support the connection among equipments.


## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**


**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

The primary view of the game puts the player in the perspective of a quadcopter pilot in the cock pit. The player will see the control console in front of them as well as a health bar that shows the remaining health of the 'spaceship'.

Once the game starts, the health bar gradually decreases by time, and the player assumes full control of the spaceship's movement and weapons systems. The goal of the game is to destroy the enemy target using the spaceship's missiles before the ship's health bar completely depletes.

**Main Controls:**

The main control scheme of the game is mapped out to the Xbox 360 controller or any other controller Windows systems can recoginze and map as such.

- Movement: In order to move the space ship around, the player can use the controller's left joystick. Tilting the stick up and down will move the ship forward and backward, respectively, while tilting it sideways will make the ship strafe left/right
- Camera: As the game is designed for VR settings, it uses the VR headset to control the camera; by looking at specific directions, the user can aim the main camera and the crosshairs of the missile launcher. The user's field of view is limited by the ship's movement such that it emulates a pilot's cockpit perspective.
- Weapons: The `jump` button is mapped to the main missile launching mechanic. For an Xbox controller, this button corresponds to the Y button. Pressing the button will launch one missile directly in the direction the player's camera is facing.
# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## User Interface

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
In this game, the user interface mainly consists of the following items:
- some fbx objects developed by and imported from blender, such as the spaceship 3d model interior (we call it as **console**) and the missile.
- a gameObject who is the **parent** of the console (because we need to rotate and position the console correctly, so it can sit in front of the main camera, and let players to feel they are sitting in the spaceship under VR view)
- a UI text that indicates the power percentage left for the spaceship. Once the power goes down to zero and the player hasn't won the game, the UI text will be changed to **Fail** to prompt the player.

The following are the relevant scripts:
- `AbstractCameraController.cs` and `QuadcopterCameraController.cs`: for some addition control on console position and some basic work
- `CrossHair.cs`: for controlling the crosshair
- `HealthBar.cs`: for controlling the health bar and UI text
- `BasicMovement.cs`: for controlling the quadcopter in the game by oculus controller.
- `NetworkController.cs`: A network controller for photon, it calls photon api to establish photon's multiplayer network. When multiplayer network are established and players enter into the game room, it will print out debug info to ensure network is established.
- `JoystickControl.cs`: a script to control the joystick prefab in the game. It basically use Lerp for linear interpolation of the rotation of joystick prefab. User can set the rotation strength by changing the stepSize variable in unity and leftStickTrueRightStickFalse variable to select if the prefab is for left joy stick or right joystick.
- `MissileLauncher.cs`: a script used for missile launching motion. It detects oculus controller and fires a missile when user press "fire" button. It uses `NormalModeMissileMovement.cs` as default firing mode of the missile in the game. Users can define different speeds and effects of the missile by creating and deriving a similar script and enable it in `MissileLauncher.cs` from `AbstractCameraController.cs`.
- `NormalModeMissileMovement.cs`: a script that defines the speed and effect of the missile fired by normal mode in the game. Currently, this is the only firing mode in the game.
- `AbstractCameraController.cs`: a script that defines the abstract class for missile movement modes in the game.
- `Pun2RoomManager.cs`: a script used for establishing a game room manager using Pun2 Engine.
- `CreateCurvedPlane.cs`: a script to project video from the camera on the quadcopter in the real-life to the game. For more info, please contact Jim.
- `CurvedPlanePositioner.cs`: a script to position the above curve plane. For more info, please contact Jim.
- `PhotonPlayerManager.cs`: a script used for player management by Pun2 Engine Api.
- `Pun2GameLobby.cs`: a script to create a Pun2 UI game lobby and allows users to join the game room.
- `_ync.cs`: a utility script to create connection for the sync of the quadcopter in real-life and the game. For more info, please contact Jim.
- `server.cs`: a server for connecting game and the quadcopter in real-life. For more info, please contact Jim.


The following are the relevant fbx objects (unfortunately, due to the fact that unity doesn't support some of the materials I used in blender, so it is not as `fancy` as it looks in blender):
- `console` : spaceship fbx object imported from blender after the 3d model is developed and render


![console image](https://github.com/quadcopter-ar/QuadcopterAR-Shooting/blob/master/images/console.png?raw=true)

```



```

- `missile`: missile fbx object imported from blender after the 3d model is developed and render

![console image](https://github.com/quadcopter-ar/QuadcopterAR-Shooting/blob/master/images/missile.png?raw=true)

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?** az

*Missile Launching* - The main mechanism of launching missiles exists through the `MissileLauncher.cs` script. The script is bound to the main console Game Object, and creates new instances of the Missile Prefab every time it recieves a Jump command. The script then assigns the missiles with proper velocity and rotation relative to the main console to make sure they behave as expected when launched. This script follows the command design pattern. [link here]()

*Spaceship Movement* - The main mechanism of spaceship movement is established by the framework scripts with a few of adjustments:
- `BasicMovement.cs`: for basic movement control, using xbox style control
- `_ync.cs` and `CreateCurvedPlane.cs`: for quadcopter position and system communication

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.** az

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**
- this game is developed with unity version 2020.1.15f1
- this game is developed under windows 10 system
- this game uses oculus input system (with VR and quadcopter)/XBOX controller(without VR and quadcopter)
- this game will drive a quadcopter - 3drsolo
- Wifi
- other software/hardware required for the framework

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**
The game logic is simple, it is a simple game that let player to fly and launch missile to kill enemy. 
This game mainly consists of the following states:
- `start` : audio and UI text will introduce the story background and what the players need to achieve to win the game
- `fighting`: players will need to kill the enemy before the power of the spaceship goes down to zero
- `fail`: once the power of the spaceship goes to zero and the player hasn't killed all the enemy yet, the game will enter into fail state and triggers `Application.Quit()`.
- `sucess`: once the player kill all the enemy before the power of the spaceship goes down to zero, the game enters to sucess state and triggers `Application.Quit()`.

# Sub-Roles

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.**

**Sound effects and assets:**

- `MissileLaunch.wav`:  Originally named "Comedy/cartoon missile launch". Acquired from ZapSplat.com - Standard License
- `ThemeMusic.mp3`: Originally named "ON THE RUN TRAILER". Authored by XHALE303. Acquired from freesound.org; Attribution Noncommercial License
- `Alert.wav`: Originally named "Science fiction alarm or siren 1". Acquired from ZapSplat.com - Standard License

**Implementation:**
Our audio system is based on player actions and game progress. We have two sources of music being used; the main theme music which plays from the start of the game until the end, and the missile launch effect, which plays once every time the player launches a missile.

In order to play both these pieces, we added two `Audio Sources` to the game components; one to the `console` object, and the other to the `Main Camera`.

The `console` audio source Plays `ThemeMusic.mp3` on start. This audio file is a result of looping the source `ON THE RUN TRAILER` file and fading in the `Alert.wav` to the last 15 seconds. Thus, the game launches with the intense music as intended, and the sirens would play if the player has 15 seconds to go before their health depletes.

We use the `Main Camera` as our missile launcher, as their directions must match, and thus we attached the `MissileLaunch.wav'` file to its audio source and used the `PlayOneShot()` function to play the clip every time the `fire()` function is called.

**Sound Style:**
Our main theme music is `ThemeMusic.mp3` plays for the duration of the gameplay in order to give a sense of excitement and intensity reflecting our status as a ship losing power. As for player actions, `MissileLaunch.wav` is played whenever the player launces a missile, respectively. This is done to immerse the player into the setting and gameplay as well as provide feedback for their actions. As the game progresses into the low health portion, the main music has the `Alert.wav`  fade into the main music until the game ends one way or the other in order to instill a sense of danger and dread matching our current low health. Lastly, one future addition we agreed upon but hadn't implemented is that on player victory, we play `HeroicMusic.wav`, a triumphant tone that gives the player a sense of accomplishment for their victory.

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**
We tested and debugged in the lab, the initial test went well and players who tested our game thinks that this game is fun, but we also notice that wifi speed will affect players' game feel due to the fact that we use wifi to establish the connection between the quadcopter (and its camera), unity on the windows, and the oculus headset/controllers. However, we haven't had a chance to retry the updated game due to restricted hours after Covid and limited time on final week.

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.**

There is moderate narrative present throughout our game via game components alone; the control console signals that we are in a spaceship, the crosshairs show that we have a missile system ready to use, and design of the enemey spaceship shows they are a target. Moreover, the 'power down' down theme is featured through the decreasing life bar in the player's HUD.

As for assets, we intially aimed to have a story present through audio dialogue from a supporting off-screen NPC. This character would convey that the space-ship's shield generator is losing power, and if it runs out the enemy can destroy the ship easily. Therefore, the pilot must take out the target before their own power runs out. However due to time constraints, we weren't able to record the voice-over lines and implement them within our assets to play on game launch, and so we added it as a text instruction at the start of the game.

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

We wanted to capture the intensity of the game and contrast between a relaxed and power-down mode in the trailer. To do so, we planned to include footage without the decreasing life bar and cross-hairs, then through a transition accompanied by the alert music, we showcase the full gameplay of the missile system as well as the health bar. However, it was difficult to acquire footage of the gameplay due to the difficulty of accessing the VR equipment due to the Pandemic.

In terms of screenshots, our choices weren't many, so it was easy to select screenshots depicting the core gameplay components such as movement and camera, missile launching, and lifebar decay.



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**


**Immersion**: The main idea and concept of the VR platform is full immersion; the user's first-person view emulates that of a cockpit pilot with the control console in front of them as well as the health bar, which mimics a status display a pilot would see in one of the monitors in his dashboard. So, in order to intensify that immersion and amplify it, we focused strongly on the audio side of the things. The audio effects of moving the ship around, launching a missile, the 'low-health' phase danger alert is something very common in sci-fi and something the player is likely accustomed to from other media. Thus, adding this in combined with their VR viewpoint, should make the player fully absorb the intensity of the situation and add to their immersion.

**Player agency**: To augment the VR setting of the game, we aimed to add elements that give weight and importance to the player and their actions. Thus, we started the game by calling out the player directly in order to add importance to their character and role in the game (in future versions, this would be replaced by an audio recording of an NPC instead of a text prompt). Moreover, we accompanied the player's actions with sound effects that emphasize the fact that they are the one creating these affects at will.


