using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Tetromino : Tetromino
{
    protected override void GenerateRotations()
    {
        rotations = new Vector2[,]
        {
            { Vector2.left, Vector2.zero, Vector2.right, Vector2.right*2 },
            { Vector2.up, Vector2.zero, Vector2.down, Vector2.down * 2},
            { Vector2.left, Vector2.zero, Vector2.right, Vector2.right*2 },
            { Vector2.down, Vector2.zero, Vector2.up, Vector2.up * 2}
        };
    }

    protected override Color GetColor()
    {
        return new Color(0, 1, 1, 1);
    }
}
