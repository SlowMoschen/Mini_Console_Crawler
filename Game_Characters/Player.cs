using Person;
using Weapons;
using System.Collections;
using Game_Essentials;
namespace Player 
{


public class Player : Person.Person {

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
        this.healRating = 10;
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

    public void Heal() {
        this.health += this.healRating;
        if(this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
    }

    public void Defend (Person.Person target) {
        this.isDefending = true;
    }

    new public void Attack (Person.Person target) {

        if(this.endurance < this.currentWeapon.enduranceCost) {
            Console.WriteLine(" You don't have enough endurance to attack!");
            return;
        }

        if(target.isDefending) {
            target.isDefending = false;
        } else {
            target.health -= this.currentWeapon.attack * this.strength / target.armor;
            this.endurance -= this.currentWeapon.enduranceCost;
        }
    }

    public void kickAttack (Person.Person target) {

        if(this.endurance < 6) {
            Console.WriteLine(" You don't have enough endurance to attack!");
            return;
        }

        if(target.isDefending) {
            target.isDefending = false;
        } else {
            target.health -= (this.attack + kickAttackStrength) * this.strength / target.armor ;
            this.endurance -= 6;
        }
    }

    public void useSpecialAttack(Person.Person target) {
        if(this.currentWeapon != null) {
            this.currentWeapon.specialAttack(target, this);
        } else {
            Console.WriteLine(" You don't have a weapon equipped!");
        }
    }

    public string chooseAttack(Person.Person target) {
        string attackChoice = DisplayManager.displayOptionMenu(" What attack would you like to use?", GameVariables.GameSettings.Options.attackOptions);

        var attackActions = new Dictionary<string, Action<Person.Person>> {
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