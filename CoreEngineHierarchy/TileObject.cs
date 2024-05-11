using Positioning;

namespace CoreEngineHierarchy
{
    public class TileObject
    {
        /// <summary>
        /// Properties
        /// </summary>
        public string TileObjectChar { get; set; }
        public Position[] Positions { get; set; }
        public Position CurrentPos { get; set; }
        public Player Owner { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Dictionary with the possibale possitions on the board
        /// </summary>
        public List<Position> PossiblePositions { get; internal set; }

        /// <summary>
        /// Creates a playerless tileObject, commonly used for empty spaces
        /// </summary>
        public TileObject(string tileObjectChar, Position[] tileObjrctPositions, Position startingPos, ConsoleColor color)
        {
            TileObjectChar = tileObjectChar;
            Positions = tileObjrctPositions;
            PossiblePositions = new List<Position>();
            Color = color;

            foreach (Position pos in Positions)
            {
                PossiblePositions.Add(pos);
            }

            CurrentPos = startingPos;
        }

        /// <summary>
        /// Creates a tileObject with a player reference
        /// </summary>
        public TileObject(string tileObjectChar, Position[] tileObjrctPositions, Position startingPos, Player owner)
        {
            TileObjectChar = tileObjectChar;
            Positions = tileObjrctPositions;
            PossiblePositions = new List<Position>();
            Owner = owner;
            Color = owner.PiecesColor;

            foreach (Position pos in Positions)
            {
                PossiblePositions.Add(pos);
            }

            CurrentPos = startingPos;
        }

    }
}
