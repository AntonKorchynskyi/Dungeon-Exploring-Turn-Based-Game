namespace Final_Project_200524341_ProgFund;
class Program
{
    //main menu
    static void Main(string[] args)
    {
        bool gameStop = true;
        String menuChoice;
        while (gameStop == true)
        {
            Console.WriteLine("Hello, fellow explorer!");
            Console.WriteLine("This time you need to clear this dangerous dungeon by defeating two bosses that appear after third and sixth battles respectively!");
            Console.WriteLine("Here is the list of your possible choices:");
            Console.WriteLine("1. Start exploring and clearing the dungeon");
            Console.WriteLine("2. Exit the game");
            Console.WriteLine("Input the number of option to proceed");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    Game game = new Game();
                    game.runGame();
                    break;
                case "2":
                    Console.WriteLine("You successfully exited the game");
                    gameStop = !gameStop;
                    break;
                default:
                    Console.WriteLine("Hey, the only possible choices are 1 and 2");
                    Console.WriteLine("Please, input again");
                    break;
            }
        }
    }

}
