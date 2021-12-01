using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BugmapActive : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile key_tile;

    Vector3Int key_position = new Vector3Int(-1, -2, 0);

    // Start is called before the first frame update
    void Start()
    {
        // check if player has beaten all 3 beginner levels. if so, give them the key to unlock the next area
        if (PlayerPrefs.GetInt("level1") == 1 && PlayerPrefs.GetInt("level2") == 1 && PlayerPrefs.GetInt("level3") == 1)
        {
            tilemap.SetTile(key_position, key_tile);
        }
    }
}
