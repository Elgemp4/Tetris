using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La class <c>PieceSequence</c> gère la génération et le stockage des prochains <c>Tetromino</c>
/// </summary>
public class PieceSequence : MonoBehaviour
{
    public static PieceSequence Instance;

    private Queue<GameObject> NextTetrominoes;

    private List<GameObject> VisisbleTetrominoes;

    private const float TETROMINOES_GAP = 3f;

    private static string[] TetrominoesIndex =
    {
        "I_Tetromino",
        "S_Tetromino",
        "Z_Tetromino",
        "L_Tetromino",
        "J_Tetromino",
        "T_Tetromino",
        "O_Tetromino"
    };

    void Start()
    {
        Instance = this;

        NextTetrominoes = new Queue<GameObject>();

        VisisbleTetrominoes = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            AddTetrominoBag();
        }

        InstantiateFirstTetrominoes();
    }

    /// <summary>
    /// Ajoute 7 <c>Tetromino</c> dans un ordre aléatoire à la séquence de <c>Tetromino</c>
    /// </summary>
    private void AddTetrominoBag()
    {
        int[] bag = new int[7];
        
        for(int i = 0; i < bag.Length; i++) 
        {
            int index = Random.Range(0, bag.Length);

            if (bag[index] == 0)
            {
                bag[index] = i;
            }
            else 
            {
                i--;
            }
        }

        foreach(int index in bag)
        {
            CreateTetromino(index);
        }
    }

    /// <summary>
    /// Crée un <c>Tetromino</c> à partir de son index
    /// </summary>
    /// <param name="index">Index du <c>Tetromino</c> à créer</param>
    private void CreateTetromino(int index)
    {
        string choosenTetromino = TetrominoesIndex[index];

        NextTetrominoes.Enqueue(Resources.Load("Tetrominoes/" + choosenTetromino) as GameObject);
    }

    /// <summary>
    /// Instancie les 5 premiers <c>Tetromino</c> de la séquence à des fins d'affichage
    /// </summary>
    private void InstantiateFirstTetrominoes()
    {
        for (int i = 0; i < 5; i++) 
        {
            InstintateNextVisibleTetromino(i);
        }
    }

    /// <summary>
    /// Instancie le prochain <c>Tetromino</c> de la séquence à des fins d'affichage
    /// </summary>
    /// <param name="position">Position dans l'affichage</param>
    private void InstintateNextVisibleTetromino(int position)
    {
        GameObject tetrominoe = Instantiate(NextTetrominoes.Dequeue());

        tetrominoe.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - TETROMINOES_GAP * position);

        VisisbleTetrominoes.Add(tetrominoe);
    }

    /// <summary>
    /// Décale les <c>Tetromino</c> affichés vers le haut
    /// </summary>
    private void ShiftUpVisibleTetrominoes()
    { 
        foreach(GameObject tetrominoes in VisisbleTetrominoes)
        {
            tetrominoes.transform.position += Vector3.up * TETROMINOES_GAP;
        }
    }

    /// <summary>
    /// Retourne le prochain <c>Tetromino</c> de la séquence et l'enlève de celle-ci
    /// </summary>
    /// <returns></returns>
    public GameObject GetNextTetromino()
    {
        GameObject nextTetromino = VisisbleTetrominoes[0];

        VisisbleTetrominoes.RemoveAt(0);
        
        ShiftUpVisibleTetrominoes();

        InstintateNextVisibleTetromino(4);

        if(NextTetrominoes.Count + VisisbleTetrominoes.Count <= 7 ) 
        {
            AddTetrominoBag();
        }

        return nextTetromino;
    }
}
