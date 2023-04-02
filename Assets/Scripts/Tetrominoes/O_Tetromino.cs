using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Tetromino : Tetromino
{
    protected override void GenerateRotations()
    {
        rotations = new Vector2[,]
        {
            { (Vector2.left + Vector2.up) / 2, (Vector2.right + Vector2.up) / 2, (Vector2.left + Vector2.down) / 2, (Vector2.right + Vector2.down) / 2},
        };
    }

    protected override Color GetColor()
    {
        return new Color(255, 255, 0, 255);
    }
}
