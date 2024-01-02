using Game_Characters;
using Game_Essentials;

/**
    Class for the Demonic Sorcerer enemy
    Has high stats in general
    but nearly no armor
    hellFireBlast - deals more damage and has a chance to set the player on fire
    darkPact - Adds a set percentage of damage to the Demonic Sorcerer's attack and decreases its health by a set percentage
 */

namespace _DemonicSorcerer_MiniBoss
{
    public class DemonicSorcerer : Enemy 
    {

        public DemonicSorcerer(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat) : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat) {
            this.specialAttackCount = 2;
        }

        public void hellFireBlast(Player target) {
            int damage = (int)(((this.attack + GameVariables.EnemyStats.DemonicSorcerer.hellFireBlastDamage) * this.strength) * (1 - this.CalculateDamageReduction(target.armor)));

            if (target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= damage;
                if(GameVariables.getChance(GameVariables.EnemyStats.DemonicSorcerer.burnChance)) {
                    target.isBurning = true;
                    target.burningTurns = 3;
                }
            }
        }

        public void darkPact() {
            this.attack += (int)(this.attack * GameVariables.EnemyStats.DemonicSorcerer.darkPactAttackPercentage);
            this.health -= (int)(this.health * GameVariables.EnemyStats.DemonicSorcerer.darkPactHealthPercentage);
        }

        public override string executeMove(Player target) {
            string move = "";

            switch(getRandomAttack()) {
                case 1:
                    move = "attack";
                    this.Attack(target);
                    break;
                case 2:
                    move = "hellFireBlast";
                    this.hellFireBlast(target);
                    break;
                case 3:
                    move = "darkPact";
                    this.darkPact();
                    break;
                case 4:
                    move = "defend";
                    this.Defend(target);
                    break;
            }

            return move;
        }
    }
}