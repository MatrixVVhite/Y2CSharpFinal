using Positioning;

namespace CoreEngineHeirarchy
{
    public class TileObject
    {
        /// <summary>
        /// Properties
        /// </summary>
        public string TileObjectChar { get; set; }
        public Position[] Positions { get; set; }
        public Position CurrentPos { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Dictionary with the possibale possitions on the board
        /// </summary>
        public List<Position> PossiblePositions { get; internal set; }

        /// <summary>
        /// Dictionary with the possibale possitions on the board
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

    }
}
