# BendyAndTheArchipelagoMachine Setup Guide

## Required Software

- [Bendy and the Ink Machine](https://store.steampowered.com/app/622650/Bendy_and_the_Ink_Machine/)
- The [BATIM apworld](https://github.com/Lorecrafter703/Archipelago/releases), 
  if not bundled with your version of Archipelago
- Thunderstore Mod Manager

If installing manually:
- [Bendy and the Archipelago Machine](https://github.com/Lorecrafter703/BendyAndTheArchipelagoMachine/releases) mod
- [BepInEx](https://github.com/BepInEx/BepInEx/releases)

## Installation

### Thunderstore

1. In the Thunderstore Mod Manager, create a profile and select Bendy and the Ink Machine as the game
2. Go to "Get Mods" section and search for "Bendy and the Archipelago Machine"
3. Click on it to expand the listing, and click the Download button that appears
4. Click on "Start Modded" to open the mod

### Manual Install

1. Extract the BepInEx zip file into your game's root directory
2. Run the game once to complete the installation
3. Navigate to the config folder, and open up the BepInEx.cfg file
4. Locate the following options and set them to true
	- HideManagerGameObject in [Chainloader]
	- Enabled is [Logging.Console]
5. Extract the mod folder into the plugins folder of the BepInEx install
6. Opening the game should now also bring up a BepInEx console


## Joining a new MultiWorld

1. When the game opens up, you should see a new section in the top left to input connection information.
2. After filling in the required fields, you may click **Begin**.
3. On a successful connection, you will be able to choose a save file. (You may need to click Begin a second time)
   - NOTE: Whichever save file you connect to first will be the save you must use every time you connect in the future.
4. After choosing a save file, you will be able to continue to chapter select, and begin playing.


## What does randomization do to this game?

By default, the following can be obtained as items:
 - Ritual items from chapter 1 (Book, Doll, Gear, Inkwell, Record, and Wrench)
 - Wally's lost keys from chapter 2
 - The collectible valve wheel from chapter 2
 - The toys clogging the toy machine in chapter 3
 - The book puzzle in chapter 4
 - Bertrum's Bossfight
 - Chapter Unlocks
 - Cans of Bacon Soup
 - Filler items

By default, the following can be sent as location checks:
 - Ritual items from chapter 1 (Book, Doll, Gear, Inkwell, Record, and Wrench)
 - Wally's lost keys from chapter 2
 - The collectible valve wheel from chapter 2
 - Defeating Bertrum in chapter 4
 - Defeating Brute Boris in chapter 4
 - Defeating Sammy Lawrence in chapter 5
 - All cans of bacon soup
 - All audio logs
 - All radios
 - Completion of a chapter
 - (optionally) Finding theMeatly cutouts
 - (optionally) Getting perfect scores in the chapter 4 warehouse minigames.

## What is the goal?

Currently, the only available goal is to beat Beast Bendy at the end of Chapter 5. Starting chapter 5 will require a
configurable number of bacon soup cans to be received.

## I received an item, but I can't place it where it needs to go?

Since Bendy and the Ink Machine doesn't have a real inventory, you still have to interact with an item to "pick it up". If
you don't have an item yet, the interaction will only try to send out the location check, and you will need to interact
with the object again after receiving the item to be able to use it fully.
