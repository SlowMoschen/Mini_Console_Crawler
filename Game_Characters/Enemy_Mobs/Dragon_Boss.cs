using Game_Characters;
using Game_Essentials;

/**
    Class for the Dragon Boss enemy
    Has high stats in general
    Cannot defend
    Can use a special attacks to deal more damage
    fireBreath - deals more damage and sets the player on fire
    throwRock - deals more damage and has a chance to stun the player
    tailStrike - Ignore the player's armor
 */

namespace Boss_Dragon
{
    public class Dragon : Enemy
    {
        public Dragon(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat) : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat)
        {
            this.specialAttackCount = 3;
        }

        public void fireBreath(Player target)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= ((this.attack + GameVariables.EnemyStats.Dragon.fireBreathDamage) * this.strength) / target.armor;
                target.isBurning = true;
                target.burningTurns = 3;
            }
        }

        public void rockThrow(Player target)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= ((this.attack + GameVariables.EnemyStats.Dragon.throwRockDamage) * this.strength) + GameVariables.EnemyStats.Dragon.throwRockDamage / target.armor;
                if(GameVariables.getChance(GameVariables.EnemyStats.Dragon.stunChance)) {
                    target.isStunned = true;
                }
            }
        }

        public void tailStrike(Player target)
        {
            if (target.isDefending)
            {
                target.isDefending = false;
                return;
            }
            else
            {
                target.health -= ((this.attack + GameVariables.EnemyStats.Dragon.tailStrikeDamage) * this.strength);
            }
        }

        public override string executeMove(Player target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.fireBreath(target);
                    move = "fire breath";
                    break;
                case 2:
                    this.rockThrow(target);
                    move = "rock throw";
                    break;
                case 3:
                    this.tailStrike(target);
                    move = "tail strike";
                    break;
                case 4:
                    this.Attack(target);
                    move = "attack";
                    break;
                case 5:
                    this.Attack(target);
                    move = "attack";
                    break;
            }
            return move;
        }
    }
}