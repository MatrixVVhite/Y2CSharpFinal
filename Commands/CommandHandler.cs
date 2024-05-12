using Positioning;
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

        public static List<Command> commandList= [];
        public static Action<Command> AddNameDescription; //Name and description of the new commands


        public static void HandleCommands()
        {
            AddNameDescription += AddToHelpList;
            Command SelectCommand = new("Select", "Does Something", Select);
            AddCommandNameAndDescription(SelectCommand);
            Command Move = new("Move", "Does Something", TryMoveCommand);
            AddCommandNameAndDescription(Move);
            Command DeselectCommand = new("Deselct", "Does Something",DeSelect);
            AddCommandNameAndDescription(DeselectCommand);
        }

        //Use this method to add your command to the command list
        public static void  AddCommandNameAndDescription(Command newCommand)
        {
            AddNameDescription.Invoke(newCommand);
        }

        public static void  Example(object xy)
        {
            Console.WriteLine(xy);
        }
        public static  void  AddToHelpList(Command command)
        {
            commandList.Add(command);
        }
        public static void Help()
        {
            foreach (var item in commandList)
            {
                Console.WriteLine(item.Name + " " + item.Description);
                Console.WriteLine("--------------");
            }
        }


        private static void Select(Position position, TileMap tileMap, RenderingEngine renderer)
        {
            CommandtileMap = tileMap;
            Console.WriteLine("Selected Position " + position.X + " , " + position.Y);
            var selectedobject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
            Console.WriteLine("Selected Tile Object " + selectedobject.TileObjectChar);
            foreach (var item in selectedobject.Positions)
            {
                Position destination = new Position(position.X + item.X, position.Y + item.Y);
                var check = tileMap.TileMapMatrix[destination.X, destination.Y].Pass(position, destination, tileMap);

                if (check)
                {
                    tileMap.TileMapMatrix[destination.X, destination.Y].Color = ConsoleColor.Green;
                    Console.WriteLine("[Check] " + check);
                }
                else Console.WriteLine("[Check - False] My char is " + tileMap.TileMapMatrix[destination.X, destination.Y].CurrentTileObject.TileObjectChar);
            }

            SelectedTileObject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
        }
        private static void DeSelect()
        {
            Position currentPos = new Position(SelectedTileObject.CurrentPos.X, SelectedTileObject.CurrentPos.Y);
            foreach (var item in SelectedTileObject.Positions)
            {
                Position deleteAt = new Position(currentPos.X + item.X, currentPos.Y + item.Y);
                if (deleteAt.X % 2 == 0 && deleteAt.Y % 2 == 0)
                {
                    var check = CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Pass(currentPos, deleteAt, CommandtileMap);
                    if (check) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Color = ConsoleColor.White;
                    }
                }
                else if (deleteAt.X % 2 != 0 && deleteAt.Y % 2 != 0)
                {
                    var check = CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Pass(currentPos, deleteAt, CommandtileMap);
                    if (check) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Color = ConsoleColor.White;
                    }
                }
                else
                {
                    var check = CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Pass(currentPos, deleteAt, CommandtileMap);
                    if (check) //checks if target is a border tile, if it isn't, dye it back
                    {
                        CommandtileMap.TileMapMatrix[deleteAt.X, deleteAt.Y].Color = ConsoleColor.Gray;
                    }
                }
            }
        }
        private static void ForgetSelected()
        {
            SelectedTileObject = null;
        }

        private static bool TryMoveCommand(Position destinedLocation)
        {
            Position currentPos = new Position(SelectedTileObject.CurrentPos.X, SelectedTileObject.CurrentPos.Y);
            foreach (Position item in SelectedTileObject.Positions)
            {
                var check = CommandtileMap.TileMapMatrix[destinedLocation.X, destinedLocation.Y].Pass(currentPos, destinedLocation, CommandtileMap);

                if (!check)
                {
                    Console.WriteLine("can't move there!");
                    return false;
                }
                if (item.X + currentPos.X == destinedLocation.X && item.Y + currentPos.Y == destinedLocation.Y)
                {
                    DeSelect();
                    CommandtileMap.MoveTileObject(SelectedTileObject, destinedLocation);
                    Console.WriteLine(SelectedTileObject.Positions[0].X + SelectedTileObject.CurrentPos.X + ", " + SelectedTileObject.Positions[0].Y + SelectedTileObject.CurrentPos.Y);
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
                            commandList[0].select.Invoke(new Position(chars.IndexOf(value2) + 1, (int)value), tileMap, renderer);
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
                    //DeSelect();
                    commandList[2].Action.Invoke();
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
                            if (commandList[1].move.Invoke(new Position(chars.IndexOf(value2) + 1, (int)value)))
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
