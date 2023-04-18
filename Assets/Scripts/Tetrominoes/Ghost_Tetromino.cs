using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Tetromino : Tetromino
{
    private Tetromino Replicated_Tetromino = null;

    public void SetReplicated_Tetromino(Tetromino tetromino) 
    {
        Replicated_Tetromino = tetromino;

        this.RotationIndex = Replicated_Tetromino.RotationIndex;

        RemoveBlocks();
        
        GenerateRotations();

        InstantiateBlocks();
    }

    public void ShowAtTheBottom()
    {
        this.ChangePosition(Replicated_Tetromino.transform.position);

        this.RotationIndex = Replicated_Tetromino.RotationIndex;

        this.ActualizeBlockPosition();

        while (!playfield.HasLanded(this))
        {
            MoveDown();
        }

        
    }

    public void RemoveBlocks()
    {
        if (blocks == null)
        {
            return;
        }

        foreach(GameObject block in blocks)
        {
            Destroy(block);
        }
    }

    protected override void GenerateRotations()
    {
        if (Replicated_Tetromino == null)
        {
            rotations = new Vector2[0,0];
            return;
        }

        rotations = Replicated_Tetromino.rotations;
    }

    protected override Color GetColor()
    {
        return new Color(1, 1, 1, 0.3f);
    }
}
