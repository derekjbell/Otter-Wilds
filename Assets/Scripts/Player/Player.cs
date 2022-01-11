using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    Tilemap bugmap;
    MoveInterface movement;
    MoveArbiter arbiter;
    bool key_down = false;

    void Start()
    {
        arbiter = GameObject.Find("/MoveArbiter").GetComponent<MoveArbiter>();
        bugmap = GameObject.Find("/Grid/Bugmap").GetComponent<Tilemap>();
        movement = GetComponent<MoveInterface>();
        movement.MoveConflict = MoveConflict;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            if (!key_down)
            {
                key_down = true;

                if (horizontal > 0.0f)
                {
                    movement.SetMove(MoveType.Right);
                }
                else if (horizontal < 0.0f)
                {
                    movement.SetMove(MoveType.Left);
                }
                else if (vertical > 0.0f)
                {
                    movement.SetMove(MoveType.Forward);

                }
                else if (vertical < 0.0f)
                {
                    movement.SetMove(MoveType.Backward);
                }

                arbiter.Tick();
            }
        }
        else
        {
            key_down = false;

            if (Input.GetButtonDown("PlaceObject"))
            {
                Tile existing_tile = bugmap.GetTile<Tile>(movement.position);
                bugmap.SetTile(movement.position, movement.GetTile());
                movement.SetTile(existing_tile);
                arbiter.Tick();
            }
        }
    }

    void MoveConflict(GameObject other)
    {
        Destroy(gameObject);
    }
}