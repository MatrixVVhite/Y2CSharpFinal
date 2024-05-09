using Positioning;
using Rendering;

namespace TheTileMapEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TileMapEngine.GetInstance().CreateHeadLine("Chess.com");
            (int, int) boardSize = (8, 8);
            TileMapEngine.GetInstance().InitializeChessBoard(boardSize.Item1, boardSize.Item2);

            //Use thos template to generate teams
            Position[] team1Positions = new Position[boardSize.Item1];
            Position[] team2Positions = new Position[boardSize.Item1];

            for (int i = 0; i < team1Positions.Length; i++) //Team1 rows
            {
                if (i % 2 == 0)
                    team1Positions[i] = new Position(i + 1, 1);
                else
                    team1Positions[i] = new Position(i + 1, 2);
            }
            for (int i = 0; i < team2Positions.Length; i++) //Team2 rows
            {
                if (i % 2 == 0)
                    team2Positions[i] = new Position(i + 1, boardSize.Item2 - 1);
                else
                    team2Positions[i] = new Position(i + 1, boardSize.Item2);
            }

            TileMapEngine.GetInstance().Template("checkers", "black", team1Positions, team2Positions);
        }
    }
}
