using Game_Characters;
using Game_Essentials;

/**
    Class for the Spider enemy
    Has moderate stats 
    Can use a special attack to poison the player
 */


namespace _Spider 
{
    public class Spider : Enemy {
        public Spider(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            this.specialAttackCount = 1;
        }

        // spit Attack - deals small amount if initial damage and poisons target
        public void spit(Character target)
        {
            if(target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= this.attack * this.strength / target.armor;

                // 25% chance to poison target
                if(GameVariables.getChance(GameVariables.EnemyStats.Spider.poisonChance)) {
                    target.isPoisoned = true;
                    target.poisonedTurns = GameVariables.GameSettings.EffectDurations.poisonDuration;
                }
            }
        }

        // Function to execute a random attack based on the attack count
        public override string executeMove(Player target)
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