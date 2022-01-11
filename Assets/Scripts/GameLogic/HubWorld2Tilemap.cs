using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HubWorld2Tilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap bugmap;
    public GameObject level_4_accent;
    public GameObject level_5_accent;
    public Tile key_tile;
    Vector3Int key_position = new Vector3Int(-1, -2, 0);

    private void Start()
    {
        if (PlayerPrefs.GetInt("level4") == 1)
        {
            level_4_accent.SetActive(true);
        }

        if (PlayerPrefs.GetInt("level5") == 1)
        {
            level_5_accent.SetActive(true);
        }

        if (PlayerPrefs.GetInt("level4") == 1 && PlayerPrefs.GetInt("level5") == 1)
        {
            bugmap.SetTile(key_position, key_tile);
        }
    }
}
