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
            Player p1 = new Player(0, ConsoleColor.Red, 1, 1);
            TileObject to2 = new TileObject("P", new Position[] { new Position(0, 0), new Position(0, 0) }, new Position(2, 2), p1);
            to2.Positions[0] = new Position(to2.CurrentPos.X + 1 * to2.Owner.MovesToX, to2.CurrentPos.Y + 1 * to2.Owner.MovesToY);
            to2.Positions[1] = new Position(to2.CurrentPos.X - 1 * to2.Owner.MovesToX, to2.CurrentPos.Y + 1 * to2.Owner.MovesToY);
            List<TileObject> objectList = new List<TileObject>() { to2 };
            p1.AddPieces(objectList);
            foreach (TileObject obj in p1.PiecesOwned)
            {
                myTileMap.InsertObjectToMap(to2, to2.CurrentPos);
            }
            tmr.UpdateAndRender(myTileMap);
            string command = Console.ReadLine();
            CommandHandler.DiagnoseCommand(command, myTileMap, tmr);
            tmr.UpdateAndRender(myTileMap);

            while (true)
            {
                command = Console.ReadLine();
                CommandHandler.DiagnoseCommand(command, myTileMap, tmr);
                tmr.UpdateAndRender(myTileMap);
            }
        }
    }
}
