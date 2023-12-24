using Enemy;

namespace Zombie
{
    public class Zombie : Enemy.Enemy
    {
        public Zombie(string name, int attack, double strength, int armor, double health, int experienceOnDefeat)
            : base(name, attack, strength, armor, health, experienceOnDefeat)
        {
            attackCount = 2;
        }

        // Bite Attack - heals zombie for half the damage dealt
        public void bite(Person.Person target)
        {
            if(target.isDefending) {
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;
            }
        }

        // Thrash Attack - deals damage to target and self
        public void thrash(Person.Person target)
        {
            if(target.isDefending) {
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;
            }
        }

        // Function to execute a random attack based on the attack count
        public string executeMove(Person.Person target)
        {
            string move = "";
            switch (getRandomAttack())
            {
                case 1:
                    this.bite(target);
                    move = "bite";
                    break;
                case 2:
                    this.thrash(target);
                    move = "thrash";
                    break;
                case 3:
                    this.Attack(target);
                    move = "attack";
                    break;
                case 4:
                    this.Defend(target);
                    move = "defend";
                    break;
            }
            return move;
        }

    }
}