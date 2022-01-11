using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SchoolingFish : MonoBehaviour
{
    int move_index = 0;

    [SerializeField]
    List<MoveType> moves = new List<MoveType>();

    Tilemap items;

    MoveInterface movement;

    SchoolingFish leader = null;


    MoveArbiter arbiter;

    bool moved = false;


    static SchoolingFish[,] fish = new SchoolingFish[128, 128];
    static bool cleared_fish = true;

    private void Awake()
    {
        arbiter = GameObject.Find("/MoveArbiter").GetComponent<MoveArbiter>();
        items = GameObject.Find("/Grid/Bugmap").GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        arbiter.OnTick += Tick;
        arbiter.BeforeTick += PreTick;

        movement = GetComponent<MoveInterface>();
        movement.MoveConflict = MoveConflict;

        
    }

    public (List<MoveType>, int) GetMoves()
    {
        return (new List<MoveType>(moves), move_index);
    }

    MoveType GetMove()
    {
        if (!moved)
        {
            Tick();
        }
        return movement.GetMove();
    }

    (SchoolingFish, Direction) GetLeader()
    {
        // See if this fish is following any other
        Vector3Int pos = movement.position;

        Direction forward_direction = movement.direction;
        Direction left_direction = Move.Rotate(movement.direction, Rotation.CCW);
        Direction right_direction = Move.Rotate(movement.direction, Rotation.CW);

        Vector3Int front = Move.Unit(forward_direction);
        Vector3Int left = Move.Unit(left_direction);
        Vector3Int right = Move.Unit(right_direction);

        (Vector3Int, Direction)[] leader_positions = new (Vector3Int, Direction)[] {
                (pos + front, forward_direction),
                (pos + front + left, left_direction),
                (pos + front + right, right_direction)
            };

        SchoolingFish leader = null;

        foreach ((Vector3Int leader_pos, Direction d) in leader_positions)
        {
            leader = fish[leader_pos.y + 64, leader_pos.x + 64];

            if (leader != null)
            {
                return (leader, d);
            }
        }

        return (leader, Direction.Up);
    }

    void Tick()
    {
        cleared_fish = false;

        if (moved)
        {
            return;
        }

        moved = true;

        Direction leader_direction;
        (leader, leader_direction) = GetLeader();

        MoveType bug = Tiles.LookupBug(items.GetTile(movement.position));

        if (bug != MoveType.None)
        {
            // If on a move tile, do that
            movement.SetMove(bug);
            items.SetTile(movement.position, null);
        }
        else if (leader != null)
        {
            // Otherwise, follow the leader
            if (leader.movement.direction == movement.direction)
            {
                // Leader.GetMove() will force the leader to tick
                movement.SetMove(leader.GetMove());

                // Copy leader's list of moves
                (moves, move_index) = leader.GetMoves();
            }
            else if (leader.movement.direction == Move.Rotate(movement.direction, Rotation.R180))
            {
                // Rotate away from leader
                movement.SetMove(RotationToMove(Move.RotationTowards(movement.direction, Move.Rotate(leader_direction, Rotation.R180))));
            }
            else
            {
                // Rotate towards leader
                movement.SetMove(RotationToMove(Move.RotationTowards(movement.direction, leader.movement.direction)));


                // Copy leader's list of moves
                (moves, move_index) = leader.GetMoves();
            }
        }
        else if (moves.Count > 0)
        {
            // If no leader, follow steps.
            movement.SetMove(moves[move_index]);
            move_index = (move_index + 1) % moves.Count;
        }
    }

    MoveType RotationToMove(Rotation r)
    {
        switch (r)
        {
            case Rotation.CCW:
                return MoveType.Rotate90CCW;
            case Rotation.CW:
                return MoveType.Rotate90CW;
            case Rotation.R180:
                return MoveType.Rotate180;
            case Rotation.None:
                return MoveType.Wait;
        }

        Debug.Assert(false);

        return MoveType.None;
    }

    void PreTick()
    {
        moved = false;

        if (!cleared_fish)
        {
            for (int i = 0; i < fish.GetLength(0); ++i)
            {
                for (int j = 0; j < fish.GetLength(1); ++j)
                {
                    fish[i, j] = null;
                }
            }

            cleared_fish = true;
        }

        Vector3Int pos = movement.position;
        fish[pos.y + 64, pos.x + 64] = this;
    }

    void MoveConflict(GameObject other)
    {
        SchoolingFish other_fish = other.GetComponent<SchoolingFish>();
        if (other_fish != leader)
        {
            movement.SetMove(MoveType.Rotate90CCW);
        }
        else if (other_fish != null)
        {
            movement.SetMove(MoveType.Wait);
        }
    }
}
