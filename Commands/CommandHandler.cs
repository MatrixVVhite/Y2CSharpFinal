using Positioning;
using CoreEngineHierarchy;
using Rendering;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Commands
{
    public static class CommandHandler
    {
        private static TileObject SelectedTileObject { get; set; } //ref
        private static TileMap CommandtileMap { get; set; } //ref

        private static List<Char> chars = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h']; //used for select

        private static List<Command> commandList= []; //List Of Commands avialable for use

        private static Action<Command> AddNameDescription; //Name and description of the new commands

        /// <summary>
        /// This method handles all the commands.
        /// Create a new Command (Class) and use method AddCommandNameAndDescription with (New Command instance inside)
        /// </summary>
        public static void HandleCommands()
        {
            AddNameDescription += AddToHelpList;
            Command SelectCommand = new("Select","Select your desired pawn", Select,true);
            AddCommandNameAndDescription(SelectCommand);
            Command DeselectCommand = new("Deselect","Deselect your selected pawn",DeSelect,false);
            AddCommandNameAndDescription(DeselectCommand);
            Command MoveCommand = new("Move", "Move your selected pawn to a marked position of your choice", TryMoveCommand,true);
            AddCommandNameAndDescription(MoveCommand);
            Command HelpCommand = new("Help", "Display all commands", Help, false);
            AddCommandNameAndDescription(HelpCommand);
        }

        //Use this method to add your command to the command list
        private static void  AddCommandNameAndDescription(Command newCommand)
        {
            AddNameDescription.Invoke(newCommand);
        }
        /// <summary>
        /// This adds the commands to the help list. Invoked by AddCommandNameAndDescription
        /// </summary>
        /// <param name="command"></param>
        private static  void  AddToHelpList(Command command)
        {
            commandList.Add(command);
        }
        private static bool Help(Position position, TileMap tileMap, RenderingEngine renderer)
        {
            int index = 1;
            foreach (var item in commandList)
            {
                Console.SetCursorPosition((chars.Count+2)*3,index);
                Console.WriteLine(item.Name + " - " + item.Description);
                index++;
            }
            Console.SetCursorPosition((chars.Count + 2) * 3, index+1);
            Console.WriteLine("Press Any key To continue...");
            Console.SetCursorPosition(0, (chars.Count + 1));
            Console.ReadKey();
            return false;
        }


        private static bool Select(Position position, TileMap tileMap, RenderingEngine renderer)
        {
            CommandtileMap = tileMap;
            Console.WriteLine("Selected Position " + position.X + " , " + position.Y);
            var selectedobject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
            Console.WriteLine("Selected Tile Object " + selectedobject.TileObjectChar);
            foreach (var item in selectedobject.Positions)
            {
                Position destination = new (position.X + item.X, position.Y + item.Y);
                var check = tileMap.TileMapMatrix[destination.X, destination.Y].Pass(position, destination, tileMap);

                if (check)
                {
                    tileMap.TileMapMatrix[destination.X, destination.Y].Color = ConsoleColor.Green;
                }
            }

            SelectedTileObject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;

            return true;
        }
        
        private static bool DeSelect(Position position, TileMap tileMap, RenderingEngine renderer)
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
            return true;
        }
        private static bool TryMoveCommand(Position destinedLocation, TileMap tileMap, RenderingEngine renderer)
        {
            if(SelectedTileObject != null)
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
                     
                        DeSelect(destinedLocation,tileMap,renderer);
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
            }
            return false;
        }
        private static void ForgetSelected()
        {
            SelectedTileObject = null;
        }

        private static (int, int) ReturnPosition()
        {
            
            Console.WriteLine("Choose a tile");
            var tile = Console.ReadLine();
            if (!string.IsNullOrEmpty(tile) && tile.Length == 2)
            {
                char first = tile.First();
                char last = tile.Last();
                var x = char.ToLower(first);
                var y = char.GetNumericValue(last);
                if (char.IsLetter(first) && char.IsNumber(last) && chars.IndexOf(x) + 1 <= chars.Count - 2 && y <= chars.Count-2 )
                {
                    Console.WriteLine("Diagnose Print " + x + y);
                    return (chars.IndexOf(x) + 1, (int)y);
                }
                else
                {
                    Console.WriteLine("Wrong position");
                    Console.WriteLine("Press Any key To continue...");
                    Console.ReadKey();
                }
            }
            return (0, 0);
        }

        public static void DiagnoseCommand(string input, TileMap tileMap, Rendering.RenderingEngine renderer)
        {
            bool commandSucc=false ;
           
            for (int i = 0; i < commandList.Count; i++)
            {
                if (input.ToLower() == commandList[i].Name.ToLower() && commandList[i].PosReq)
                {
                    commandSucc = true;
                    var xy = ReturnPosition();
                    commandList[i].Execute.Invoke(new Position(xy.Item1, xy.Item2), tileMap, renderer);
                    break;
                }
                else if (input.ToLower() == commandList[i].Name.ToLower() && !commandList[i].PosReq)
                {
                    commandSucc = true;
                    commandList[i].Execute.Invoke(new Position(0,0), tileMap, renderer);
                    break;
                }
                else commandSucc = false;
            }
            if (!commandSucc)
            {
                renderer.UpdateAndRender(tileMap);
                Help(new Position(0, 0), tileMap, renderer);
                renderer.UpdateAndRender(tileMap);
                Console.SetCursorPosition(0, (chars.Count + 1));
                DiagnoseCommand(Console.ReadLine(), tileMap, renderer);
            } 
        }
    }
}
