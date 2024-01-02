using Game_Essentials;
using Game_Characters;

namespace _GiantSpider_MiniBoss
{
    public class GiantSpider : Enemy
    {
        public GiantSpider(string name, int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat) : base(name, attack, strength, armor, health, experienceOnDefeat, goldOnDefeat) {
            this.specialAttackCount = 2;
        }

        public void webShot(Player target) {
            int damage = (int)(((this.attack + GameVariables.EnemyStats.GiantSpider.webShotDamage) * this.strength) * (1 - this.CalculateDamageReduction(target.armor)));

            if (target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= damage;
                if(GameVariables.getChance(GameVariables.EnemyStats.GiantSpider.stunChance)) {
                    target.isStunned = true;
                }
            }
        }

        public void poisonBite(Player target) {
            int damage = (int)(((this.attack + GameVariables.EnemyStats.GiantSpider.poisonBiteDamage) * this.strength) * (1 - this.CalculateDamageReduction(target.armor)));


            if (target.isDefending) {
                target.isDefending = false;
                return;
            } else {
                target.health -= damage;
                if(GameVariables.getChance(GameVariables.EnemyStats.GiantSpider.poisonChance)) {
                    target.isPoisoned = true;
                    target.poisonedTurns = 3;
                }
            }
        }

        public override string executeMove(Player target) {
            string move = "";

            switch(getRandomAttack()) {
                case 1:
                    move = "attack";
                    this.Attack(target);
                    break;
                case 2:
                    move = "webShot";
                    this.webShot(target);
                    break;
                case 3:
                    move = "poisonBite";
                    this.poisonBite(target);
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