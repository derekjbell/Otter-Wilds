using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileQuantizer : MonoBehaviour
{
    Grid grid;

    [SerializeField]
    float animation_time = 0.25f;

    double move_start_time = 0;
    Vector3 target_position;
    Vector3 last_position;

    float target_rotation;
    float last_rotation;

    private void Awake()
    {
        grid = GameObject.Find("/Grid").GetComponent<Grid>();
    }

    // Start is called before the first frame update
    void Start()
    {
        move_start_time = Time.timeAsDouble;
        last_position = transform.position;
        target_position = transform.position;
        last_rotation = transform.rotation.eulerAngles.z;
        target_rotation = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(last_position, target_position, (float)(Time.timeAsDouble - move_start_time) / animation_time);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(last_rotation, last_rotation + Move.SignedRemainder(target_rotation - last_rotation, 360.0f), (float)(Time.timeAsDouble - move_start_time) / animation_time));
    }

    static float DirectionToDegrees(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return 0.0f;
            case Direction.Down:
                return 180.0f;
            case Direction.Left:
                return 90.0f;
            case Direction.Right:
                return -90.0f;
        }

        return 0.0f; // Silence warning
    }

    public void SetTarget(Vector3Int pos, Direction dir)
    {
        move_start_time = Time.timeAsDouble;
        last_position = transform.position;
        last_rotation = transform.eulerAngles.z;
        target_position = grid.GetCellCenterWorld(pos);
        target_rotation = DirectionToDegrees(dir);
    }
}
