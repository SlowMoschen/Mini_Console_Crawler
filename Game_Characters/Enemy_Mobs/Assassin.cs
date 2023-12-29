using Game_Characters;

/*
    Class for the Assassin enemy
    Has low health and armor but high attack
    Can use a special attack to deal double damage and ignore target's defense
*/

namespace _Assassin 
{
    public class Assassin : Enemy{
        public Assassin(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            this.specialAttackCount = 1;
        }

        // Backstab Attack - deals double damage and ignores target's defense
        public void backstab(Player target)
        {
            if(target.isDefending) {
                target.isDefending = false;
                target.health -= (this.attack * this.strength / target.armor) * 2;
            } else {
                target.health -= (this.attack * this.strength / target.armor) * 2;
            }
        }

        // Function to execute a random attack based on the attack count
        public override string executeMove(Player target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.backstab(target);
                    move = "backstab";
                    break;
                case 2:
                    this.Attack(target);
                    move = "attack";
                    break;
                case 3:
                    this.Defend(target);
                    move = "defend";
                    break;
            }
            return move;
        }
    }
}