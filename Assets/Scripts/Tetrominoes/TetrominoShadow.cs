using UnityEngine;

/// <summary>
/// Cette class repr�sente l'ombre d'un <c>Tetromino</c>, autrement dit, il indique l'endroit o� se posera le <c>Tetromino</c>.
/// </summary>
public class TetrominoShadow : Tetromino
{
    private Tetromino Replicated_Tetromino = null;

    /// <summary>
    /// D�fini le <c>Tetromino</c> dont l'ombre sera affich�.
    /// </summary>
    /// <param name="tetromino">Le <c>Tetromino</c> � repliquer</param>
    public void SetReplicated_Tetromino(Tetromino tetromino) 
    {
        Replicated_Tetromino = tetromino;

        this.RotationIndex = Replicated_Tetromino.RotationIndex;

        Reset();
        
        GenerateRotations();

        InstantiateBlocks();
    }

    /// <summary>
    /// D�place l'ombre le plus bas possible.
    /// </summary>
    public void MoveAtTheBottom()
    {
        this.SetPosition(Replicated_Tetromino.transform.position);

        this.RotationIndex = Replicated_Tetromino.RotationIndex;

        this.ActualizeBlockPosition();

        while (!playfield.HasLanded(this))
        {
            Move(Vector2.down);
        }
    }

    /// <summary>
    /// R�initialise l'ombre.
    /// </summary>
    public void Reset()
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
