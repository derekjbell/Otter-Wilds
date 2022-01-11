using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles
{
    public enum WallType
    {
        Wall,
        LockedDoor,
        Rock,
        None
    }

    public enum Item
    {
        Key,
        Bug,
        None
    }

    static public WallType LookupWallType(TileBase tile)
    {
        if (tile == null)
        {
            return WallType.None;
        }
        else if (tile.name.StartsWith("Terrain-"))
        {
            return WallType.Wall;
        }
        else if (tile.name.StartsWith("Terrain_"))
        {
            return WallType.LockedDoor;
        }
        else if (tile.name == "tiles_12")
        {
            return WallType.Rock;
        }

        return WallType.None;
    }

    static public Item LookupItem(TileBase tile)
    {
        if (tile == null)
        {
            return Item.None;
        }
        else if (tile.name == "Key-Tile")
        {
            return Item.Key;
        }
        else if (tile.name == "Add-Extra-Move"
              || tile.name == "180-Turn"
              || tile.name == "Take-Away-Move"
              || tile.name == "Sleep-Tile"
              || tile.name == "CCW-90-Turn"
              || tile.name == "CW-90-Turn")
        {
            return Item.Bug;
        }

        return Item.None;
    }

    static public MoveType LookupBug(TileBase tile)
    {
        if (tile == null)
        {
            return MoveType.None;
        }
        else if (tile.name == "Add-Extra-Move")
        {
            return MoveType.Forward;
        }
        else if (tile.name == "180-Turn")
        {
            return MoveType.Rotate180;
        }
        else if (tile.name == "Take-Away-Move")
        {
            return MoveType.Backward;
        }
        else if (tile.name == "Sleep-Tile")
        {
            return MoveType.Wait;
        }
        else if (tile.name == "CCW-90-Turn")
        {
            return MoveType.Rotate90CCW;
        }
        else if (tile.name == "CW-90-Turn")
        {
            return MoveType.Rotate90CW;
        }

        return MoveType.None;
    }
}