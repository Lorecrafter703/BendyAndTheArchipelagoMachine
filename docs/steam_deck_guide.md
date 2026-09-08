# STEAMOS / Steam Deck / Arch Linux Manual Download Guide

#### For Bendy and the Archipelago Machine versions prior to v2.0.0

**1.** Download the most recent versions of the BATIM APWorld, Mod, and BepInEx Windows X64 version.

**2.** Open your BATIM game location via opening Steam, going to the game, clicking the settings icon, followed by manage and finally Browse Local Files.

**3.** Extract BepInEx into the folder you opened in the last step.

**4.** Go back to the settings icon and click properties.

**5.** Maneuver to the Compatibility Tab and Force use of specific Steam Play compatibility tool and change it to Proton Experimental

**6.** Move back to general and go to the text box at the bottom. In this box type the following EXACTLY. Every bit matters. From symbols to space to capital letters.

WINEDLLOVERRIDES="winhttp=n,b" %command%

**7.** Move over to "Game Versions And Betas" tab. And pick the "release-1.5.2.2" build.

(This step may be taken out for  versions of the mod that release after this post.)

**8.** Boot the game and wait for you to reach the title screen. (It should also boot a side screen. This is normal and just BepInEx initializing.)

**9.** Quit game and go back to your files.

**10.** Extract the mod into your Bendy And The Ink Machine/BepInEx/plugins

NOTE: THE FOLDER CREATED IN THE PLUGINS FOLDER SHOULD BE TITLED
Lorecrafter703-Bendy\_and\_the\_Archipelago\_Machine/
WITH A 2ND BendyAndTheArchipelagoMachine FOLDER WITHIN IT.

**11.** Move back to the BepInEx folder before clicking on config then opening the BepInEx.cfg file.

**12.** Set the following two parameters to "true"
"HideManagerGameObject" (inside of the ChainLoader section)
"Enabled" (inside of the Logging.Console Section)

**13.** Save the file.

**14.** Download the APWorld into your Archipelago app just like any other apworld (don't forget to close and reopen Archipelago for your changes to take place.)

**15.** If everything was done properly, You should just be able to open the game and a new menu at the top left of your screen should appear. This is how you know the mod is loaded properly.

# To Play

Just type in your info. As you would other games.
Host is where your host code is input (ask your host for the room code)
PlayerName is the name you put onto the Yaml you made for the game.
Password is the room Password, only needed if set by the host.



Credit to @bonestennyson on discord for the guide

