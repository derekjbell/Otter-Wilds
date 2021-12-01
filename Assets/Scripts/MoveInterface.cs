using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveInterface : MonoBehaviour
{
    MoveArbiter arbiter;
    Grid grid;
    TileQuantizer quantizer;

    Tile item_tile = null;
    GameObject item_object = null;

    [SerializeField]
    MoveType wall_move = MoveType.Wait;

    public Vector3Int position = new Vector3Int();
    public Direction direction;

    MoveType next_move;
    public Vector3Int next_position;
    public Direction next_direction;

    public delegate void MoveConflictHandler(GameObject other);
    public MoveConflictHandler MoveConflict;

    private void Awake()
    {
        arbiter = GameObject.Find("MoveArbiter").GetComponent<MoveArbiter>();
    }

    // Start is called before the first frame update
    void Start()
    {

        arbiter.Register(this);

        item_object = transform.GetChild(0).gameObject;

        grid = GameObject.Find("Grid").GetComponent<Grid>();

        quantizer = GetComponent<TileQuantizer>();

        QuantizeTransform();
    }

    public void DoWallMove()
    {
        SetMove(wall_move);
    }

    public void SetMove(MoveType move)
    {
        next_move = move;
        switch (move)
        {
            case MoveType.Wait:
            case MoveType.None:
                // Do nothing
                next_position = position;
                next_direction = direction;
                break;
            case MoveType.Forward:
                next_position = position + Math2.Unit(direction);
                next_direction = direction;
                break;
            case MoveType.Backward:
                next_position = position - Math2.Unit(direction);
                next_direction = direction;
                break;
            case MoveType.Left:
                next_position = position + Math2.Unit(Math2.Rotate(direction, Rotation.CCW));
                next_direction = direction;
                break;
            case MoveType.Right:
                next_position = position + Math2.Unit(Math2.Rotate(direction, Rotation.CW));
                next_direction = direction;
                break;
            case MoveType.Rotate180:
                next_position = position;
                next_direction = Math2.Rotate(direction, Rotation.R180);
                break;
            case MoveType.Rotate90CW:
                next_position = position;
                next_direction = Math2.Rotate(direction, Rotation.CW);
                break;
            case MoveType.Rotate90CCW:
                next_position = position;
                next_direction = Math2.Rotate(direction, Rotation.CCW);
                break;
        }
    }

    public MoveType GetMove()
    {
        return next_move;
    }

    public (Vector3Int, Direction) GetNextState()
    {
        return (next_position, next_direction);
    }


    void OnDestroy()
    {
        arbiter.UnRegister(this);
    }

    public void ExecuteMove()
    {
        position = next_position;
        direction = next_direction;
        next_move = MoveType.None;
        quantizer.SetTarget(position, direction);
    }

    public TileBase GetTile()
    {
        return item_tile;
    }

    public void SetTile(Tile tile)
    {
        item_tile = tile;
        item_object.GetComponent<SpriteRenderer>().sprite = item_tile ? item_tile.sprite : null;
    }


    void QuantizeTransform()
    {
        position = grid.WorldToCell(transform.position);
        switch ((int)Mathf.Round(transform.rotation.eulerAngles.z / 90.0f))
        {
            case 0:
                direction = Direction.Up;
                break;
            case 1:
                direction = Direction.Left;
                break;
            case 2:
            case -2:
                direction = Direction.Down;
                break;
            case -1:
            case 3:
                direction = Direction.Right;
                break;
        }

        quantizer.SetTarget(position, direction);
    }
}
