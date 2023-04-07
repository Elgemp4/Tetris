using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Tetromino : Tetromino
{
    protected override void GenerateRotations()
    {
        rotations = new Vector2[,]
        {
            { Vector2.up + Vector2.left, Vector2.up, Vector2.zero, Vector2.right},
            { Vector2.up + Vector2.right, Vector2.right, Vector2.zero, Vector2.down },
            { Vector2.down + Vector2.right, Vector2.down, Vector2.zero, Vector2.left },
            { Vector2.down + Vector2.left, Vector2.left, Vector2.zero, Vector2.up }
        };
    }

    protected override Color GetColor()
    {
        return new Color(1, 0, 0, 1);
    }
}
