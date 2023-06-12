namespace Final_Project_200524341_ProgFund
{
    public class Enemy
    {
        //stats of normal enemies
        public int enemyHP;
        public int enemyAttack;
        public int enemyHPtempCount;

        public Enemy(int hP, int attack)
        {
            enemyHP = hP;
            enemyAttack = attack;
            enemyHPtempCount = 0;
        }
    }
}