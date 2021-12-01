using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public enum MoveType
{
    None,
    Wait,
    Rotate180,
    Left,
    Right,
    Forward,
    Backward,
    Rotate90CCW,
    Rotate90CW
}

public class MoveArbiter : MonoBehaviour
{
    Tilemap tile_map;

    List<MoveInterface> movers = new List<MoveInterface>();

    public delegate void TickHandler();
    public event TickHandler OnTick;
    public event TickHandler BeforeTick;


    void Start()
    {
        tile_map = GameObject.Find("/Grid/Tilemap").GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetButtonDown("Tick"))
        {
            Tick();
        }
    }

    public void Tick()
    {
        if (BeforeTick != null)
        {
            BeforeTick();
        }

        if (OnTick != null)
        {
            OnTick();
        }

        foreach (MoveInterface mover in movers)
        {
            (Vector3Int new_pos, Direction new_dir) = mover.GetNextState();

            Tiles.WallType dest_tile = Tiles.LookupWallType(tile_map.GetTile(new_pos));

            if (dest_tile == Tiles.WallType.None)
            {
                // Do nothing
            }
            else if (dest_tile == Tiles.WallType.Wall)
            {
                mover.DoWallMove();
            }
            else if (dest_tile == Tiles.WallType.LockedDoor)
            {
                if (Tiles.LookupItem(mover.GetTile()) == Tiles.Item.Key)
                {
                    tile_map.SetTile(new_pos, null);
                    mover.SetTile(null);
                }
                else
                {
                    mover.DoWallMove();
                }
            }
        }

        bool was_conflict = true;
        while (was_conflict)
        {
            was_conflict = false;
            for (int i = 0; i < movers.Count - 1; ++i)
            {
                MoveInterface mover1 = movers[i];
                for (int j = i + 1; j < movers.Count; ++j)
                {
                    MoveInterface mover2 = movers[j];

                    if ((mover1.position == mover2.next_position
                        && mover2.position == mover1.next_position)
                        || mover1.next_position == mover2.next_position)
                    {
                        // There is a move conflict. Undo moves and handle conflicts.
                        // Note that some characters can overlap, in which case MoveConflict will do nothing.
                        if (mover1.MoveConflict != null && mover1.next_position != mover1.position)
                        {
                            try
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                                //was_conflict = true;
                                //mover1.MoveConflict(mover2.gameObject);
                            }
                            catch
                            {
                                Debug.Log("move conflict for mover1");
                            }
                        }

                        if (mover2.MoveConflict != null && mover2.next_position != mover2.position)
                        {
                            try
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                                //was_conflict = true;
                                //mover2.MoveConflict(mover1.gameObject);
                            }
                            catch
                            {
                                Debug.Log("move conflict for mover2");
                            }
                        }
                    }
                }
            }
        }

        foreach (MoveInterface mover in movers)
        {
            mover.ExecuteMove();
        }
    }

    public void Register(MoveInterface mover)
    {
        movers.Add(mover);
    }

    public void UnRegister(MoveInterface mover)
    {
        movers.Remove(mover);
    }
}
