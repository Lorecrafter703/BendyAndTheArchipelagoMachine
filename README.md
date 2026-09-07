# BendyAndTheArchipelagoMachine Setup Guide

## NOTICE

In the recent update to the game, Beany and the Ink Machine had it's unity version updated, and as such, BepInEx 5 no longer is compatible. To use this
mod, you will need to downpatch your game to a previous version.

## Required Software

- [Bendy and the Ink Machine](https://store.steampowered.com/app/622650/Bendy_and_the_Ink_Machine/)
- The [BATIM apworld](https://github.com/Lorecrafter703/Archipelago/releases), 
  if not bundled with your version of Archipelago
- Thunderstore Mod Manager

If installing manually:
- [Bendy and the Archipelago Machine](https://github.com/Lorecrafter703/BendyAndTheArchipelagoMachine/releases) mod
- [BepInEx 5](https://github.com/BepInEx/BepInEx/releases)

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
	- Enabled in [Logging.Console]
5. Extract the mod folder into the plugins folder of the BepInEx install
6. Opening the game should now also bring up a BepInEx console

### Steam Deck/Steam OS/Arch Linux?

Check out this [guide by bonestennyson](https://github.com/Lorecrafter703/BendyAndTheArchipelagoMachine/blob/master/docs/steam_deck_guide.md) for a
better install guide.


## Joining a new MultiWorld

1. When the game opens up, you should see a new section in the top left to input connection information.
2. After filling in the required fields, you may click **Connect**.
3. On a successful connection, you should see the connection menu replaced with a count for received Bacon Soups,
and a toggle for deathlink. You should now be able to select **Begin**, and continue as normal.

**NOTE:** It is recommended to back up your save files before playing, as selecting a slot will
automatically override any data that was previously there.


## What does randomization do to this game?

The following can be obtained as items:
 - Ritual items from chapter 1 (Book, Doll, Gear, Inkwell, Record, and Wrench)
 - Wally's lost keys from chapter 2
 - The collectible valve wheel from chapter 2
 - The toys clogging the toy machine in chapter 3
 - The book puzzle in chapter 4
 - Bertrum's Bossfight
 - Chapter Unlocks
 - Cans of Bacon Soup
 - Filler items
	- Empty Soup Can
	- Empty Ink Well
	- Broken Banjo String
 - (optionally) Checkpoints
 - (optionally) The Tommy Gun
 - (optionally) Boris's Bone

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
 - (optionally) Checkpoints
 - (optionally) The Tommy Gun
 - (optionally) Completing the CH3 Lever Challenge (1 check per wave)
 - (optionally) Boris's Bone

## What is the goal?

The default goal is to beat Beast Bendy at the end of Chapter 5. Starting the fight will require a
configurable number of bacon soup cans to be received. The goal can be changed to completion of any chapter,
with later chapters being discluded from randomization unless specifically included. Chapters preceding the
goal chapter can also be set to be required for completion.

## I received an item, but I can't use it?

Since Bendy and the Ink Machine doesn't have a real inventory, you still have to interact with an item to "pick it up". If
you don't have an item yet, the interaction will only try to send out the location check, and you will need to interact
with the object again after receiving the item to be able to use it fully.

## I can't interact with something?

Several spots in the game require you to interact with something that has now been locked behind an item in the mod.
Generally, if there is something that won't let you interact with it, it is probably an item you are missing, which can
be found in the list above.

## How does Deathlink work?

Deathlink works mostly how you would expect, with a few caveats. If you are in chapter 1, death links you recieve will
still "trigger", but nothing will actually happen. Also, deathlinks cannot happen while you are in the death tunnel
(though they can still be queued), as well as any time the game considers you to be in a "hidden" state. This includes
the Miracle Stations and riding the cart at the end of chapter 4 for example.