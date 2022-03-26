# AssetFlip
Using the myBad Studios assets it takes minutes to turn any demo scene into a game

A lot of the work is already done inside the various assets but some work still needs to be done manually. The Asset Flip package seems to automate that as far as possible and provide a code-free component system to take care of as much as it can

This asset will be updated and added to as time goes on and this file will be updated to explain what is in the asset so far

**Inventory And Game state manager**
The inventory is a static class that stores any- and everything you want it to. Int's floats, bools, strings, Vectors, Quaternions, Arrays... To use it as a game state management system simply add all your game progress and mark everything that happens inside this. For example, when you complete a level for the first time, set "CompletedLevel" to 3 or 5 owhatever the level is you just completed. Or, for quest tracking, store how many fish was caught or how many trolls were killed etc.

Everything stored to the inventory can be saved or loaded using a single line of code each. If the Bridge: Data asset is installed in your project you can even save all of this to the cloud, instantly making your game cloud based

**Save Game Spot**
Making use of the Inventory, this class allows you to drop any number of game save prefabs into your scene and upon the user triggering the collider the entire inventory (and thus everything the player has done, collected etc) is automatically stored to disk (or online if you have Bridge; Data installed in your project). 

**Collectibles**
Simple run into an object to add it to your inventory. Optionally destroy it in the scene

**Mob Spawning**
Drop one class onto enemies to make them spawnable then drag all the spawnable characters into the list of enemies you want to be spawable in a particular reason. Scale the region to place the characters in then seelct how often the spawner must check if it should respawn enemies to keep the area filled with enemies (or to make them spawn once every 24 hours only, up to you). Select the max number to spawn and how many to spawn on game start... now hit play.

More to come...
