namespace Console_RPG;
using Game_Characters;
using Game_Essentials;
using Zombie;
using Weapons;
using Dungeon_Generator;
using System;
using System.Linq;

class Program
{

    // Method to get a chance for something
    // Example: getChance(50) - will return true 50% of the time
    public static bool getChance(int chance) {
        Random random = new Random();
        int randomNumber = random.Next(1, 100);
        if(randomNumber <= chance) {
            return true;
        } else {
            return false;
        }
    }
    
    static void Main(string[] args)
    {
        DisplayManager DisplayManager = new DisplayManager();
        
        DisplayManager.displayGreetings();
        // DisplayManager.getPlayerName();
        Player player = new Player(GameVariables.GameSettings.playerName, attack: 50, strength: 1.0, armor: 10, health: 100, maxHealth: 100, level: 1, experience: 0, experienceToLevelUp: 100);

        string playerWeaponChoice = DisplayManager.getPlayerWeapon();

        switch (playerWeaponChoice) {
            case "Sword":
                player.currentWeapon = new Sword(name: "Sword", attack: 12, enduranceCost: 10);
                break;
            case "Axe":
                player.currentWeapon = new Axe(name: "Axe", attack: 25, enduranceCost: 20);
                break;
            case "Mace":
                player.currentWeapon = new Mace(name: "Mace", attack: 20, enduranceCost: 15);
                break;
        }
        player.setAttackOptions();

        while(GameVariables.GameLoopBooleans.isInMenu) {

            string menuChoice = DisplayManager.displayMainMenu();

            if(GameVariables.GameLoopBooleans.isDead) {
                player.health = player.maxHealth;
                GameVariables.GameLoopBooleans.isDead = false;
                Console.WriteLine(" You have been revived!");
                break;
            }

            switch (menuChoice)
            {
                case "Enter Dungeon":
                    GameVariables.GameLoopBooleans.isInMenu = false;
                    GameVariables.GameLoopBooleans.isInFight = true;
                    string fightChoice = DisplayManager.displayOptionMenu(" In wich Dungeon do you want to go?", GameVariables.GameSettings.Options.difficultyOptions);

                    Room[] dungeon = DungeonGenerator.generateDungeon(fightChoice);
                    
                    DisplayManager.displayEnteredDungeon(fightChoice);
                    DisplayManager.waitForInput();
                    
                    foreach (Room room in dungeon)
                    {
                        int totalZombiesInDungeon = dungeon.Sum(room => room.zombies.Length);
                        DisplayManager.displayEnteredRoom(room.roomNumber, dungeon.Length, room.zombies.Length, totalZombiesInDungeon);
                        DisplayManager.waitForInput();
                        foreach (Zombie zombie in room.zombies)
                        {
                            if(GameVariables.GameLoopBooleans.isInFight) {
                                DisplayManager.displayBattleWith(player, zombie, GameVariables.GameSettings.Options.battleOptions, GameVariables.GameSettings.Options.attackOptions);
                            }
                        }
                        bool allZombiesDefeated = room.zombies.All(zombie => zombie.health <= 0);
                        if(allZombiesDefeated) {
                            DisplayManager.displayRoomVictory();
                            GameVariables.GameStats.surviedRooms++;
                        }
                        
                        bool allRoomsDefeated = dungeon.All(room => room.zombies.All(zombie => zombie.health <= 0));
                        if(allRoomsDefeated) {
                            GameVariables.GameLoopBooleans.isDungeonCleared = true;
                            break;
                        }

                    }
                    

                    if(GameVariables.GameLoopBooleans.isDungeonCleared) {
                        DisplayManager.displayDungeonVictory();
                        GameVariables.GameLoopBooleans.isInMenu = true;
                        GameVariables.GameLoopBooleans.isInFight = false;
                        GameVariables.GameLoopBooleans.isDungeonCleared = false;
                        GameVariables.GameStats.survivedDungeons++;
                    }
                    break;
                case "View Stats":
                    player.printStats();
                    DisplayManager.waitForInput();
                    break;
            }
        }

    }
}
