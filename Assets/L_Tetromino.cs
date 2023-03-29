using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Tetromino : Tetromino
{
    protected override void GenerateRotations()
    {
        rotations = {
            { }
        };
    }

    protected override Color GetColor()
    {
        return new Color(255, 151, 0, 255);
    }
}
