using UnityEngine;

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

    public void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;

        ActualizeBlockPosition();
    }

    public void SetAtStart()
    {
        ChangePosition(StartPosition);
    }

    public void Move(Vector2 direction) 
    {
        ChangePosition((Vector2)this.transform.position + direction);
    }

    public void Rotate(int direction)
    {
        int modulus = rotations.GetLength(0);

        RotationIndex = (RotationIndex + direction + modulus) % modulus;

        ActualizeBlockPosition();
    }

    public void ResetRotation()
    {
        RotationIndex = 0;

        ActualizeBlockPosition();
    }

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

    protected abstract Color GetColor();

    protected abstract void GenerateRotations();
}
