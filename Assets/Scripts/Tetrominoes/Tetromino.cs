using UnityEngine;

/// <summary>
/// Cette classe abstraite représente un <c>Tetromino</c>.
/// </summary>
public abstract class Tetromino : MonoBehaviour
{
    public const int LEFT = -1, RIGHT = 1;

    private const int NumberOfBlock = 4;

    protected Playfield playfield;

    private GameObject blockPrefab;

    public readonly Vector2 StartPosition = new Vector2(5, 20);

    public GameObject[] blocks { private set; get; }

    public Vector2[,] rotations { protected set; get; }

    public int RotationIndex { protected set; get; } = 0;

    private void Awake()
    {
        this.playfield = Playfield.Instance;

        blockPrefab = Resources.Load(@"Tetrominoes/Block") as GameObject;

        GenerateRotations();

        InstantiateBlocks();
    }

    /// <summary>
    /// Défini la position du <c>Tetromino</c>
    /// </summary>
    /// <param name="newPosition"></param>
    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;

        ActualizeBlockPosition();
    }

    /// <summary>
    /// Défini la position du <c>Tetromino</c> à sa position de départ
    /// </summary>
    public void SetAtStart()
    {
        SetPosition(StartPosition);
    }

    /// <summary>
    /// Déplace le <c>Tetromino</c> dans la direction donnée
    /// </summary>
    /// <param name="direction">Un vecteur représentant la direction du mouvement</param>
    public void Move(Vector2 direction) 
    {
        SetPosition((Vector2)this.transform.position + direction);
    }

    /// <summary>
    /// Tourne le <c>Tetromino</c> dans la direction donnée
    /// </summary>
    /// <param name="direction">Un nombre représentant la rotation</param>
    public void Rotate(int direction)
    {
        int modulus = rotations.GetLength(0);

        RotationIndex = (RotationIndex + direction + modulus) % modulus;

        ActualizeBlockPosition();
    }

    /// <summary>
    /// Réinitialise la rotation du <c>Tetromino</c> à celle d'origine
    /// </summary>
    public void ResetRotation()
    {
        RotationIndex = 0;

        ActualizeBlockPosition();
    }

    /// <summary>
    /// Instancie l'ensemble des blocs du <c>Tetromino</c>
    /// </summary>
    protected void InstantiateBlocks()
    {
        blocks = new GameObject[NumberOfBlock];
        
        blockPrefab.GetComponent<SpriteRenderer>().color = GetColor();
        
        for (int i = 0; i < NumberOfBlock; i++)
        {
            blocks[i] = Instantiate(blockPrefab, this.transform, false); //Creation des blocs avec une position relative au tetromino
        }

        ActualizeBlockPosition();
    }

    /// <summary>
    /// Actualise la position des blocs du <c>Tetromino</c> selon la position du celui-ci
    /// </summary>
    protected void ActualizeBlockPosition()
    {
        if (blocks == null || blocks.Length == 0)
        {
            return;
        }


        for (int i = 0; i < rotations.GetLength(1); i++)
        {
            blocks[i].transform.position = this.transform.position;
            blocks[i].transform.position += (Vector3)rotations[RotationIndex, i];
        }
    }

    /// <summary>
    /// Retourne la couleur du <c>Tetromino</c>
    /// </summary>
    /// <returns>La couleur du <c>Tetromino</c></returns>
    protected abstract Color GetColor();

    /// <summary>
    /// Génère les rotations du <c>Tetromino</c>
    /// </summary>
    protected abstract void GenerateRotations();
}
