using Rendering;
using CoreEngineHierarchy;
using Positioning;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;

namespace TheTileMapEngine
{
    public class TileMapEngine
    {
        public static int InitialSizeX { get; set; } = 10; // default value
        public static int InitialSizeY { get; set; } = 10; // default value


        private static TileMapEngine _instance;
        private RenderingEngine _renderingEngine;
        private TileMap _addTiles;

        public void CreateHeadLine(string Title)
        {
            Console.WriteLine("   _   _   _   _   _    ");
            Console.WriteLine("  / \\ / \\ / \\ / \\ / \\   ");
            Console.WriteLine("     " + Title + "   ");
            Console.WriteLine("  \\_/ \\_/ \\_/ \\_/ \\_/   ");
        }


        public TileMapEngine()
        {
            _renderingEngine = new RenderingEngine(InitialSizeX, InitialSizeY);
            _addTiles = new TileMap(InitialSizeX, InitialSizeY);
        }

        public static TileMapEngine GetInstance()
        {
            return _instance ?? (_instance = new TileMapEngine());
        }

        public void Initialize(int sizeX, int sizeY)
        {
            InitialSizeX = sizeX;
            InitialSizeY = sizeY;
            _renderingEngine = new RenderingEngine(InitialSizeX, InitialSizeY);
            _addTiles = new TileMap(InitialSizeX, InitialSizeY);
        }

        public RenderingEngine InitializeChessBoard(int sizeX, int sizeY)
        {
            Initialize(sizeX, sizeY);
            _renderingEngine.ReplaceMap(_addTiles);
            _renderingEngine.DisplayAllTiles();

            return _renderingEngine;
        }

        public void Template(string gameType, string pieceColor, Position position)
        {
            char[] pieces = GetPieces(gameType);
            ConsoleColor pieceConsoleColor = GetPieceColor(pieceColor);

            TileObject to1 = new TileObject(pieces[0].ToString(), new Position[] { }, new Position(1, 1), pieceConsoleColor);
            _addTiles.InsertObjectToMap(to1, position);
            _renderingEngine.ReplaceMap(_addTiles);
            _renderingEngine.DisplayAllTiles();
        }

        private char[] GetPieces(string gameType)
        {
            switch (gameType.ToLower())
            {
                case "checkers":
                    return new char[] { 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o' };

                case "chess":
                    return new char[] { '♜', '♞', '♝', '♛', '♚', '♝', '♞', '♜', '♟', '♟', '♟', '♟', '♟', '♟', '♟', '♙', '♙', '♙', '♙', '♙', '♙', '♙', '♖', '♘', '♗', '♕', '♔', '♗', '♘', '♖' };

                default:
                    throw new ArgumentException("Invalid game type", nameof(gameType));
            }
        }

        private ConsoleColor GetPieceColor(string pieceColor)
        {
            switch (pieceColor.ToLower())
            {
                case "black":
                    return ConsoleColor.Black;
                case "white":
                    return ConsoleColor.White;
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
            // Logic to add as many as the user want actors...
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