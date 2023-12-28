using Weapons;
using System.Collections;
using Game_Essentials;

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

        public bool isDefending = false;
        public bool isPoisoned = false;
        
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

        public int experienceOnDefeat;
        public int specialAttackCount { get; set; }
        public int battleOptionCount { get; set; } = 2;
        public static string[] attackNames { get; set; }

        public Enemy(string name, int attack, double strength, int armor, double health, int experienceOnDefeat)
            : base(name, attack, strength, armor, health)
        {
            this.experienceOnDefeat = experienceOnDefeat;
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
        public virtual string executeMove(Character target) {
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
    public Weapon currentWeapon { get; set; } = new Weapon("Fists", 5, 0);
    public int level;
    public int experience;
    public int experienceToLevelUp;
    public int maxHealth;
    public int healRating;
    public int endurance = 100;
    public int maxEndurance = 100;
    public int kickAttackStrength = 5;


    public Player (string name, int attack, double strength, int armor, double health, int maxHealth, int level, int experience, int experienceToLevelUp) 
        : base(name, attack, strength, armor, health) {
        this.maxHealth = maxHealth;
        this.level = level;
        this.experience = experience;
        this.experienceToLevelUp = experienceToLevelUp;
        this.healRating = 100;
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

    public void useHealPotion() {
        if(GameVariables.PlayerInventory.healPotions > 0) {
            this.health += GameVariables.GameSettings.healPotionHealRating;
            GameVariables.PlayerInventory.healPotions--;
            if(this.health > this.maxHealth) {
                this.health = this.maxHealth;
            }
        } else {
            Console.WriteLine(" You don't have any heal potions!");
        }
    }

    public void Rest() {
        this.health += this.healRating;
        this.endurance += GameVariables.GameSettings.enduranceRegeneration * 2;
        if(this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
        if(this.endurance > this.maxEndurance) {
            this.endurance = this.maxEndurance;
        }
    }

    new public void Attack (Character target) {

        if(this.endurance >= this.currentWeapon.enduranceCost) {
            if(target.isDefending) {
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
            if(target.isDefending) {
                return;
            } else {
                target.health -= (this.attack + kickAttackStrength) * this.strength / target.armor ;
                this.endurance -= 6;
            }
        }
        
        return;
    }

    public void useSpecialAttack(Character target) {

        if(this.endurance >= this.currentWeapon.enduranceCost) {
            if(target.isDefending) {
                return;
            } else {
                if(this.currentWeapon != null) {
                    this.currentWeapon.specialAttack(target, this);
                    this.endurance -= this.currentWeapon.enduranceCost;
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

    public void levelUp () {
        this.level++;
        this.experience = 0;
        this.experienceToLevelUp = 100 * this.level;
        this.attack += (int)Math.Round((double)this.attack * (int)levelUpStats["attackRating"]! / this.level);
        this.strength = this.strength * (double)levelUpStats["strengthRating"]!;
        this.armor = this.armor * (int)levelUpStats["armorRating"]!;
        this.maxHealth = this.maxHealth * (int)levelUpStats["healthRating"]!;
        this.healRating = healRating * (int)levelUpStats["healthRating"]!;
        this.health = this.maxHealth;
    }

    public void gainExperience (int experience) {
        this.experience += experience;
        GameVariables.GameStats.totalExperience += experience;
        if (this.experience >= this.experienceToLevelUp) {
            Console.WriteLine(" You leveled up!");
            this.levelUp();
        }
    }

    public new void printStats () {
        base.printStats();
        Console.WriteLine(" Current Weapon: " + this.currentWeapon.name);
        Console.WriteLine(" Endurance: " + this.endurance);
        Console.WriteLine(" Level: " + this.level);
        Console.WriteLine(" Experience: " + this.experience);
        Console.WriteLine(" Experience to Level Up: " + this.experienceToLevelUp);
    }

    new public void printBattleStats () {
        base.printBattleStats();
        Console.WriteLine(" Endurance: " + this.endurance);
    }

    //Function to print choosen stat
    public void printStat (string stat) {
        switch (stat) {
            case "name":
                Console.WriteLine(" Name: " + this.name);
                break;
            case "level":
                Console.WriteLine(" Level: " + this.level);
                break;
            case "experience":
                Console.WriteLine(" Experience: " + this.experience);
                break;
            case "experienceToLevelUp":
                Console.WriteLine(" Experience to Level Up: " + this.experienceToLevelUp);
                break;
            case "attack":
                Console.WriteLine(" Attack: " + this.attack);
                break;
            case "strength":
                Console.WriteLine(" Strength: " + this.strength);
                break;
            case "armor":
                Console.WriteLine(" Armor: " + this.armor);
                break;
            case "health":
                Console.WriteLine(" Health: " + this.health);
                break;
            default:
                Console.WriteLine(" Invalid stat.");
                break;
        }
    }

}
    
    }