using Game_Characters;
using Game_Essentials;

/**
*
*   Weapons
*   - Contains all weapons and their stats
*   - All stats are stored in GameVariables.cs
*
*   @param string name - The name of the weapon
*   @param int attack - The attack of the weapon
*   @param int enduranceCost - The endurance cost of the weapon
*   @param int specialAttackEnduranceCost - The endurance cost of the special attack of the weapon
*   @param int specialAttackStrength - The strength of the special attack of the weapon
*   @param string specialAttackName - The name of the special attack of the weapon
*   @param Action<Character, Player> specialAttack - The special attack of the weapon
*
*/

namespace Weapons 
{

    public class Weapon {
        public string name;
        public int attack;
        public int enduranceCost;
        public int specialAttackEnduranceCost;
        public int specialAttackStrength;
        public string specialAttackName;
        public Action<Character, Player> specialAttack;

        public Weapon (string name, int attack, int enduranceCost, int specialAttackEnduranceCost) {
            this.name = name;
            this.attack = attack;
            this.enduranceCost = enduranceCost;
            this.specialAttackEnduranceCost = specialAttackEnduranceCost;
        }

        public void printWeaponStats () {
            Console.WriteLine("     Name: " + this.name);
            Console.WriteLine("     Attack: " + this.attack);
            Console.WriteLine("     Special Attack: " + this.specialAttackStrength);
            Console.WriteLine("     Endurance Consumption: " + this.enduranceCost);
            Console.WriteLine("     Special Attack Endurance Consumption: " + this.specialAttackEnduranceCost);
        }
        
    }

    public class Sword : Weapon {
        public Sword (string name, int attack, int enduranceCost, int specialAttackEnduranceCost) : base(name, attack, enduranceCost, specialAttackEnduranceCost) {
            this.specialAttack = this.slash;
            this.specialAttackStrength = GameVariables.WeaponStats.Sword.getSpecialAttackStrength();
            this.specialAttackName = "Slash";
        }

        public void slash(Character target, Player player)
        {
            int damage = (int)(((this.attack + this.specialAttackStrength) * player.strength) * (1 - player.CalculateDamageReduction(target.armor)));

            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= damage;
            }
        }
    }

    public class Axe : Weapon {
        public Axe (string name, int attack, int enduranceCost, int specialAttackEnduranceCost) : base(name, attack, enduranceCost, specialAttackEnduranceCost) {
            this.specialAttack = this.chop;
            this.specialAttackStrength = GameVariables.WeaponStats.Axe.getSpecialAttackStrength();
            this.specialAttackName = "Chop";
        }

        public void chop(Character target, Player player)
        {
            int damage = (int)(((this.attack + this.specialAttackStrength) * player.strength) * (1 - player.CalculateDamageReduction(target.armor)));

            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= damage;
            }
        }
    }

    public class Mace : Weapon {
        public Mace (string name, int attack, int enduranceCost, int specialAttackEnduranceCost) : base(name, attack, enduranceCost, specialAttackEnduranceCost) {
            this.specialAttack = this.bash;
            this.specialAttackStrength = GameVariables.WeaponStats.Mace.getSpecialAttackStrength();
            this.specialAttackName = "Bash";
        }

        public void bash(Character target, Player player)
        {
            int damage = (int)(((this.attack + this.specialAttackStrength) * player.strength) * (1 - player.CalculateDamageReduction(target.armor)));

            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= damage;
            }
        }
    }
}