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
            InitialSizeX = sizeX + 2;
            InitialSizeY = sizeY + 2;
            _renderingEngine = new RenderingEngine(InitialSizeX, InitialSizeY);
            _addTiles = new TileMap(InitialSizeX, InitialSizeY);
        }

        public RenderingEngine InitializeChessBoard(int sizeX, int sizeY)
        {
            Initialize(sizeX, sizeY);
            _renderingEngine.UpdateAndRender(_addTiles);

            return _renderingEngine;
        }

        public void Template(string gameType, string pieceColor, Position[] team1Positions, Position[] team2Positions)
        { //TODO Make it work with chess & 2 colors
            char[] pieces = GetPieces(gameType);
            ConsoleColor pieceConsoleColor = GetPieceColor(pieceColor);

            for (int i = 0; i < team1Positions.Length; i++)
            {
                TileObject to = new TileObject(pieces[i].ToString(), new Position[] { }, new Position(4, 4), pieceConsoleColor);
                _addTiles.InsertObjectToMap(to, team1Positions[i]);
            }

            for (int i = 0; i < team2Positions.Length; i++)
            {
                TileObject to = new TileObject(pieces[i].ToString(), new Position[] { }, new Position(4, 4), pieceConsoleColor);
                _addTiles.InsertObjectToMap(to, team2Positions[i]);
            }

            _renderingEngine.UpdateAndRender(_addTiles);
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