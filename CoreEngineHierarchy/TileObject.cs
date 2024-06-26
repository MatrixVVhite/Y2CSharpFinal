﻿using Positioning;

namespace CoreEngineHierarchy
{
    public class TileObject : ICloneable
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
        /// Dictionary with the possibale positions on the board
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

        /// <summary>
        /// Clones the tileObject on the same position
        /// </summary>
        public Object Clone()
        {
            return new TileObject(TileObjectChar, Positions, CurrentPos, Owner);
        }

        /// <summary>
        /// Clones the tileObject to a different position
        /// </summary>
        public Object CloneToPos(Position newPos)
        {
            return new TileObject(TileObjectChar, Positions, newPos, Owner);
        }

        /// <summary>
        /// Clones the ownerless tileObject on the same position
        /// </summary>
        public Object CloneOwnerless()
        {
            return new TileObject(TileObjectChar, Positions, CurrentPos, Color);
        }

        /// <summary>
        /// Clones the ownerless tileObject to a different position
        /// </summary>
        public Object CloneToPosOwnerless(Position newPos)
        {
            return new TileObject(TileObjectChar, Positions, newPos, Color);
        }
    }
}
