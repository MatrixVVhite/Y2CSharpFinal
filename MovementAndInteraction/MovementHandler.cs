using CoreEngineHierarchy;
using Positioning;
using Rendering;
using NewTileMapEngine; 


namespace MovementAndInteraction
{
    public class MovementHandler
    {
        public TileObject SelectedTileObject { get; set; } //ref
        public TileMap CommandtileMap { get; set; } //ref
        
        public List<Char> chars = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h','i','j']; //used for select

        //Where to move
        public void MoveAction(TileMap newtileMap)
        {
            RenderingEngine renderingEngine = new RenderingEngine(0, 0);
            newtileMap.MoveTileObject(SelectedTileObject, new Position(1, 1));
            //renderingEngine.ReplaceMap(newtileMap);
            //renderingEngine.DisplayAllTiles();
        }

        public bool TryMoveCommand(Position destinedLocation, TileMap tileMap, RenderingEngine renderer)
        {
            if (SelectedTileObject != null)
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

                        DeSelect(destinedLocation, tileMap, renderer);
                        CommandtileMap.MoveTileObject(SelectedTileObject, destinedLocation);
                        Console.WriteLine(SelectedTileObject.Positions[0].X + SelectedTileObject.CurrentPos.X + ", " + SelectedTileObject.Positions[0].Y + SelectedTileObject.CurrentPos.Y);
                        ForgetSelected();
                        if (HundleTurns.CurrentPlayer < HundleTurns.NumberOfPlayers)
                        {
                            HundleTurns.CurrentPlayer++;
                        }
                        else
                        {
                            HundleTurns.CurrentPlayer = 1;
                        }
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

        public bool Select(Position position, TileMap tileMap, RenderingEngine renderer)
        {
            CommandtileMap = tileMap;
            Console.WriteLine("Selected Position " + position.X + " , " + position.Y);
            var selectedobject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
            if (selectedobject.Owner.PlayerID == HundleTurns.CurrentPlayer)
            {
                Console.WriteLine("Selected Tile Object " + selectedobject.TileObjectChar);
                foreach (var item in selectedobject.Positions)
                {
                    Position destination = new(position.X + item.X, position.Y + item.Y);
                    var check = tileMap.TileMapMatrix[destination.X, destination.Y].Pass(position, destination, tileMap);

                    if (check)
                    {
                        tileMap.TileMapMatrix[destination.X, destination.Y].Color = ConsoleColor.Green;

                    }
                }

                SelectedTileObject = tileMap.TileMapMatrix[position.X, position.Y].CurrentTileObject;
                

                return true;
            }
            else
            {
                Console.WriteLine( "Wait! its not your turn");
                Console.ReadKey();
            } 
            return false;
        }
        public bool DeSelect(Position position, TileMap tileMap, RenderingEngine renderer)
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
        public void ForgetSelected()
        {
            SelectedTileObject = null;
        }

        //Check collision
        public void OnCollision()
        {
        }
    }
}
