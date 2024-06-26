﻿using Positioning;

namespace CoreEngineHeirarchy
{
    public interface ITileAble
    {
        /// <summary>
        /// Give the background to the function which determen if the tile object can move to the next square or not
        /// </summary>
        bool Land(Position finalDestination);
        bool Pass(Position destination, TileMap currentMap);
    }
}
