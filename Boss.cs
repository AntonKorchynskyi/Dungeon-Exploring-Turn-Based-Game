namespace Final_Project_200524341_ProgFund
{
    public class Boss : Enemy//allows bosses to take stats from normal enemies
    {
        public int countAttack = 2;//allows bosses to attack twice
        public Boss(int hP, int attack, int cAttack) : base(hP, attack)
        {
            this.countAttack = cAttack;
        }
    }
}