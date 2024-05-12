using Positioning;
using Rendering;

namespace TheTileMapEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TileMapEngine.GetInstance().CreateHeadLine("Chess.com");

            TileMapEngine.GetInstance().InitializeChessBoard(7, 7);

            for (int i = 1; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TileMapEngine.GetInstance().Template_Checkers("checkers", "black", new Position(i, j));
                }

                for (int j = 6; j < 8; j++)
                {

                    TileMapEngine.GetInstance().Template_Checkers("checkers", "White", new Position(i, j));
                }
            }

            TileMapEngine.GetInstance().UpdateBoard();
        }
    }
}
