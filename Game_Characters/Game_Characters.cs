using Weapons;
using System.Collections;
using Game_Essentials;
using Console_Output;
using _Inventory;
using _Items;

namespace Game_Characters
{

    /*
        ----------------------------------
        |         Character Class         |
        ----------------------------------
    **/

    public class Character {

        public string name;
        public int attack;
        public double strength;
        public int armor;
        public double health;
        public bool isStunned = false;
        public bool isDefending = false;
        public bool isPoisoned = false;
        public bool isBurning = false;
        public int burningTurns = 0;
        public int poisonedTurns = 0;
        public bool strengthBuffed = false;
        
        public Character (string name, int attack, double strength, int armor, double health) {
            this.name = name;
            this.attack = attack;
            this.strength = strength;
            this.armor = armor;
            this.health = health;
        }

        public void Attack (Character target) {
            if(target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= this.attack * this.strength / target.armor;
            }
        }

        // Function to defend next attack
        public void Defend (Character target) {
            this.isDefending = true;
        }


        public void applyOverTimeEffects()
        {
            if (this.poisonedTurns > 0)
            {
                this.poisonedTurns--;
                this.health -= GameVariables.EnemyStats.Spider.poisonDamage;
            }

            if(this.poisonedTurns == 0) {
                this.isPoisoned = false;
            }

            if (this.burningTurns > 0)
            {
                this.burningTurns--;
                this.health -= GameVariables.EnemyStats.Dragon.burningDamage;
            }

            if(this.burningTurns == 0) {
                this.isBurning = false;
            }
        }

        // Print Battle Stats
        public void printBattleStats () {
            Console.WriteLine(" Name: " + this.name);
            Console.WriteLine(" Health: " + this.health);
            Console.WriteLine(" Armor: " + this.armor);
        }

        public void printStats () {
            Console.WriteLine(" Name: " + this.name);
            Console.WriteLine(" Attack: " + this.attack);
            Console.WriteLine(" Strength: " + this.strength);
            Console.WriteLine(" Armor: " + this.armor);
            Console.WriteLine(" Health: " + this.health);
        }

    }

    /*
        ----------------------------------
        |           Enemy Class          |
        ---------------------------------- 
    **/

    public class Enemy : Character
    {
        public int goldOnDefeat;
        public int experienceOnDefeat;
        public int specialAttackCount { get; set; }
        public int battleOptionCount { get; set; } = 2;
        public static string[] attackNames { get; set; }

        public Enemy(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health)
        {
            this.experienceOnDefeat = experienceOnDefeat;
            this.goldOnDefeat = goldOnDefeat;
        }
    
    // fucntion to get a random choice for the enemy
        public int getRandomAttack() {
            Random random = new Random();
            int choice = random.Next(1, (this.specialAttackCount + this.battleOptionCount + 1));
            return choice;
        }

        public new void printBattleStats () {
            Console.WriteLine(" Enemy: " + this.name);
            Console.WriteLine(" Health: " + this.health);
            Console.WriteLine(" Armor: " + this.armor);
        }

        // Function gets overriden in child classes
        public virtual string executeMove(Player target) {
            return "";
        }
    }

    /*
        ----------------------------------
        |           Player Class          |
        ---------------------------------- 
    **/

    public class Player : Character {

    DisplayManager DisplayManager = new DisplayManager();
    public Weapon currentWeapon { get; set; } = new Weapon("Fists", 5, 0, 0);
    // public int level;
    // public int experience;
    // public int experienceToLevelUp;
    // public int maxHealth;
    public int healRating;
    public int endurance = 100;
    public int maxEndurance = 100;
    public int kickAttackStrength = 5;
    public InventoryManager InventoryManager { get; set; } = new InventoryManager();
    public int strengthBuffTurns { get; set; }

    public Player (string name, int attack, double strength, int armor, double health, int maxHealth, int level, int experience, int experienceToLevelUp) 
        : base(name, attack, strength, armor, health) {
        // this.maxHealth = maxHealth;
        // this.level = level;
        // this.experience = experience;
        // this.experienceToLevelUp = experienceToLevelUp;
        this.healRating = 20;
    }

    public void usePotion(string potionType) {
       Item item = this.InventoryManager.getExistingItem(potionType);
       if(item is Potion) {
            Potion potion = (Potion)item;
         if(potion != null) {
              switch (potion.type) {
                case "Health Potion":
                     this.health += potion.effectValue;
                     if(this.health > GameVariables.PlayerStats.maxHealth) {
                          this.health = GameVariables.PlayerStats.maxHealth;
                     }
                     break;
                case "Strength Potion":
                     this.strength += potion.effectValue;
                     break;
                case "Endurance Potion":
                     this.endurance += potion.effectValue;
                     if(this.endurance > this.maxEndurance) {
                          this.endurance = this.maxEndurance;
                     }
                     break;
              }
              this.InventoryManager.removeItem(potion);
         }
       }
    }

    public void decrementBuffs() {
        if(this.strengthBuffed) {
            this.strengthBuffTurns--;
            if(this.strengthBuffTurns <= 0) {
                this.strengthBuffed = false;
                this.strength = 1.0;
            }
        }
    }

    public void Rest() {
        this.health += this.healRating;
        this.endurance += GameVariables.GameSettings.enduranceRegeneration * 2;
        if(this.health > GameVariables.PlayerStats.maxHealth) {
            this.health = GameVariables.PlayerStats.maxHealth;
        }
        if(this.endurance > this.maxEndurance) {
            this.endurance = this.maxEndurance;
        }
    }

    new public void Attack (Character target) {

        if(this.endurance >= this.currentWeapon.enduranceCost) {
            GameVariables.GameLoopBooleans.wasPlayerAttackMade = true;
            if(target.isDefending) {
                this.endurance -= this.currentWeapon.enduranceCost;
                return;
            } else {
                target.health -= this.currentWeapon.attack * this.strength / target.armor;
                this.endurance -= this.currentWeapon.enduranceCost;
            }
        }

        return;
    }

    public void kickAttack (Character target) {

        if(this.endurance > 6) {
            GameVariables.GameLoopBooleans.wasPlayerAttackMade = true;
            if(target.isDefending) {
                this.endurance -= 6;
                return;
            } else {
                target.health -= (this.attack + kickAttackStrength) * this.strength / target.armor ;
                this.endurance -= 6;
            }
        }
        
        return;
    }

    public void useSpecialAttack(Character target) {

        if(this.endurance >= this.currentWeapon.specialAttackEnduranceCost) {
            GameVariables.GameLoopBooleans.wasPlayerAttackMade = true;
            if(target.isDefending) {
                this.endurance -= this.currentWeapon.specialAttackEnduranceCost;
                return;
            } else {
                if(this.currentWeapon != null) {
                    this.currentWeapon.specialAttack(target, this);
                    this.endurance -= this.currentWeapon.specialAttackEnduranceCost ;
                }
            }
        } 

        return;

    }

    public string chooseAttack(Character target) {
        string attackChoice = DisplayManager.displayOptionMenu(" What attack would you like to use?", GameVariables.GameSettings.Options.attackOptions);

        var attackActions = new Dictionary<string, Action<Character>> {
            { "Normal Attack", this.Attack },
            { this.currentWeapon?.specialAttackName, this.useSpecialAttack },
            { "Kick Attack", this.kickAttack }
        };

        if (attackActions.TryGetValue(attackChoice, out var attackAction)) {
            attackAction(target);
        }

        return attackChoice;
    }

    public void setAttackOptions() {
        if(this.currentWeapon != null) {
            GameVariables.GameSettings.Options.attackOptions = new string[] {"Normal Attack", this.currentWeapon.specialAttackName, "Kick Attack"};
        } else {
            GameVariables.GameSettings.Options.attackOptions = new string[] { "Normal Attack", "Kick Attack"};
        }
    }

    //Level rating - how much stats will increase per level
    Hashtable levelUpStats = new Hashtable {
        {"experienceRating", 100},
        {"attackRating", 2},
        {"strengthRating", 1.1},
        {"armorRating", 2},
        {"healthRating", 2},
        {"healRating", 2}
    };

    public void levelUp () {
        if(GameVariables.PlayerStats.level >= GameVariables.PlayerStats.maxLevel) {
            return;
        }
        GameVariables.PlayerStats.level++;
        GameVariables.PlayerStats.experience = 0;
        GameVariables.PlayerStats.experienceToLevelUp = GameVariables.LevelUpRatings.experienceRating * GameVariables.PlayerStats.level;
        GameVariables.PlayerStats.attack += GameVariables.LevelUpRatings.increaseAttackRating * GameVariables.PlayerStats.level;
        GameVariables.PlayerStats.armor += GameVariables.LevelUpRatings.increaseArmorRating;
        GameVariables.PlayerStats.maxHealth += GameVariables.LevelUpRatings.increaseMaxHealthRating * GameVariables.PlayerStats.level;
        GameVariables.PlayerStats.health = GameVariables.PlayerStats.maxHealth;
        this.health = GameVariables.PlayerStats.maxHealth;
    }

    public void gainExperience (int experience) {
        GameVariables.PlayerStats.experience += experience;
        GameVariables.GameStats.totalExperience += experience;
        if (GameVariables.PlayerStats.experience >= GameVariables.PlayerStats.experienceToLevelUp) {
            Console.WriteLine(" You leveled up!");
            this.levelUp();
        }
    }

    public void buyItem (Item item) {
        Item exitingItem = this.InventoryManager.getExistingItem(item.type);

        if(exitingItem != null) {
            if(exitingItem.quantity < exitingItem.maxQuantity) {
                if(this.InventoryManager.gold >= item.price) {
                    this.InventoryManager.gold -= item.price;
                    InventoryManager.addItem(item);
                    Console.WriteLine(" You bought a " + item.type + " for " + item.price + "G.");
                    return;
                } else {
                    Console.WriteLine(" You don't have enough gold.");
                    return;
                }
            } else {
                Console.WriteLine(" You already have the maximum amount of this item.");
                return;
            }
        }

        if(exitingItem == null) {
            if(this.InventoryManager.gold >= item.price) {
                this.InventoryManager.gold -= item.price;
                InventoryManager.addItem(item);
                Console.WriteLine(" You bought a " + item.type + " for " + item.price + "G.");
                return;
            } else {
                Console.WriteLine(" You don't have enough gold.");
                return;
            }
        }
    }

    public string calculateMaxPotionsPurchase (Potion potion) {
        int maxPotions = 0;
        if(this.InventoryManager.gold >= potion.price) {
            maxPotions = this.InventoryManager.gold / potion.price;
        }
        if(maxPotions > potion.maxQuantity) {
            maxPotions = potion.maxQuantity;
        }
        return maxPotions.ToString();
    }

    public void gainGold (int gold) {
        this.InventoryManager.gold += gold;
        GameVariables.GameStats.totalGold += gold;
    }

    // Function to lose gold - if player ran away, lose half of gold
    public void loseGold (int gold) {

        if(GameVariables.GameLoopBooleans.ranAway) {
            this.InventoryManager.gold = this.InventoryManager.gold / 2;
            return;
        }

        if(this.InventoryManager.gold >= gold) {
            this.InventoryManager.gold -= gold;
        } else {
            this.InventoryManager.gold = 0;
        }
    }

    public void printInventory () {
        Console.WriteLine(" Current Weapon: ");
        this.currentWeapon.printWeaponStats();
        Console.WriteLine(" Gold: " + this.InventoryManager.gold);
        Console.WriteLine(" Potions: ");
        this.printPotionsInventory();
    }

    public void printPotionsInventory () {
        Console.WriteLine("     Heal Potions: " + this.InventoryManager.getItemQuantity("Health Potion"));
        Console.WriteLine("     Strength Potions: " + this.InventoryManager.getItemQuantity("Strength Potion"));
        Console.WriteLine("     Endurance Potions: " + this.InventoryManager.getItemQuantity("Endurance Potion"));
    }

    public new void printStats () {
        base.printStats();
        Console.WriteLine(" Endurance: " + this.endurance);
        Console.WriteLine(" Level: " + GameVariables.PlayerStats.level);
        Console.WriteLine(" Experience: " + GameVariables.PlayerStats.experience);
        Console.WriteLine(" Experience to Level Up: " + GameVariables.PlayerStats.experienceToLevelUp);
    }

    new public void printBattleStats () {
        base.printBattleStats();
        Console.WriteLine(" Endurance: " + this.endurance);

        if(this.strengthBuffed) {
            Console.WriteLine($" Strength is buffed for {this.strengthBuffTurns} turns.");
        }

        if(this.isPoisoned) {
            Console.WriteLine($" Poisoned for {this.poisonedTurns} turns.");
        }

        if(this.isBurning) {
            Console.WriteLine($" Burning for {this.burningTurns} turns.");
        }
    }
}
    
    }