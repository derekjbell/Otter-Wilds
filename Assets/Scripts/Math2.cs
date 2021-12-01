using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
}

public enum Rotation
{
    None = 0,
    CCW = 1,
    R180 = 2,
    CW = 3
}

public class Math2
{
    public static Direction Rotate(Direction dir, Rotation r)
    {
        return (Direction)(((int)dir + (int)r + 4) % 4);
    }

    public static Vector3Int Unit(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return new Vector3Int(0, 1, 0);
            case Direction.Down:
                return new Vector3Int(0, -1, 0);
            case Direction.Left:
                return new Vector3Int(-1, 0, 0);
            case Direction.Right:
                return new Vector3Int(1, 0, 0);
        }

        return new Vector3Int(); // Suppress warning
    }

    public static Rotation RotationTowards(Direction dir, Direction target)
    {
        int angle_grad = (SignedRemainderInt((int)target - (int)dir, 4) + 4) % 4;
        if (angle_grad == 2)
        {
            angle_grad = 1;
        }
        return (Rotation)angle_grad;
    }

    public static float SignedRemainder(float a, float b)
    {
        return a - Mathf.Round(a / b) * b;
    }

    public static int SignedRemainderInt(int a, int b)
    {
        return a - Mathf.RoundToInt((float)a / (float)b) * b;
    }
}