using Rendering;

namespace TheTileMapEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TileMapEngine.GetInstance().CreateHeadLine("Chess.com");
            TileMapEngine.GetInstance().InitializeChessBoard(8, 8);
            TileMapEngine.GetInstance().Template("checkers", "black", new Positioning.Position(1, 2));
        }
    }
}
