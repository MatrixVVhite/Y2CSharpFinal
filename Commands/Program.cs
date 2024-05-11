﻿using CoreEngineHierarchy;
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
            Player p2 = new Player(1, ConsoleColor.Blue, 1, -1);

            TileObject to1 = new TileObject("E", new Position[] { new Position(0, 0), new Position(0, 0) }, new Position(2, 2), p1);
            to1.Positions[0] = new Position(to1.CurrentPos.X + 1 * to1.Owner.MovesToX, to1.CurrentPos.Y + 1 * to1.Owner.MovesToY);
            to1.Positions[1] = new Position(to1.CurrentPos.X - 1 * to1.Owner.MovesToX, to1.CurrentPos.Y + 1 * to1.Owner.MovesToY);
            TileObject to3 = new TileObject("E", new Position[] { new Position(0, 0), new Position(0, 0) }, new Position(4, 4), p1);
            to3.Positions[0] = new Position(to3.CurrentPos.X + 1 * to3.Owner.MovesToX, to3.CurrentPos.Y + 1 * to3.Owner.MovesToY);
            to3.Positions[1] = new Position(to3.CurrentPos.X - 1 * to3.Owner.MovesToX, to3.CurrentPos.Y + 1 * to3.Owner.MovesToY);
            TileObject to2 = new TileObject("P", new Position[] { new Position(0, 0), new Position(0, 0) }, new Position(2, 4), p2);
            to2.Positions[0] = new Position(to2.CurrentPos.X + 1 * to2.Owner.MovesToX, to2.CurrentPos.Y + 1 * to2.Owner.MovesToY);
            to2.Positions[1] = new Position(to2.CurrentPos.X - 1 * to2.Owner.MovesToX, to2.CurrentPos.Y + 1 * to2.Owner.MovesToY);

            List<TileObject> p1List = new List<TileObject>() { to1, to3 };
            p1.AddPieces(p1List);
            List<TileObject> p2List = new List<TileObject>() { to2 };
            p2.AddPieces(p2List);

            foreach (TileObject obj in p1.PiecesOwned)
            {
                myTileMap.InsertObjectToMap(obj, obj.CurrentPos);
            }
            foreach (TileObject obj in p2.PiecesOwned)
            {
                myTileMap.InsertObjectToMap(obj, obj.CurrentPos);
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
