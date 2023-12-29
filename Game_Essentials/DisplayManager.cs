using Game_Essentials;
using Game_Characters;
using System;
using System.Linq;
using System.Diagnostics;
using UserInput;
using Dungeon_Generator;
using _Items;

namespace Console_Output 
{
    public class DisplayManager {

        InputHandler InputHandler = new InputHandler();


        /*
        /
        /    Methods for Displaying Logic before the game starts
        /
        */

        public void displayGameLogo() {
            Console.Clear();
            Console.WriteLine(" ---------------------------");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |       Console_RPG       |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |   A simple console RPG  |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" ---------------------------");
            Console.WriteLine(" Version: " + GameVariables.GameStats.version);
            Console.WriteLine();
        }

        public void displayGreetings() {
            Console.Clear();
            this.displayGameLogo();
            Console.WriteLine(" Welcome to Console_RPG!");
            // Console.WriteLine(" The goal of this game is to defeat all the enemies in the hardest Dungeon.");
            Console.WriteLine(" There is no Goal yet, but you can defeat enemies in Dungeons.");
            this.waitForInput();
            Console.Clear();
        }

        // Method to get the player name before the game starts
        public void getPlayerName() {
            Console.Clear();
            this.displayGameLogo();
            Console.WriteLine(" What is the Name of your Hero?");
            string playerName = Console.ReadLine();
            GameVariables.PlayerStats.playerName = playerName;
            Console.WriteLine();
            Console.WriteLine(" Your Hero is called " + playerName);
            this.waitForInput();
            Console.Clear();
        }

        // Method to get the player weapon before the game starts
        public string getPlayerWeapon() {
            Console.Clear();
            this.displayGameLogo();
            string playerWeapon = InputHandler.getChoice(" What weapon will your Hero use?", new string[] { "Sword", "Axe", "Mace" });
            Console.WriteLine();
            Console.WriteLine(" Your Hero will use a " + playerWeapon);
            this.waitForInput();
            Console.Clear();
            return playerWeapon;
        }

        /*
        /
        /    Methods for Displaying Menu Logic
        /
        */

        public string displayMainMenu() {
            Console.Clear();
            this.displayHeader("Main Menu");
            string menuChoice = this.displayOptionMenu(" What would you like to do?", GameVariables.GameSettings.Options.mainMenuOptions);
            return menuChoice;
        }

        public void displayShop(Player player) {
            Console.Clear();
            this.displayHeader("Shop");
            Console.WriteLine(" You have " + player.InventoryManager.gold + " gold");
            Console.WriteLine();
            Console.WriteLine(" Inventory:");
            player.printPotionsInventory();
            Console.WriteLine();
            Console.WriteLine(" You can carry a maximum of " + GameVariables.GameSettings.ItemMaxQuantity.strengthPotionMaxQuantity + " Strength Potions");
            Console.WriteLine(" You can carry a maximum of " + GameVariables.GameSettings.ItemMaxQuantity.healPotionMaxQuantity + " Heal and Endurance Potions");
            string menuChoice = this.displayOptionMenu(" What would you like to do?", GameVariables.GameSettings.Options.shopMenuOptions);
            
            switch (menuChoice) {
                case "Buy":
                    string itemChoice = this.displayOptionMenu(" What would you like to buy?", GameVariables.GameSettings.Options.shopItems, GameVariables.GameSettings.ItemPrices.allItemPrices);
                    if(itemChoice == "Heal Potion") 
                    {
                        
                        Potion healPotion = new _Items.Potion(
                            name: "Heal Potion", 
                            type: "Health Potion", 
                            description: "Heals the Player for " + GameVariables.GameSettings.healPotionHealRating + " health", 
                            price: GameVariables.GameSettings.ItemPrices.healPotionPrice, 
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.healPotionMaxQuantity, 
                            effectValue: GameVariables.GameSettings.healPotionHealRating
                        );

                        string quantityChoice = this.displayOptionMenu(" How many do you want to buy?", new string[] { "1", "2", "3", "Maximum" });
                        if(quantityChoice == "Maximum") {
                            quantityChoice = player.calculateMaxPotionsPurchase(healPotion);
                        }

                        for(int i = 0; i < int.Parse(quantityChoice); i++) {
                            player.buyItem(healPotion);
                        }
                    }
                    else if (itemChoice == "Strength Potion")
                    {
                        Potion strengthPotion = new _Items.Potion(
                            name: "Strength Potion",
                            type: "Strength Potion",
                            description: "Increases the Player's strength by " + GameVariables.GameSettings.strengthPotionStrengthRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.strengthPotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.strengthPotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.strengthPotionStrengthRating
                        );

                        string quantityChoice = this.displayOptionMenu(" How many do you want to buy?", new string[] { "1", "2", "3", "Maximum" });
                        if(quantityChoice == "Maximum") {
                            quantityChoice = player.calculateMaxPotionsPurchase(strengthPotion);
                        }

                        for(int i = 0; i < int.Parse(quantityChoice); i++) {
                            player.buyItem(strengthPotion);
                        }
                    }
                    else if (itemChoice == "Endurance Potion")
                    {
                        Potion endurancePotion = new _Items.Potion(
                            name: "Endurance Potion",
                            type: "Endurance Potion",
                            description: "Increases the Player's endurance by " + GameVariables.GameSettings.endurancePotionEnduranceRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.endurancePotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.endurancePotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.endurancePotionEnduranceRating
                        );

                        string quantityChoice = this.displayOptionMenu(" How many do you want to buy?", new string[] { "1", "2", "3", "Maximum" });
                        if(quantityChoice == "Maximum") {
                            quantityChoice = player.calculateMaxPotionsPurchase(endurancePotion);
                        }

                        for(int i = 0; i < int.Parse(quantityChoice); i++) {
                            player.buyItem(endurancePotion);
                        }
                    }
                    break;
                case "Exit":
                    GameVariables.GameLoopBooleans.isInShop = false;
                    GameVariables.GameLoopBooleans.isInMenu = true;
                    Console.WriteLine(" You left the Shop");
                    break;
            }

        }



        /*
        /
        /    Helper Methods
        /
        */

        // Method to wait for user input before continuing
        public void waitForInput(string message = " Press any key to continue...") {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        // Method to display a header with a message
        // Calulate the padding based on the message length
        // Example: displayHeader("Hello World") will display:
        // ---------------------------
        //        Hello World
        // ---------------------------
        public void displayHeader(string message) {
            // Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------");

            int dashedLineLength = 27;
            int padding = (dashedLineLength - message.Length) / 2;

            Console.WriteLine("{0," + ((dashedLineLength - message.Length) / 2 + message.Length) + "}", message);

            Console.WriteLine("---------------------------");
            Console.WriteLine();
        }

        // Method to display a menu with a message and options
        public string displayOptionMenu(string message ,string[] options, string[] prices = null) {
            Console.WriteLine();
            string playerChoice = InputHandler.getChoice(message, options, prices);
            Console.WriteLine();
            return playerChoice;
        }

        /*
        /
        /    Methods for Displaying Logic during the Battle
        /
        */

        public  void displayNewEncounter(string enemyName) {
            this.displayHeader("New Encounter");
            Console.WriteLine(" You encountered a " + enemyName);
        }

        public void displayGameStats() {
            this.displayHeader("Game Stats");
            Console.WriteLine(" Survived Rooms: " + GameVariables.GameStats.surviedRooms);
            Console.WriteLine(" Cleared Dungeons: " + GameVariables.GameStats.survivedDungeons);
            Console.WriteLine(" Killed Enemies: " + GameVariables.GameStats.killedEnemies);
            Console.WriteLine(" Total Experience gained: " + GameVariables.GameStats.totalExperience);
            Console.WriteLine(" Total Gold gained: " + GameVariables.GameStats.totalGold);
            Console.WriteLine();
        }

        public void displayBattleStats(Player player, Enemy enemy) {
            Console.WriteLine();
            player.printBattleStats();
            this.displayHeader("VS");
            enemy.printBattleStats();
        }

        public void displayEnteredRoom( int roomID, int roomCount,int enemiesCount, int totalEnemiesCount) {
            this.displayHeader("New Room");
            Console.WriteLine(" You entered Room " + roomID + " of " + roomCount + " in this Dungeon");
            string EnemySingularOrPlural = enemiesCount > 1 ? "Enemies" : "Enemy";
            Console.WriteLine(" You have to defeat " + totalEnemiesCount + " " + EnemySingularOrPlural + " to get to the end of the Dungeon");
        }

        public void displayEnteredDungeon(string difficulty) {
            this.displayHeader("New Dungeon");
            Console.WriteLine(" You entered the " + difficulty + " Dungeon");
            Console.WriteLine(" You have to defeat all the enemies to get to the end of the Dungeon");
        }

        public void displayBattleOutcome(string playerChoice, string enemyChoice, Player player, Enemy enemy, string attackChoice)
        {
            double playerDamage = player.currentWeapon.attack * player.strength / enemy.armor;
            double playerKickDamage = (player.attack + player.kickAttackStrength) * player.strength / enemy.armor;
            double playerSpecialAttackDamage = (player.currentWeapon.specialAttackStrength + player.currentWeapon.attack) * player.strength / enemy.armor;
            double enemyDamage = enemy.attack * enemy.strength / player.armor;
            double enemyHeal = enemyDamage / 2;
            double enemySelfDamage = enemyDamage / 4;

                switch (playerChoice)
                {
                    case "Attack":     
                        if(!enemy.isDefending) {
                            if(GameVariables.GameLoopBooleans.wasPlayerAttackMade) {
                                if(attackChoice == "Normal Attack") {
                                    Console.WriteLine($" You attacked the enemy for {playerDamage} damage");
                                } else if(attackChoice == "Kick Attack") {
                                    Console.WriteLine($" You kicked the enemy for {playerKickDamage} damage");
                                } else if(attackChoice == player.currentWeapon.specialAttackName) {
                                    Console.WriteLine($" You used {player.currentWeapon.specialAttackName} for {playerSpecialAttackDamage} damage");
                                }
                                GameVariables.GameLoopBooleans.wasPlayerAttackMade = false;
                            } else {
                                Console.WriteLine(" You don't have enough endurance to attack");
                                break;
                            }
                        } else {
                            enemy.isDefending = false;
                        }
                        break;
                    case "Rest":
                        Console.WriteLine($" You healed for {player.healRating} health and got {(GameVariables.GameSettings.enduranceRegeneration * 2) + GameVariables.GameSettings.enduranceRegeneration } endurance back.");
                        break;
                    case "Use Potion":
                        if(attackChoice == "Heal Potion") {
                            Console.WriteLine($" You healed for {GameVariables.GameSettings.healPotionHealRating} health");
                        } else if(attackChoice == "Strength Potion") {
                            Console.WriteLine(player.strength);
                            Console.WriteLine($" You gained strength for {GameVariables.GameSettings.EffectDurations.strengthDuration} turns");
                        } else if(attackChoice == "Endurance Potion") {
                            Console.WriteLine($" You gained {GameVariables.GameSettings.endurancePotionEnduranceRating} endurance");
                        }
                        break;
                    case "Defend":
                        Console.WriteLine(" You successfully defended the attack!");
                        break;
                    case "Run":
                        Console.WriteLine(" You ran away");
                        break;
                    case "Stunned":
                        break;
                }
            
            

            if (enemy is Zombie.Zombie && !player.isDefending)
            {
                switch (enemyChoice)
                {
                    case "bite":
                        Console.WriteLine($" The zombie bit you for {enemyDamage} damage and healed for {enemyHeal} health");
                        break;
                    case "thrash":
                        Console.WriteLine($" The zombie thrashed you for {enemyDamage} damage, and took {enemySelfDamage} damage with it.");
                        break;
                    case "attack":
                        Console.WriteLine($" The zombie attacked you for {enemyDamage} damage");
                        break;
                    case "defend":
                        Console.WriteLine(" The zombie defended the attack");
                        break;
                }
            }

            if(enemy is _Spider.Spider && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "spit":
                        if(player.isPoisoned) {
                            Console.WriteLine($" The spider spit at you for {enemyDamage} damage and poisoned you");
                            Console.WriteLine($" You took {GameVariables.EnemyStats.Spider.poisonDamage} damage from the poison");
                            Console.WriteLine($" You are poisoned for {GameVariables.GameSettings.EffectDurations.poisonDuration - 1} more turns");
                        } else {
                            Console.WriteLine($" The spider spit at you for {enemyDamage} damage");
                        }
                        break;
                    case "attack":
                        Console.WriteLine($" The spider attacked you for {enemyDamage} damage");
                        break;
                case "defend":
                        Console.WriteLine(" The spider defended the attack");
                        break;
                }

                if(player.isPoisoned && enemyChoice != "spit") {
                    Console.WriteLine($" You took {GameVariables.EnemyStats.Spider.poisonDamage} damage from the poison");
                    Console.WriteLine($" You are poisoned for {player.poisonedTurns} more turns");
                }
            }

            if(enemy is _Goblin.Goblin && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "attack":
                        Console.WriteLine($" The goblin attacked you for {enemyDamage} damage");
                        break;
                    case "steal":
                        Console.WriteLine($" The goblin stole {GameVariables.EnemyStats.Goblin.stealAmount} gold from you");
                        
                        if(GameVariables.GameLoopBooleans.wasEnemyAttackMade) {
                            Console.WriteLine($" The goblin attacked you for {enemyDamage} damage");
                        }
                        break;
                    case "defend":
                        Console.WriteLine(" The goblin defended the attack");
                        break;
                }
            }

            if(enemy is _Assassin.Assassin && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "attack":
                        Console.WriteLine($" The assassin attacked you for {enemyDamage} damage");
                        break;
                    case "backstab":
                        double backstabDamage = enemyDamage * 2;
                        Console.WriteLine($" The assassin backstabbed you for {backstabDamage} damage");
                        break;
                    case "defend":
                        Console.WriteLine(" The assassin defended the attack");
                        break;
                }
            }

            if(enemy is _StoneGolem.StoneGolem && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "attack":
                        Console.WriteLine($" The stone golem attacked you for {enemyDamage} damage");
                        break;
                    case "slam":
                        Console.WriteLine($" The stone golem slammed you for {enemyDamage} damage");
                        if(player.isStunned) {
                            Console.WriteLine(" You are stunned for the next turn");
                        }
                        break;
                }
            }

            if(enemy is Boss_Dragon.Dragon && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "attack":
                        Console.WriteLine($" The dragon attacked you for {enemyDamage} damage");
                        break;
                    case "fire breath":
                        Console.WriteLine($" The dragon used fire breath on you for {enemyDamage} damage");
                        if(player.isBurning) {
                            Console.WriteLine($" You took {GameVariables.EnemyStats.Dragon.burningDamage} damage from the fire");
                            Console.WriteLine($" You are burning for {player.burningTurns} more turns");
                        }
                        break;
                    case "rock throw":
                        Console.WriteLine($" The dragon threw a rock at you for {enemyDamage} damage");
                        if(player.isStunned) {
                            Console.WriteLine(" You are stunned for the next turn");
                        }
                        break;
                    case "tail strike":
                        double tailStrikeDamage = (enemy.attack + GameVariables.EnemyStats.Dragon.tailStrikeDamage) * enemy.strength;
                        Console.WriteLine($" The dragon used tail strike on you for {tailStrikeDamage} damage");
                        break;
                }

                if(player.isBurning && enemyChoice != "fire breath") {
                    Console.WriteLine($" You took {GameVariables.EnemyStats.Dragon.burningDamage} damage from the fire");
                    Console.WriteLine($" You are burning for {player.burningTurns} more turns");
                }
            }
        }

        public void displayBattleWith(Player player, Enemy enemy, string[] options, string[] attackOptions) {
                            Console.Clear();
                            this.displayNewEncounter(enemy.name);

                            while (player.health > 0 && enemy.health > 0)
                        {
                            Console.WriteLine();

                            this.displayBattleStats(player, enemy);

                            string playerChoice = "";
                            string attackChoice = "";

                            if(!player.isStunned) {
                                playerChoice = this.displayOptionMenu(" What will you do?", options);

                                switch (playerChoice)
                                {
                                    case "Attack":
                                        attackChoice = player.chooseAttack(enemy);
                                        break;
                                    case "Rest":
                                        player.Rest();
                                        break;
                                    case "Use Potion":
                                        attackChoice = this.displayOptionMenu(" What item do you want to use?", GameVariables.GameSettings.Options.shopItems);
                                        player.usePotion(attackChoice);
                                        break;
                                    case "Defend":
                                        player.Defend(enemy);
                                        break;
                                    case "Run":
                                        Console.WriteLine(" Runnig away will end the fight, but you get no reward and you lose half of your Gold.");
                                        string isRunning = this.displayOptionMenu(" Are you sure you want to run?", new string[] { "Yes", "No" });
                                        if (isRunning == "Yes")
                                        {
                                            GameVariables.GameLoopBooleans.ranAway = true;
                                            Console.WriteLine(" You ran away");
                                            // player.loseGold(0);
                                            GameVariables.GameLoopBooleans.isInMenu = true;
                                            GameVariables.GameLoopBooleans.isInFight = false;
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                }
                            } else {
                                Console.WriteLine(" You are stunned and can't do anything");
                                player.isStunned = false;
                                playerChoice = "Stunned";
                            }

                            string enemyChoice = enemy.executeMove(player);
                            player.decrementBuffs();
                            player.applyOverTimeEffects();

                            if (!GameVariables.GameLoopBooleans.isInFight)
                            {
                                break;
                            }

                            if (enemy.health <= 0)
                            {
                                Console.WriteLine($" You defeated the {enemy.name}");
                                GameVariables.GameStats.killedEnemies++;
                                Console.WriteLine(" You gained " + enemy.experienceOnDefeat + " experience");
                                Console.WriteLine(" The Enemy dropped " + enemy.goldOnDefeat + " gold");
                                player.gainGold(enemy.goldOnDefeat);
                                player.gainExperience(enemy.experienceOnDefeat);
                                this.waitForInput();
                                break;
                            }
                            else
                            {
                                this.displayBattleOutcome(playerChoice, enemyChoice, player, enemy, attackChoice);

                                // Regenerate Endurance
                                if(player.endurance != player.maxEndurance) {
                                    player.endurance += GameVariables.GameSettings.enduranceRegeneration;
                                    if(player.endurance > player.maxEndurance) {
                                        player.endurance = player.maxEndurance;
                                    }
                                }

                                if (player.health <= 0)
                                {
                                    this.displayDefeat();

                                    GameVariables.GameLoopBooleans.isDead = true;
                                    GameVariables.GameLoopBooleans.isInMenu = true;
                                    GameVariables.GameLoopBooleans.isInFight = false;

                                    break;
                                }
                            }

                            this.waitForInput();
                            Console.Clear();
                        }
        }

        public void enterDungeon(Player player) {
            string fightChoice = this.displayOptionMenu(" In wich Dungeon do you want to go?", GameVariables.GameSettings.Options.difficultyOptions);

                    Dungeon dungeon = new Dungeon(fightChoice);

                    this.displayEnteredDungeon(fightChoice);
                    int totalMobsInDungeon = dungeon.rooms.Sum(room => room.roomEnemies.Length);
                    this.waitForInput();
                    
                    foreach (Room room in dungeon.rooms)
                    {
                        if(GameVariables.GameLoopBooleans.isInMenu) {
                            break;
                        }

                        this.displayEnteredRoom(room.roomID, dungeon.rooms.Length, room.roomEnemies.Length, totalMobsInDungeon);
                        this.waitForInput();
                        foreach (Enemy enemy in room.roomEnemies)
                        {
                            if(GameVariables.GameLoopBooleans.isInFight) {
                                this.displayBattleWith(player, enemy, GameVariables.GameSettings.Options.battleOptions, GameVariables.GameSettings.Options.attackOptions);
                            }
                        }
                        bool allMobsDefeated = room.roomEnemies.All(enemy => enemy.health <= 0);
                        if(allMobsDefeated) {
                            this.displayRoomVictory();
                            GameVariables.GameStats.surviedRooms++;
                        }
                        
                        bool allRoomsDefeated = dungeon.rooms.All(room => room.roomEnemies.All(zombie => zombie.health <= 0));
                        if(allRoomsDefeated) {
                            GameVariables.GameLoopBooleans.isDungeonCleared = true;
                            break;
                        }

                    }
                    

                    if(GameVariables.GameLoopBooleans.isDungeonCleared) {
                        this.displayDungeonVictory(dungeon, player);
                        GameVariables.GameLoopBooleans.isInMenu = true;
                        GameVariables.GameLoopBooleans.isInFight = false;
                        GameVariables.GameLoopBooleans.isDungeonCleared = false;
                        GameVariables.GameStats.survivedDungeons++;
                    }
        }

        /*
        /
        /    Methods for Displaying Logic after the Battle
        /
        */

        public void displayDungeonVictory(Dungeon dungeon, Player player) {
            this.displayHeader("Dungeon Cleared");
            Console.WriteLine(" You defeated all the Enemies in this Dungeon");
            Console.WriteLine();
            this.displayHeader("Rewards");
            dungeon.openChest(player);
            this.waitForInput(" Press any key to get back to the Main Menu");
        }

        public void displayRoomVictory() {
            this.displayHeader("Room Cleared");
            this.waitForInput(" Press any key to get back to enter the next Room");
        }

        public void displayDefeat() {
            this.displayHeader("You Died");
            this.displayGameStats();
            this.waitForInput(" Press any key to get back to the Main Menu");
        }
    }
}