using Game_Characters;
using Game_Essentials;

/**
    Class for the Goblin enemy
    Has low stats in general
    Can use a special attack to steal gold from the player
 */

namespace _Goblin 
{
    public class Goblin : Enemy {

        public Goblin(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            specialAttackCount = 1;
        }

        public void steal(Player target)
        {
            if(target.isDefending) {
                target.isDefending = false;
                target.loseGold(GameVariables.EnemyStats.Goblin.stealAmount);
                return;
            } else {
                GameVariables.GameLoopBooleans.wasEnemyAttackMade = true;
                target.health -= this.attack * this.strength / target.armor;
                target.loseGold(GameVariables.EnemyStats.Goblin.stealAmount);
            }
        }

        // Function to execute a random attack based on the attack count
        public override string executeMove(Player target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.steal(target);
                    move = "steal";
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