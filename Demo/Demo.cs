using TheTileMapEngine;

namespace Demo
{
    class Demo
    {
        private static Demo _instance;

        /// <summary>
        /// singletone
        /// </summary>
        /// <returns></returns>
        public static Demo GetInstance()
        {
            return _instance ?? (_instance = new Demo());
        }

        string _choice_player;

        //Make the first scene
        public void OpenScene()
        {
            Console.WriteLine();
            TileMapEngine.GetInstance().CreateHeadLine("Checkers");
            Console.WriteLine();

            string _new_game = "------ To start a game enter 1 ------";

            Console.Write(new string(' ', (Console.WindowWidth - _new_game.Length) / 2));
            Console.WriteLine(_new_game);

            Console.WriteLine();

            string _exit_game = "------ To exit game enter 2 ------";

            Console.Write(new string(' ', (Console.WindowWidth - _exit_game.Length) / 2));
            Console.WriteLine(_exit_game);

            _choice_player = Console.ReadLine();
        }

        public void RunGame()
        {
            OpenScene();

            while (true)
            {
                if (_choice_player == "1")
                {
                    Start_Game();
                }

                else if (_choice_player == "2")
                {
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine("There is no optation like this, try again.");
                    _choice_player = Console.ReadLine();
                }
            }
        }

        public void Start_Game()
        {
            //TileMapEngine.GetInstance().InitializeChessBoard(7, 7);

            //for (int i = 1; i < 8; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        TileMapEngine.GetInstance().Template_Checkers("checkers", "black", new Position(i, j));
            //    }

            //    for (int j = 6; j < 8; j++)
            //    {

            //        TileMapEngine.GetInstance().Template_Checkers("checkers", "White", new Position(i, j));
            //    }
            //}

            //TileMapEngine.GetInstance().UpdateBoard();
        }
    }
}
