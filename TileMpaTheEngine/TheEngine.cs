using Rendering;
using CoreEngineHierarchy;
using Positioning;
using MovmentAndInteraction;
using Commands;

namespace TileMpaTheEngine
{
    public class TheEngine
    {
        public int InitialSizeX { get; set; } = 10; // default value
        public int InitialSizeY { get; set; } = 10; // default value


        private static TheEngine _instance;
        private RenderingEngine _renderingEngine;
        private TileMap _addTiles;

        /// <summary>
        /// Template of an headline
        /// </summary>
        /// <param name="Title"></param>
        public void CreateHeadLine(string Title)
        {
            Console.WriteLine("   _   _   _   _   _    ");
            Console.WriteLine("  / \\ / \\ / \\ / \\ / \\   ");
            Console.WriteLine("     " + Title + "   ");
            Console.WriteLine("  \\_/ \\_/ \\_/ \\_/ \\_/   ");
        }


        /// <summary>
        /// singletone
        /// </summary>
        /// <returns></returns>
        private TheEngine()
        {
            _renderingEngine = new RenderingEngine(InitialSizeX, InitialSizeY);
            _addTiles = new TileMap(InitialSizeX, InitialSizeY);
        }

        public static TheEngine GetInstance()
        {
            return _instance ?? (_instance = new TheEngine());
        }

        /// <summary>
        /// Initialize of necessery parameters
        /// </summary>
        /// <returns></returns>
        public void Initialize(int sizeX, int sizeY)
        {
            InitialSizeX = sizeX + 2;
            InitialSizeY = sizeY + 2;
            _renderingEngine = new RenderingEngine(InitialSizeX, InitialSizeY);
            _addTiles = new TileMap(InitialSizeX, InitialSizeY);
        }

        /// <summary>
        /// Set the chess size
        /// </summary>
        /// <returns></returns>
        public RenderingEngine InitializeChessBoard(int sizeX, int sizeY)
        {
            Initialize(sizeX, sizeY);

            return _renderingEngine;
        }


        /// <summary>
        /// Update board
        /// </summary>
        /// <returns></returns>
        public void UpdateBoard()
        {
            _renderingEngine.UpdateAndRender(_addTiles);
        }

        /// <summary>
        /// Create commands
        /// </summary>
        /// <returns></returns>
        public void CreateCommand()
        {
            //CommandHandler.HandleCommands()
        }


        /// <summary>
        /// Allow the consumer an order to move the player
        /// </summary>
        /// <returns></returns>
        public void MoveTo(Position position)
        {
        }

        /// <summary>
        /// Template of checkers that the player could use if want to or put any other string
        /// </summary>
        /// <returns></returns>
        public void Template_Checkers(string gameType, string pieceColor, Position position)
        {
            char pieces = GetPieces(gameType);
            ConsoleColor pieceConsoleColor = GetPieceColor(pieceColor);

            TileObject to1 = new TileObject(pieces.ToString(), new Position[] { }, new Position(1, 1), pieceConsoleColor);
            _addTiles.InsertObjectToMap(to1, position);
        }


        /// <summary>
        /// Piece template
        /// </summary>
        /// <returns></returns>
        private char GetPieces(string gameType)
        {
            switch (gameType.ToLower())
            {
                case "checkers":
                    return 'o';

                default:
                    throw new ArgumentException("Invalid game type", nameof(gameType));
            }
        }

        /// <summary>
        /// Color template
        /// </summary>
        /// <returns></returns>
        private ConsoleColor GetPieceColor(string pieceColor)
        {
            switch (pieceColor.ToLower())
            {
                case "black":
                    return ConsoleColor.Black;
                case "white":
                    return ConsoleColor.DarkYellow;
                default:
                    throw new ArgumentException("Invalid piece color", nameof(pieceColor));
            }
        }
    }


    /// <summary>
    /// All the methods that connected to the player
    /// </summary>
    public static class HundleTurns
    {
        static int NumberOfmovesEachTurn;
        static int NumberOfPlayers;

        static event Action PlayerActions = () => { };

        public static void AddTurns(int numberOfmovesEachTurn, int numberOfPlayers_AI)
        {
            NumberOfmovesEachTurn = numberOfmovesEachTurn;
            NumberOfPlayers = numberOfPlayers_AI;
            SubscribeEventOfTheClass();
            PlayerActions?.Invoke();
        }

        internal static void SubscribeEventOfTheClass()
        {
            PlayerActions += () => HandleTurns();
        }

        internal static void HandleTurns()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                if (i > 0)
                {
                    Console.WriteLine("Player Turn over");
                }
                for (int j = 0; j < NumberOfmovesEachTurn; j++)
                {
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Method to set the number of actors in the game
        /// </summary>
        /// <param name="numberOfActors"></param>
        /// <returns></returns>
        public static int setNumberOfPlayers(int numberOfActors)
        {
            int NumberOfActors = numberOfActors;

            return NumberOfActors;
        }
    }
}
