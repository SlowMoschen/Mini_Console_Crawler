using Zombie;
using Game_Essentials;
using Game_Characters;

namespace Dungeon_Generator {

    public class DungeonGenerator {
            
            public static Room[] generateDungeon(string difficulty) {

                int rooms;
                switch (difficulty)
                {
                    case "Easy":
                        rooms = GameVariables.GameSettings.DungeonSettings.easyRooms;
                        break;
                    case "Medium":
                        rooms = GameVariables.GameSettings.DungeonSettings.mediumRooms;
                        break;
                    case "Hard":
                        rooms = GameVariables.GameSettings.DungeonSettings.hardRooms;
                        break;
                    default:
                        rooms = 1;
                        break;
                }
                Room[] dungeon = new Room[rooms];
    
                for (int i = 0; i < rooms; i++)
                {
                    dungeon[i] = new Room(i + 1, difficulty);
                }
    
                return dungeon;
            }
    }

    public class Room {

        public int roomID { get; set; }
        public Enemy[] roomEnemies { get; set; }

        public Room(int roomID, string difficulty) {
            this.roomID = roomID;
            this.roomEnemies = generateRoom(difficulty);
        }

        // Generate a room of enemies
        public static Enemy[] generateRoom(string difficulty) {
            Enemy[] enemies;
            switch (difficulty)
            {
                case "Easy":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.easyMobs);
                    break;
                case "Medium":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.mediumMobs);
                    break;
                case "Hard":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.hardMobs);
                    break;
                default:
                    enemies = generateMobs(1);
                    break;
            }
            return enemies;
        }

        // Generate a number of zombies
        public static Enemy[] generateMobs(int count)
        {
            Enemy[] enemies = new Enemy[count];
            string[] enemytypes = GameVariables.GameSettings.DungeonSettings.enemyTypes;
            Random random = new Random();
            int index = random.Next(enemytypes.Length);

            string enemyType = enemytypes[index];

            for (int i = 0; i < count; i++)
            {
                switch (enemyType)
                {
                    case "Spider":
                        enemies[i] = new _Spider.Spider(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Zombie":
                        enemies[i] = new Zombie.Zombie(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Goblin":
                        enemies[i] = new _Goblin.Goblin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Goblin.attack,
                            strength: GameVariables.EnemyStats.Goblin.strength,
                            armor: GameVariables.EnemyStats.Goblin.armor,
                            health: GameVariables.EnemyStats.Goblin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Goblin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Goblin.goldOnDefeat
                        );
                        break;
                    case "Assassin":
                        enemies[i] = new _Assassin.Assassin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Assassin.attack,
                            strength: GameVariables.EnemyStats.Assassin.strength,
                            armor: GameVariables.EnemyStats.Assassin.armor,
                            health: GameVariables.EnemyStats.Assassin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Assassin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Assassin.goldOnDefeat
                        );
                        break;
                    case "Stone Golem":
                        enemies[i] = new _StoneGolem.StoneGolem(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.StoneGolem.attack,
                            strength: GameVariables.EnemyStats.StoneGolem.strength,
                            armor: GameVariables.EnemyStats.StoneGolem.armor,
                            health: GameVariables.EnemyStats.StoneGolem.health,
                            experienceOnDefeat: GameVariables.EnemyStats.StoneGolem.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.StoneGolem.goldOnDefeat
                        );
                        break;
                }
            }

            return enemies;
        }
    }
}