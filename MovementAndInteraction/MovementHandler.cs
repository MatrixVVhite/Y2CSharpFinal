using CoreEngineHierarchy;
using Positioning;
using Commands;
using Rendering;

namespace MovementAndInteraction
{
    public class MovementHandler
    {
        //Where to move
        public void MovrAction(TileMap newtileMap)
        {
            RenderingEngine renderingEngine = new RenderingEngine(0, 0);
            newtileMap.MoveTileObject(CommandHandler.SelectedTileObject, new Position(1, 1));
            renderingEngine.ReplaceMap(newtileMap);
            renderingEngine.DisplayAllTiles();
        }

        //Check collision
        public void OnCollision()
        {
        }
    }
}
