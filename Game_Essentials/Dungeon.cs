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

        public int roomNumber { get; set; }
        public Zombie.Zombie[] zombies { get; set; }

        public Room(int roomNumber, string difficulty) {
            this.roomNumber = roomNumber;
            this.zombies = generateRoom(difficulty);
        }

        public static Zombie.Zombie[] generateRoom(string difficulty) {
            // Generate a random room based on the difficulty
            // Easy - 1 enemy
            // Medium - 3 enemies
            // Hard - 5 enemies
            switch (difficulty)
            {
                case "Easy":
                    return generateZombies(GameVariables.GameSettings.DungeonSettings.RoomSettings.easyEnemies);
                case "Medium":
                    return generateZombies(GameVariables.GameSettings.DungeonSettings.RoomSettings.mediumEnemies);
                case "Hard":
                    return generateZombies(GameVariables.GameSettings.DungeonSettings.RoomSettings.hardEnemies);
            }

            return generateZombies(1);
        }

        // Generate a number of zombies
        public static Zombie.Zombie[] generateZombies(int count) {
            Zombie.Zombie[] zombies = new Zombie.Zombie[count];

            for (int i = 0; i < count; i++)
            {
                zombies[i] = new Zombie.Zombie("Zombie", attack: GameVariables.GameSettings.maxZombieAttack, strength: GameVariables.GameSettings.maxZombieStrength, armor: GameVariables.GameSettings.maxZombieArmor, health: GameVariables.GameSettings.maxZombieHealth, experienceOnDefeat: GameVariables.GameSettings.maxZombieExperience);
            }            

            return zombies;
        }
    }
}