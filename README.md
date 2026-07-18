# BendyAndTheArchipelagoMachine Setup Guide

## Required Software

- [Bendy and the Ink Machine](https://store.steampowered.com/app/622650/Bendy_and_the_Ink_Machine/)
- The [BATIM apworld](https://github.com/Lorecrafter703/Archipelago/releases), 
  if not bundled with your version of Archipelago
- [Bendy and the Archipelago Machine](https://github.com/Lorecrafter703/BendyAndTheArchipelagoMachine/releases) mod

## Installation

1. Back up your save files. The mod is still in development, and currently does not preserve saves.
   1. The saves will be in a file called **batim.game**, located in whichever directory steam stores
   save files on your operating system.
2. Download the latest release of the [Bendy and the Archipelago Machine](https://github.com/Lorecrafter703/BendyAndTheArchipelagoMachine/releases) mod
3. Extract the zip file into the game's root directory where the exe file is located.
4. Open the game, if all is well you should see a BepInEx console open up.

## Joining a new MultiWorld

1. When the game opens up, you should see a new section in the top left to input connection information.
2. After filling in the required fields, you may click **Begin**.
3. On a successful connection, you will be able to choose a save file. (You may need to click Begin a second time)
   - NOTE: Whichever save file you connect to first will be the save you must use every time you connect in the future.
4. After choosing a save file, you will be able to continue to chapter select, and begin playing.

## Uninstalling

To uninstall, first locate the game's home directory.

To remove all mods:
 - Delete or move the BepInEx folder out of the game directory.

To remove only this mod:
 - Navigate to BepInEx/plugins/
 - Delete or move the BendyAndTheArchipelagoMachine folder out of the directory

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
