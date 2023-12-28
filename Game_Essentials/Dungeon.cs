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
        public static Enemy[] generateMobs(int count) {
            Enemy[] enemies = new Enemy[count];
            string[] enemytypes = GameVariables.GameSettings.DungeonSettings.enemyTypes;
            Random random = new Random();
            int index = random.Next(enemytypes.Length);

            string enemyType = enemytypes[index];
            
            for (int i = 0; i < count; i++)
            {
                switch (enemyType) {
                    case "Spider":
                        enemies[i] = new _Spider.Spider(enemytypes[index], attack: 100, strength: 1.0, armor: 5, health: 100, experienceOnDefeat: 25);
                        break;
                    case "Zombie":
                        enemies[i] = new Zombie.Zombie(enemytypes[index], attack: 50, strength: 1.0, armor: 5, health: 50, experienceOnDefeat: 15);
                        break;
                }
            }

            return enemies;
        }
    }
}