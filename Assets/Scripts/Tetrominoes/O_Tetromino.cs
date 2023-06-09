using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Tetromino : Tetromino
{
    protected override void GenerateRotations()
    {
        rotations = new Vector2[,]
        {
            { Vector2.zero, Vector2.left, Vector2.up, Vector2.left + Vector2.up},
        };
    }

    protected override Color GetColor()
    {
        return new Color(1, 1, 0, 1);
    }
}
