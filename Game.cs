namespace Final_Project_200524341_ProgFund
{
    public class Game
    {
        int playerHP = 100;//player's current HP
        int playerHPMax = 100;//player's maximum HP
        int attackPower = 25;//player's current attack
        int healingPotionsNum = 4;//number of healing potions
        int attackRaiseNum = 2;//number of temporarily attack raising items
        int attackRaiseNumCount = 0;//count the item's duration
        int tempMaxHPpotion = 2;//number of temporarily HP raising items
        int tempMaxHPpotionCount = 0;//count the item's duration
        int lowerEnemyAttack = 2;//number of enemy's attack lowering items
        int tempMaxHPEnemy = 2;//number of temporarily enemy's HP lowering items
        int battleCount = 0;//how many battles player have won
        int bossCount = 0;//how many bosses player have won
        int escapeFlag = 2;//how many times players can escape in one game
        String[] spaceBetween = { "----------------------------", "/*****************************/", "/////////////////////////////",
                                  "****************************", "//***************************//", "#############################"};
        String[,] bossBattleText = { { "New boss appears! Prepare for battle!", "Boss attacked!" } };
        Random rnd = new Random();

        //checking when player should fight bosses and when player won the game
        public void runGame()
        {
            do
            {
                if (bossCount == 2)
                {
                    Console.WriteLine(spaceBetween[4]);
                    Console.WriteLine("You successfully cleared this dungeon!");
                    Console.WriteLine("You will be returned to the main menu");
                    Console.WriteLine(spaceBetween[4]);
                    break;
                }
                battle();
                if (realPlayerHP() > 0)
                {
                    battleCount++;
                    if (battleCount == 3 || battleCount == 6)
                    {
                        Console.WriteLine("You will have boss battle next!");
                        Console.WriteLine("You will automatically rest before it");
                        Console.WriteLine("Oh yeah, an do not forget that bosses attack twice!");
                        rest();
                        bossBattle();
                        bossCount++;
                        if (realPlayerHP() > 0)
                        {
                            afterBattle();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        afterBattle();
                    }
                }
            }
            while (realPlayerHP() > 0);
            if (bossCount != 2 || realPlayerHP() < 0) {
                Console.WriteLine("Game OVER!");
            }
            
        }

        //for battles with normal enemies
        private void battle()
        {
            String action;
            bool battleStop = true;
            int type = rnd.Next(1, 4);
            Enemy enemy = getEnemy(type);
            Console.WriteLine("");
            Console.WriteLine("New foe appears! Prepare for battle!");
            Console.WriteLine(spaceBetween[5]);
            while (battleStop == true)
            {
                //outputs the stats
                Console.WriteLine(spaceBetween[0]);
                Console.Write("Player's current HP: ");
                Console.WriteLine(realPlayerHP());
                Console.Write("Player's current attack: ");
                Console.WriteLine(realPlayerAttack());
                Console.WriteLine("Enemy's HP is: " + realEnemyHP(enemy));
                Console.WriteLine("Enemy's attack is: " + enemy.enemyAttack);
                Console.WriteLine(spaceBetween[0]);

                //player's battle choices
                Console.WriteLine(spaceBetween[1]);
                Console.WriteLine("Your choices include:");
                Console.WriteLine("Attack");//deal damage to enemy
                Console.WriteLine("Consume");//use consumable
                Console.WriteLine("Escape (you can only escape twice in the game)");//leave the battle, counts as win
                Console.WriteLine("Inventory");//show the number of various consumables
                Console.WriteLine("Input word to perform the command");
                Console.WriteLine(spaceBetween[1]);
                action = Console.ReadLine().ToUpper();
                Console.WriteLine("");
                switch (action)
                {
                    case "ATTACK":
                        enemy.enemyHP -= realPlayerAttack();
                        break;
                    case "CONSUME":
                        consume(enemy);
                        break;
                    case "ESCAPE":
                        if (escapeFlag <= -2)
                        {
                            Console.WriteLine("Sorry, but you can you can escape only twice in the game");
                            Console.WriteLine("");
                            escapeFlag--;
                        }
                        else if (escapeFlag == 2 || escapeFlag == 0)
                        {
                            escapeFlag--;
                            rest();
                        }
                        break;
                    case "INVENTORY":
                        inventory();
                        break;
                    default:
                        Console.WriteLine("Oops, wrong command and enemy exploited that!");
                        Console.WriteLine("Try inputting command from the list above");
                        break;
                }
                //checks when battle should finish
                if (realEnemyHP(enemy) <= 0 || escapeFlag == 1 || escapeFlag == -1)
                {
                    battleStop = false;
                    if (escapeFlag == 1) {
                        escapeFlag = 0;
                    }
                    else if (escapeFlag == -1) {
                        escapeFlag = -2;
                    }
                }
                //enemy attack
                else
                {
                    Console.WriteLine(spaceBetween[1]);
                    Console.WriteLine("Enemy attacked!");
                    playerHP -= enemy.enemyAttack;
                    Console.WriteLine("You lost: " + enemy.enemyAttack + " HP");
                }
                counterTempItems(enemy);//count the duration of items with temporary effects
                Console.WriteLine(spaceBetween[1]);
                //activates when player loses all of the HP
                if (playerHP <= 0)
                {
                    break;
                }
            }
            Console.WriteLine(spaceBetween[5]);
        }

        //outputs number of various items that player has
        private void inventory()
        {

            Console.WriteLine("");

            Console.Write("Number of healing potions: ");
            Console.WriteLine(healingPotionsNum);
            Console.Write("Number of temporarily attack raising potions: ");
            Console.WriteLine(attackRaiseNum);
            Console.Write("Number of temporarily raising player's max HP potions: ");
            Console.WriteLine(tempMaxHPpotion);
            Console.Write("Number of temporarily lowering enemy's max HP items: ");
            Console.WriteLine(tempMaxHPEnemy);
            Console.Write("Number of lowering enemy's attack power items: ");
            Console.WriteLine(lowerEnemyAttack);

            Console.WriteLine("");
        }

        //restores player's HP to maximum
        private void rest()
        {
            Console.WriteLine(spaceBetween[2]);
            playerHP = playerHPMax;
            Console.WriteLine("You have successfully rested and now have full HP");
            Console.WriteLine(spaceBetween[2]);
        }

        //randomly chooses enemy type based on earlier created object
        private Enemy getEnemy(int type)
        {
            int hP;
            int attack;
            switch (type)
            {
                case 1:
                    hP = 50;
                    attack = 40;
                    break;
                case 2:
                    hP = 100;
                    attack = 10;
                    break;
                case 3:
                    hP = 130;
                    attack = 5;
                    break;
                default:
                    hP = 10;
                    attack = 10;
                    break;
            }
            return new Enemy(hP, attack);
        }

        //what happens after each battle
        private void afterBattle()
        {
            Console.WriteLine("You WON!");
            Console.WriteLine("Your next choices are? [rest, explore]");
            switch (Console.ReadLine().ToUpper())
            {
                case "REST":
                    rest();
                    break;
                case "EXPLORE":
                    explore();
                    break;
                default:
                    Console.WriteLine("Oops, wrong command and enemy exploited that!");
                    Console.WriteLine("Try inputting command from the list above");
                    break;
            }
            Console.WriteLine(spaceBetween[5]);
        }

        //letting player find random item
        private void explore()
        {
            Console.WriteLine(spaceBetween[3]);
            int itemType = rnd.Next(1, 4);
            try
            {
                switch (itemType)
                {
                    case 1:
                        Console.WriteLine("You found item that raised your maximum HP!");
                        playerHPMax += 25;
                        playerHP = playerHPMax;
                        break;
                    case 2:
                        Console.WriteLine("You found item that raised your maximum attack!");
                        attackPower += 25;
                        break;
                    case 3:
                        Console.WriteLine("You found one of the consumables!");
                        consGenerator();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Oops, mistake happened, you did not receive anything");
            }
            Console.WriteLine(spaceBetween[3]);
        }

        //specifies what kind of consumable player have previously found
        private void consGenerator()
        {
            Console.WriteLine(spaceBetween[4]);
            int consType = rnd.Next(1, 6);
            try
            {
                switch (consType)
                {
                    case 1:
                        Console.WriteLine("You found healing potion!");
                        healingPotionsNum++;
                        break;
                    case 2:
                        Console.WriteLine("You found item that raises your attack temporarily!");
                        attackRaiseNum++;
                        break;
                    case 3:
                        Console.WriteLine("You found potion that raises your maximum HP temporarily!");
                        tempMaxHPpotion++;
                        break;
                    case 4:
                        Console.WriteLine("You found item that lowers enemy's maximum HP temporarily!");
                        tempMaxHPEnemy++;
                        break;
                    case 5:
                        Console.WriteLine("You found item that lowers enemy's attack!");
                        lowerEnemyAttack++;
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Oops, mistake happened, you did not receive anything");
            }
            Console.WriteLine(spaceBetween[4]);
        }

        //specifies how exactly consumable items should work
        //and allows player to choose one of them
        private void consume(Enemy enemy)
        {
            Console.WriteLine(spaceBetween[1]);
            Console.WriteLine("Choose what item to use");
            Console.WriteLine("Your choices are?");
            Console.WriteLine("1 - healing potion (heal yourself by 25)");
            Console.WriteLine("2 - temporarily raise your attack by  25");
            Console.WriteLine("3 - temporarily raise your maximum HP by 25");
            Console.WriteLine("4 - temporarily lower your enemy's maximum HP by 10");
            Console.WriteLine("5 - lower your enemy's attack by 5");

            switch (Console.ReadLine())
            {
                case "1":
                    if (healingPotionsNum > 0)
                    {
                        healingPotionsNum--;
                        playerHP += 25;
                        Console.WriteLine("You have used healing potion!");
                    }
                    else
                    {
                        Console.WriteLine("You do not have any items of such type left!");
                    }
                    break;
                case "2":

                    if (attackRaiseNum > 0 && attackRaiseNumCount == 0)
                    {
                        attackRaiseNum--;
                        attackRaiseNumCount = 2;
                        Console.WriteLine("You have used item that raises your attack temporarily!");
                    }
                    else
                    {
                        Console.WriteLine("You do not have any items of such type left or you have already used it less then two turns ago!");
                    }
                    break;
                case "3":
                    if (tempMaxHPpotion > 0 && tempMaxHPpotionCount == 0)
                    {
                        tempMaxHPpotion--;
                        tempMaxHPpotionCount = 2;
                        Console.WriteLine("You have used potion that raises your maximum HP temporarily!");
                    }
                    else
                    {
                        Console.WriteLine("You do not have any items of such type left or you have already used it less then two turns ago!");
                    }
                    break;
                case "4":
                    if (tempMaxHPEnemy > 0 && enemy.enemyHPtempCount == 0)
                    {
                        tempMaxHPEnemy--;
                        enemy.enemyHPtempCount = 2;
                        Console.WriteLine("You have used item that lowers enemy's maximum HP temporarily!");
                    }
                    else
                    {
                        Console.WriteLine("You do not have any items of such type left or you have already used it less then two turns ago!");
                    }
                    break;
                case "5":
                    if (lowerEnemyAttack > 0)
                    {
                        lowerEnemyAttack--;
                        enemy.enemyAttack -= 5;
                        Console.WriteLine("You have used item that lowers enemy's attack!");
                    }
                    else
                    {
                        Console.WriteLine("You do not have any items of such type left!");
                    }
                    break;
                default:
                    Console.WriteLine("Oops, mistake happened, and an enemy exploited that");
                    break;
            }
            Console.WriteLine(spaceBetween[1]);
        }

        //counts the real HP of enemy after using a temporarily HP lowering consumable on it
        private int realEnemyHP(Enemy enemy)
        {
            if (enemy.enemyHPtempCount > 0)
            {
                return enemy.enemyHP - 10;
            }
            else
            {
                return enemy.enemyHP;
            }
        }

        //counts the real HP of player after using a temporarily HP raising consumable on him
        private int realPlayerHP()
        {
            if (tempMaxHPpotionCount > 0)
            {
                return playerHP + 25;
            }
            else
            {
                return playerHP;
            }
        }

        //counts the real HP of player after using a temporarily HP raising consumable
        private int realPlayerAttack()
        {
            if (attackRaiseNumCount > 0)
            {
                return attackPower + 25;
            }
            else
            {
                return attackPower;
            }
        }

        //counts the duration of items with temporarily effects
        private void counterTempItems(Enemy enemy)
        {
            if (enemy.enemyHPtempCount > 0)
            {
                enemy.enemyHPtempCount--;
            }
            if (tempMaxHPpotionCount > 0)
            {
                tempMaxHPpotionCount--;
            }
            if (attackRaiseNumCount > 0)
            {
                attackRaiseNumCount--;
            }
        }

        //for battles with bosses
        private void bossBattle()
        {
            String action;
            bool battleStop = true;
            int type = rnd.Next(1, 3);
            Boss boss = getBoss(type);
            Console.WriteLine("");
            Console.WriteLine(bossBattleText[0, 0]);
            Console.WriteLine(spaceBetween[5]);
            while (battleStop == true)
            {
                //outputs the stats
                Console.WriteLine(spaceBetween[0]);
                Console.Write("Player's current HP: ");
                Console.WriteLine(realPlayerHP());
                Console.Write("Player's current attack: ");
                Console.WriteLine(realPlayerAttack());
                Console.WriteLine("Enemy's HP is: " + realEnemyHP(boss));
                Console.WriteLine("Enemy's attack is: " + boss.enemyAttack);
                Console.WriteLine(spaceBetween[0]);

                //player's battle choices
                Console.WriteLine(spaceBetween[1]);
                Console.WriteLine("Your choices include:");
                Console.WriteLine("Attack");
                Console.WriteLine("Consume");
                Console.WriteLine("Inventory");
                Console.WriteLine("Input word to perform the command");
                Console.WriteLine("");
                action = Console.ReadLine().ToUpper();
                Console.WriteLine("");
                switch (action)
                {
                    case "ATTACK":
                        boss.enemyHP -= realPlayerAttack();
                        break;
                    case "CONSUME":
                        consume(boss);
                        break;
                    case "INVENTORY":
                        inventory();
                        break;
                    default:
                        Console.WriteLine("Oops, wrong command and enemy exploited that!");
                        Console.WriteLine("Try inputting command from the list above");
                        break;
                }
                //checks when battle should finish
                if (realEnemyHP(boss) <= 0)
                {
                    battleStop = false;
                }
                //enemy attack
                else
                {
                    for (int i = 0; i < boss.countAttack; i++)
                    {
                        Console.WriteLine(bossBattleText[0, 1]);
                        playerHP -= boss.enemyAttack;
                        Console.WriteLine("You lost: " + boss.enemyAttack + " HP");
                    }
                }

                counterTempItems(boss);//count the duration of items with temporary effects 
                Console.WriteLine(spaceBetween[1]);
                //activates when player loses all of the HP
                if (playerHP <= 0)
                {
                    break;
                }
            }
            Console.WriteLine(spaceBetween[5]);
        }

        //randomly chooses boss type based on earlier created object
        private Boss getBoss(int type)
        {
            int hP;
            int attack;
            switch (type)
            {
                case 1:
                    hP = 200;
                    attack = 10;
                    break;
                case 2:
                    hP = 300;
                    attack = 5;
                    break;
                default:
                    hP = 10;
                    attack = 10;
                    break;
            }
            return new Boss(hP, attack, 2);
        }

    }
}