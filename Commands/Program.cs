using CoreEngineHierarchy;
using Positioning;
using Rendering;

namespace Commands
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RenderingEngine tmr = new RenderingEngine(8, 8);
            TileMap myTileMap = new TileMap(8, 8);
            TileObject to2 = new TileObject("P", new Position[] { new Position(0, 0), new Position(0, 0) }, new Position(2, 2), ConsoleColor.Red);
            to2.Positions[0] = new Position(to2.CurrentPos.X + 1, to2.CurrentPos.Y + 1);
            to2.Positions[1] = new Position(to2.CurrentPos.X - 1, to2.CurrentPos.Y + 1);
            myTileMap.InsertObjectToMap(to2, to2.CurrentPos);
            tmr.ReplaceMap(myTileMap);
            tmr.DisplayAllTiles();
            string command = Console.ReadLine();
            CommandHandler.DiagnoseCommand(command, myTileMap, tmr);
            tmr.ReplaceMap(myTileMap);
            tmr.DisplayAllTiles();

            while (true)
            {
                command = Console.ReadLine();
                CommandHandler.DiagnoseCommand(command, myTileMap, tmr);
                tmr.ReplaceMap(myTileMap);
                tmr.DisplayAllTiles();
            }
        }
    }
}
