using Game_Characters;
using Game_Essentials;  

namespace _Spider 
{
    public class Spider : Enemy {
        public Spider(string name, int attack, double strength, int armor, double health, int experienceOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat)
        {
            this.specialAttackCount = 1;
        }

        // spit Attack - deals small amount if initial damage and poisons target
        public void spit(Character target)
        {
            if(target.isDefending) {
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;

                // 25% chance to poison target
                if(GameVariables.getChance(25)) {
                    target.isPoisoned = true;
                }
            }
        }

        // Function to execute a random attack based on the attack count
        public override string executeMove(Character target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.spit(target);
                    move = "spit";
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