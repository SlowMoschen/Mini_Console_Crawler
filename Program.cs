namespace Console_RPG;
using Player;
using Zombie;
using Game_Methods;
using Dungeon_Generator;
using System;
using System.Linq;

public class GameState {
    public static bool isInMenu { get; set;}
    public static bool isInFight { get; set;}
    public static bool isDead { get; set;}
    public static int surviedRooms { get; set;}
    public static int killedEnemies { get; set;}
    public static int totalExperience { get; set;}

    public GameState() {
        isDead = false;
        isInMenu = true;
        isInFight = false;
        surviedRooms = 0;
        killedEnemies = 0;
        totalExperience = 0;
    }
}

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
        GameState GameState = new GameState();
        Game Game = new Game();
        Player player = new Player("Player", attack: 50, strength: 1.0, armor: 10, health: 10, maxHealth: 100, level: 1, experience: 0, experienceToLevelUp: 100);

        string[] mainMenuOptions = { "Fight", "Stats" };
        string[] options = { "Attack", "Heal", "Defend", "Run" };
        string[] attackOptions = { "Normal Attack", "Slash Attack", "Kick Attack" }; 
        string[] roomOptions = { "Easy", "Medium", "Hard" };
        
        Game.displayGreetings();

        while(GameState.isInMenu) {

            Game.displayMenuText();
            string menuChoice = Game.displayOptionMenu(" What would you like to do?", mainMenuOptions);

            if(GameState.isDead) {
                player.Heal();
                GameState.isDead = false;
                Console.WriteLine(" You have been revived!");
                break;
            }

            switch (menuChoice)
            {
                case "Fight":
                    GameState.isInMenu = false;
                    GameState.isInFight = true;
                    string fightChoice = Game.displayOptionMenu(" In wich Dungeon do you want to go?", roomOptions);

                    Room[] dungeon = DungeonGenerator.generateDungeon(fightChoice);
                    
                    Game.displayEnteredDungeon(fightChoice);
                    Game.waitForInput();
                    
                    foreach (Room room in dungeon)
                    {
                        int totalZombiesInDungeon = dungeon.Sum(room => room.zombies.Length);
                        Game.displayEnteredRoom(room.roomNumber, dungeon.Length, room.zombies.Length, totalZombiesInDungeon);
                        Game.waitForInput();
                        foreach (Zombie zombie in room.zombies)
                        {
                            if(GameState.isInFight) {
                                Game.displayBattleWith(player, zombie, options, attackOptions);
                            }
                        }
                        bool allZombiesDefeated = room.zombies.All(zombie => zombie.health <= 0);
                        if(allZombiesDefeated) {
                            Game.displayRoomVictory();
                            GameState.surviedRooms++;
                            GameState.isInMenu = true;
                            GameState.isInFight = false;
                            break;
                        }
                    }
                    Game.displayDungeonVictory();
                    break;
                case "Stats":
                    player.printStats();
                    Game.waitForInput();
                    break;
            }
        }

    }
}
