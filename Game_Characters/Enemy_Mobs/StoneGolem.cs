using Game_Characters;
using Game_Essentials;

/**
    Class for the StoneGolem enemy
    Has much health and armor but low attack
    Can use a special attack to stun the player
    Cant defend
 */


namespace _StoneGolem 
{
    public class StoneGolem : Enemy {
        public StoneGolem(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            specialAttackCount = 1;
        }

        // slam Attack - deals small amount if initial damage and stuns target
        public void slam(Character target)
        {
            if(target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= this.attack * this.strength / target.armor;

                // 15% chance to stun target
                if(GameVariables.getChance(GameVariables.EnemyStats.StoneGolem.stunChance)) {
                    target.isStunned = true;
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
                    this.slam(target);
                    move = "slam";
                    break;
                case 2:
                    this.Attack(target);
                    move = "attack";
                    break;
                case 3:
                    this.Attack(target);
                    move = "attack";
                    break;
            }
            return move;
        }
    }
}