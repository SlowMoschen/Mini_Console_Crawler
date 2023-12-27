using Game_Characters;

namespace Weapons 
{

    public class Weapon {
        public string name;
        public int attack;
        public int enduranceCost;
        public int specialAttackStrength;
        public string specialAttackName;
        public Action<Character, Player> specialAttack;

        public Weapon (string name, int attack, int enduranceCost) {
            this.name = name;
            this.attack = attack;
            this.enduranceCost = enduranceCost;
        }

        public void printWeaponStats () {
            Console.WriteLine(" Name: " + this.name);
            Console.WriteLine(" Attack: " + this.attack);
            Console.WriteLine(" Endurance Consumption: " + this.enduranceCost);
        }
        
    }

    public class Sword : Weapon {
        public Sword (string name, int attack, int enduranceCost) : base(name, attack, enduranceCost) {
            this.specialAttack = this.slash;
            this.specialAttackStrength = 20;
            this.specialAttackName = "Slash";
        }

        public void slash(Character target, Player player)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
            }
            else
            {
                target.health -= (this.attack + this.specialAttackStrength * player.strength) / target.armor;
            }
        }
    }

    public class Axe : Weapon {
        public Axe (string name, int attack, int enduranceCost) : base(name, attack, enduranceCost) {
            this.specialAttack = this.chop;
            this.specialAttackStrength = 30;
            this.specialAttackName = "Chop";
        }

        public void chop(Character target, Player player)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
            }
            else
            {
                target.health -= (this.attack + this.specialAttackStrength * player.strength) / target.armor;
            }
        }
    }

    public class Mace : Weapon {
        public Mace (string name, int attack, int enduranceCost) : base(name, attack, enduranceCost) {
            this.specialAttack = this.bash;
            this.specialAttackStrength = 23;
            this.specialAttackName = "Bash";
        }

        public void bash(Character target, Player player)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
            }
            else
            {
                target.health -= (this.attack + this.specialAttackStrength * player.strength) / target.armor;
            }
        }
    }
}