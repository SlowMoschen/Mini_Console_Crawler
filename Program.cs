namespace Console_RPG;
using Player;
using Zombie;
using WriteLine;

public class GameState {
    public static bool isInMenu { get; set;}
    public static bool isInFight { get; set;}

    public GameState() {
        isInMenu = true;
        isInFight = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        GameState GameState = new GameState();
        WriteLine WriteLine = new WriteLine();
        Player player = new Player("Player", attack: 50, strength: 1.0, armor: 10, health: 100, maxHealth: 100, level: 1, experience: 0, experienceToLevelUp: 100);

        var zombies = new []
        {
            new Zombie("Zombie1", attack: 10, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 20),
            new Zombie("Zombie2", attack: 10, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 20),
            new Zombie("Zombie3", attack: 10, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 20),
            new Zombie("Zombie4", attack: 10, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 20),
            new Zombie("Zombie5", attack: 10, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 20),
        };

        string[] mainMenuOptions = { "Fight", "Stats" };
        string[] options = { "Attack", "Heal", "Defend", "Run" };
        string[] attackOptions = { "Normal Attack", "Slash Attack", "Kick Attack" }; 
        while(GameState.isInMenu) {

            WriteLine.displayMenuText();
            string menuChoice = WriteLine.displayOptionMenu(" What would you like to do?", mainMenuOptions);
            
            switch (menuChoice)
            {
                case "Fight":
                    GameState.isInMenu = false;
                    GameState.isInFight = true;
                    foreach (Zombie zombie in zombies)
                    {
                        if(GameState.isInFight) {
                            WriteLine.displayBattleWith(player, zombie, options, attackOptions);
                        }

                        bool allZombiesDefeated = zombies.All(zombie => zombie.health <= 0);
                        if(allZombiesDefeated) {
                            WriteLine.displayVictory();
                            GameState.isInMenu = true;
                            GameState.isInFight = false;
                            break;
                        }
                    }
                    break;
                case "Stats":
                    player.printStats();
                    break;
            }
        }

    }
}
