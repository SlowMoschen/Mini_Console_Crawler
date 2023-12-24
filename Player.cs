using Person;
using System.Collections;
namespace Player 
{


public class Player : Person.Person {

    public int level;
    public int experience;
    public int experienceToLevelUp;
    public int maxHealth;
    public int healRating;

    int kickAttackStrength = 5;
    int slashAttackStrength = 10;

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

    public void slashAttack (Person.Person target) {
        if(target.isDefending) {
            // Console.WriteLine(" The damage was blocked");
            target.isDefending = false;
        } else {
            target.health -= (this.attack * this.strength + slashAttackStrength) / target.armor;
        }
    }

    public void kickAttack (Person.Person target) {
        if(target.isDefending) {
            // Console.WriteLine(" The damage was blocked");
            target.isDefending = false;
        } else {
            target.health -= (this.attack * this.strength + kickAttackStrength) / target.armor ;
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
        if (this.experience >= this.experienceToLevelUp) {
            Console.WriteLine(" You leveled up!");
            this.levelUp();
        }
    }

    public new void printStats () {
        base.printStats();
        Console.WriteLine(" Level: " + this.level);
        Console.WriteLine(" Experience: " + this.experience);
        Console.WriteLine(" Experience to Level Up: " + this.experienceToLevelUp);
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
    
    //Function to get all stats - returns object with all stats
    // public object getStatsValues () {
    //     return new Dictionary<string, float> {
    //         {"level", this.level},
    //         {"experience", this.experience},
    //         {"experienceToLevelUp", this.experienceToLevelUp},
    //         {"attack", this.attack},
    //         {"strength", this.strength},
    //         {"armor", this.armor},
    //         {"health", this.health}
    //     };
    }

}