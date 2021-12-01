using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelBarrierRemoval : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject level_1_accent;
    public GameObject level_2_accent;
    public GameObject level_3_accent;
    Vector3Int level_2_barrier = new Vector3Int(-5, 0, 0);
    Vector3Int level_3_barrier = new Vector3Int(5, 0, 0);

    private void Start()
    {

        if (PlayerPrefs.GetInt("level1") == 1)
        {
            Debug.Log("Deleting barriers for levels 2 and 3..");
            tilemap.SetTile(level_2_barrier, null);
            tilemap.SetTile(level_3_barrier, null);
            level_1_accent.SetActive(true);
        }

        if (PlayerPrefs.GetInt("level2") == 1)
        {
            level_2_accent.SetActive(true);
        }

        if (PlayerPrefs.GetInt("level3") == 1)
        {
            level_3_accent.SetActive(true);
        }
    }
}
