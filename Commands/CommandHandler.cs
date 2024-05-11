﻿using Positioning;
using CoreEngineHierarchy;
using Rendering;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;

namespace Commands
{
    public static class CommandHandler
    {
        public static TileObject SelectedTileObject { get; set; }
        public static TileMap CommandtileMap { get; set; }

        private static List<Char> chars = new List<Char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private static void Select(Position position, TileMap tileMap, RenderingEngine renderer)
        {
            CommandtileMap = tileMap;
            Console.WriteLine("Selected Position " + position.X + " , " + position.Y);
            var selectedobject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
            Console.WriteLine("Selected Tile Object " + selectedobject.TileObjectChar);
            foreach (var item in selectedobject.Positions)
            {
                var check = tileMap.TileMapMatrix[item.X, item.Y].Pass(item, tileMap);

                if (check)
                {
                    tileMap.TileMapMatrix[item.X, item.Y].Color = ConsoleColor.Green;
                    Console.WriteLine("[Check] " + check);
                }
                else Console.WriteLine("[Check - False] My char is " + tileMap.TileMapMatrix[item.X, item.Y].CurrentTileObject.TileObjectChar);
            }

            SelectedTileObject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
        }
        private static void DeSelect()
        {
            foreach (var position in SelectedTileObject.Positions)
            {
                if (position.X % 2 == 0 && position.Y % 2 == 0)
                {
                    if (position.X >= 1 && position.Y >= 1 && position.X <= CommandtileMap.TileMapMatrix.GetLength(0) - 1 
                        && position.Y <= CommandtileMap.TileMapMatrix.GetLength(1) - 1) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[position.X, position.Y].Color = ConsoleColor.White;
                    }
                }
                else if (position.X % 2 != 0 && position.Y % 2 != 0)
                {
                    if (position.X >= 1 && position.Y >= 1 && position.X <= CommandtileMap.TileMapMatrix.GetLength(0) - 1
                        && position.Y <= CommandtileMap.TileMapMatrix.GetLength(1) - 1) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[position.X, position.Y].Color = ConsoleColor.White;
                    }
                }
                else
                {
                    if (position.X >= 1 && position.Y >= 1 && position.X <= CommandtileMap.TileMapMatrix.GetLength(0) - 1
                        && position.Y <= CommandtileMap.TileMapMatrix.GetLength(1) - 1) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[position.X, position.Y].Color = ConsoleColor.Gray;
                    }
                }
            }
        }
        private static void ForgetSelected()
        {
            SelectedTileObject = null;
        }

        private static bool TryMoveCommand(Position destiniedLocation)
        {
            foreach (Position pos in SelectedTileObject.Positions)
            {
                if (destiniedLocation.X <= 0 || destiniedLocation.Y <= 0 || destiniedLocation.X >= CommandtileMap.TileMapMatrix.GetLength(0) - 1
                    || destiniedLocation.Y >= CommandtileMap.TileMapMatrix.GetLength(1) - 1) //Doesn't let the player wander outside the board
                {
                    Console.WriteLine("outside the board!");
                    return false;
                }
                if (pos.X == destiniedLocation.X && pos.Y == destiniedLocation.Y) //Add condition to check what is in the destined location and how to handle it
                {
                    DeSelect();
                    CommandtileMap.MoveTileObject(SelectedTileObject, destiniedLocation);
                    Console.WriteLine(SelectedTileObject.Positions[0]);
                    ForgetSelected();
                    return true;
                }
                else
                {
                    Console.WriteLine("wrong position!");
                }
            }
            return false;
        }

        private static void Help()
        {
            Console.WriteLine("//These are the available commands:");
            Console.WriteLine("//1.Select");
            Console.WriteLine("//2.Deselect");
            Console.WriteLine("//3.Move");
        }
        private static string ResetInput()
        {
            var newInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newInput))
                return newInput.ToLower();
            else return "";
        }
        public static void DiagnoseCommand(string input, TileMap tileMap, Rendering.RenderingEngine renderer)
        {

            switch (input.ToLower())
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////
                case "select":
                    Console.WriteLine("Choose a tile");
                    var tile = Console.ReadLine();
                    if (!string.IsNullOrEmpty(tile) && tile.Length == 2)
                    {
                        char first = tile.First();
                        char last = tile.Last();
                        var value = char.GetNumericValue(last);
                        var value2 = char.ToLower(first);
                        if (char.IsLetter(first) && char.IsNumber(last))
                        {
                            Console.WriteLine("Diagnose Print " + value2 + value);
                            Select(new Position(chars.IndexOf(value2) + 1, (int)value), tileMap, renderer);
                        }
                        else
                        {
                            Console.WriteLine("Wrong tile");
                            DiagnoseCommand("select", tileMap, renderer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong tile");
                        DiagnoseCommand("select", tileMap, renderer);
                    }

                    break;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                case "deselect":
                    DeSelect();
                    ForgetSelected();
                    break;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                case "move":
                    Console.WriteLine("Choose a tile");
                    var moveTile = Console.ReadLine();
                    if (!string.IsNullOrEmpty(moveTile) && moveTile.Length == 2)
                    {
                        char first = moveTile.First();
                        char last = moveTile.Last();
                        var value = char.GetNumericValue(last);
                        var value2 = char.ToLower(first);
                        if (char.IsLetter(first) && char.IsNumber(last))
                        {
                            Console.WriteLine("Diagnose Print " + value2 + value);
                            if (TryMoveCommand(new Position(chars.IndexOf(value2) + 1, (int)value)))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong tile");
                                DiagnoseCommand("move", tileMap, renderer);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong tile");
                            DiagnoseCommand("move", tileMap, renderer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong tile");
                        DiagnoseCommand("move", tileMap, renderer);
                    }

                    //MoveCommand(SelectedTileObject.Positions[0]);
                    break;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                case "/help":
                    Help();
                    DiagnoseCommand(ResetInput(), tileMap, renderer);
                    break;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                default:
                    Console.WriteLine("Wrong input");
                    Console.WriteLine("Choose one of the avialable commands");
                    Console.WriteLine("Type /Help to see avialable commands");
                    DiagnoseCommand(ResetInput(), tileMap, renderer);
                    break;
                    ///////////////////////////////////////////////////////////////////////////////////////////////
            }
        }
    }
}
