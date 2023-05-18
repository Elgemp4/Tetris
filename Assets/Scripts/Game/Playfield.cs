using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// La classe <c>PlayField</c> a pour r�le de g�rer la zone de jeu. Elle va g�rer les collisions, la d�tection des lignes complet�es et leur suppresion, etc...
/// </summary>
public class Playfield : MonoBehaviour
{
    public static Playfield Instance;

    private ControlsManager _ControlsManager;

    private CinemachineImpulseSource _CameraShakeSource;

    private Audio _AudioPlayer;

    private Score _Score;

    private PieceSequence _PieceSequence;

    private GameObject[,] _BlockGrid;

    private TetrominoHold _TetrominoHold;

    private GameMenuManager _GameMenuManager;

    private LeaderBoard _LeaderBoard;

    [SerializeField]
    private int Width, Height;

    public Tetromino CurrentTetromino { private set; get; }

    private TetrominoShadow GhostTetromino;

    void Start()
    {
        Instance = this;

        _GameMenuManager = GameMenuManager.Instance;

        _ControlsManager = ControlsManager.Instance;

        _LeaderBoard = LeaderBoard.Instance;

        GhostTetromino = Instantiate((GameObject)Resources.Load("Tetrominoes/Ghost_Tetromino")).GetComponent<TetrominoShadow>();

        _CameraShakeSource = GetComponent<CinemachineImpulseSource>();

        _AudioPlayer = Audio.Instance;

        _PieceSequence = PieceSequence.Instance;

        _TetrominoHold = TetrominoHold.Instance;

        _BlockGrid = new GameObject[Height, Width];

        _Score = Score.Instance;

        GenerateNextTetromino();

        GhostTetromino.MoveAtTheBottom();
    }

    #region Movement
    /// <summary>
    /// Essaie de d�placer le <c>Tetromino</c> courant dans la direction pass�e en param�tre
    /// </summary>
    /// <param name="direction">La direction vers laquelle on souhaite d�placer le <c>Tetromino</c> courant</param>
    /// <returns>Un bool�an de valeur True si le d�placement s'est correctement d�roul�, False si le d�placement est impossible</returns>
    public bool TryMove(Vector2 direction)
    {
        CurrentTetromino.Move(direction);
        if (!IsTetrominoInBound() || IsOverlapping())
        {
            CurrentTetromino.Move(-direction);
            return false;
        }
        

        _AudioPlayer.PlaySoundEffect(ESoundEffects.Move);
        GhostTetromino.MoveAtTheBottom();

        return true;
        
    }

    /// <summary>
    /// Essaie de descendre le <c>Tetromino</c> courant vers la bas, si il ne peut pas descendre plus bas, il est plac� et un nouveau tetromino est g�n�r�
    /// </summary>
    /// <returns>Une valeur bool�an True si il peut encore descendre, False si il ne peut plus descendre</returns>
    public bool TryMoveDown()
    {
        if (!TryMove(Vector2.down))
        {
            PlaceTetromino();

            CheckForDestroyedLines();

            GenerateNextTetromino();

            _TetrominoHold.ResetHold();

            return false;
        }

        return true;
    }

    /// <summary>
    /// Fait descendre le <c>Tetromino</c> courant jusqu'� ce qu'il ne puisse plus descendre
    /// </summary>
    public void HardDrop()
    {
        while (TryMoveDown()){}

        _CameraShakeSource.GenerateImpulseWithForce(0.15f);

        _AudioPlayer.PlaySoundEffect(ESoundEffects.HardDrop);
    }


    /// <summary>
    /// Essaie de faire tourner le tetromino dans la direction indiqu�e
    /// </summary>
    /// <param name="direction">La direction souhait�e, 1 = vers le droite, -1 = vers la gauche</param>
    public void TryRotate(int direction)
    {
        if (Math.Abs(direction) != 1) { return; }

        CurrentTetromino.Rotate(direction);

        if (!IsTetrominoInBound() || IsOverlapping())
        {
            CurrentTetromino.Rotate(-direction);
        }
        else
        {
            _AudioPlayer.PlaySoundEffect(ESoundEffects.Rotate);
            GhostTetromino.MoveAtTheBottom();
        }
    }

    /// <summary>
    /// Renvoie si le <c>Tetromino</c> pass� en param�tre a atteri ou non
    /// </summary>
    /// <param name="tetromino">Le <c>Tetromino</c> qu'on souhaite savoir si il a atteri</param>
    /// <returns>Une valeur bool�an True si le <c>Tetromino</c> a atteri et False autrement</returns>
    public bool HasLanded(Tetromino tetromino) 
    {
        foreach (GameObject block in tetromino.blocks) 
        {
            Vector2 blockPosition = (Vector2)block.transform.position;

            if (blockPosition.y == 0 || _BlockGrid[(int)blockPosition.y - 1, (int)blockPosition.x] != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Ajoute le <c>Tetromino</c> courant � la grille du jeu.
    /// </summary>
    public void PlaceTetromino()
    {
        foreach (GameObject block in CurrentTetromino.blocks)
        {
            this._BlockGrid[(int)block.transform.position.y, (int)block.transform.position.x] = Instantiate(block, block.transform.position, block.transform.rotation, this.transform);
        }

        Destroy(CurrentTetromino.gameObject);

        _CameraShakeSource.GenerateImpulseWithForce(0.05f);
    }

    /// <summary>
    /// D�fini le <c>Tetromino</c> actuellement occup� de tomber
    /// </summary>
    /// <param name="newTetromino">Le <c>Tetromino que l'on souhaite voir tomber</c></param>
    public void SetFallingTetromino(Tetromino newTetromino)
    {
        if (newTetromino == null)
        {
            GenerateNextTetromino();
            return;
        }

        CurrentTetromino = newTetromino;

        CurrentTetromino.SetAtStart();

        GhostTetromino.SetReplicated_Tetromino(CurrentTetromino);

        GhostTetromino.MoveAtTheBottom();
    }

    /// <summary>
    /// S'occupe de r�cup�rer le prochain <c>Tetromino</c> � tomber et le d�fini en tant que tel. Si il y d�j� un tetromino � l'emplacement d'apparition, le jeu est termin�
    /// </summary>
    private void GenerateNextTetromino()
    {
        SetFallingTetromino(_PieceSequence.GetNextTetromino().GetComponent<Tetromino>());

        if (IsOverlapping())
        {
            GameOver();
        }
    }

    /// <summary>
    /// Arr�te le jeu et affiche le menu de fin de partie tout en transmettant le score au <c>LeaderBoard</c>
    /// </summary>
    private void GameOver()
    {
        _ControlsManager.PauseGame();

        _GameMenuManager.OpenGameOverMenu();

        _LeaderBoard.AddNewScore(_Score.ScoreCount);
    }

    /// <summary>
    /// V�rifie si il y a d�j� des blocs l� o� se situe le <c>Tetromino</c> actuel
    /// </summary>
    /// <returns>True si le Tetromino se superpose � des blocs, False autrement</returns>
    private bool IsOverlapping()
    {
        foreach (GameObject block in CurrentTetromino.blocks)
        {
            Vector3 position = block.transform.position;

            if (_BlockGrid[(int)position.y, (int)position.x] != null)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// V�rifie si le <c>Tetromino</c> actuel est dans les limites de la grille
    /// </summary>
    /// <returns>True si le <c>Tetromino</c> est dans le limites de la grille, False autrement</returns>
    private bool IsTetrominoInBound()
    {
        foreach (GameObject block in CurrentTetromino.blocks)
        {
            Vector3 position = block.transform.position;

            if (!IsInBound((int)position.x, (int)position.y))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// V�rifie si les coordon�es pass�es en param�tre sont dans les limites de la grille
    /// </summary>
    /// <param name="x">Coordon�es X</param>
    /// <param name="y">Coordon�es Y</param>
    /// <returns>True si les coordon�es sont dans les limites de la grille, False autrement</returns>
    private bool IsInBound(int x, int y)
    {
        return x >= 0 && x < 10 && y >= 0;
    }

    #endregion

    #region LineClearing

    /// <summary>
    /// V�rifie si il y a des lignes compl�tes et les d�truit, et puis fais descendre les blocs au dessus
    /// </summary>
    public void CheckForDestroyedLines()
    {
        int destroyedLines = 0;


        List<int> clearedLines = new List<int>();

        for (int y = 0; y < Height; y++)
        {
            if (IsLineFull(y))
            {
                DestroyLine(y);
                destroyedLines++;
                clearedLines.Add(y);
            }
        }

        if (destroyedLines > 0)
        {
            _AudioPlayer.PlayLineClear(destroyedLines);

            _Score.AddScore(destroyedLines);

            _CameraShakeSource.GenerateImpulseWithForce(destroyedLines * 0.5f);

            foreach(int line in clearedLines)
            {
                DropBlocks(line);
            }

            CheckForDestroyedLines();
        }
    }

    /// <summary>
    /// V�rifie si la ligne indiqu�e est compl�te
    /// </summary>
    /// <param name="y">Coordon�e Y de la ligne</param>
    /// <returns>True si la ligne est pleine, False autrement</returns>
    private bool IsLineFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (_BlockGrid[y, x] == null)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// D�truit la ligne indiqu�e
    /// </summary>
    /// <param name="y">Coordon�e Y de la ligne</param>
    private void DestroyLine(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            Destroy(_BlockGrid[y, x]);
            _BlockGrid[y, x] = null;
        }
    }

    /// <summary>
    /// Fais descendre les blocs au dessus de la ligne indiqu�e
    /// </summary>
    /// <param name="clearedLine">La ligne au dessus de laquelle on souhaite faire descendre les blocs</param>
    public void DropBlocks(int clearedLine)
    {
        List<Aglomerate> blockAglomerate = new List<Aglomerate>();

        for (int x = 0; x < Width; x++)
        {
            if (_BlockGrid[clearedLine + 1, x] != null)
            {
                List<GameObject> blocks = CreateAglomerate(clearedLine + 1, x);

                blockAglomerate.Add(new Aglomerate(blocks));
            }
        }

        foreach (Aglomerate aglomerate in blockAglomerate)
        {
            aglomerate.Drop();

            foreach (GameObject block in aglomerate.blockList)
            {
                _BlockGrid[(int)block.transform.position.y, (int)block.transform.position.x] = block;
            }
        }
    }

    /// <summary>
    /// Cr�er r�cursivement une liste de blocs li� entre eux
    /// </summary>
    /// <param name="y">Position Y du bloc actuellement trait�</param>
    /// <param name="x">Position X du bloc actuellement trait�</param>
    /// <returns>Liste de blocs li� entre eux</returns>
    public List<GameObject> CreateAglomerate(int y, int x)
    {
        List<GameObject> aglomerateBlocks = new List<GameObject>();

        GameObject block = null;

        if (IsInBound(x, y))
        {
            block = _BlockGrid[y, x];
        }

        if (block != null)
        {
            aglomerateBlocks.Add(block);
            _BlockGrid[y, x] = null;

            aglomerateBlocks.AddRange(CreateAglomerate(y + 1, x));
            aglomerateBlocks.AddRange(CreateAglomerate(y - 1, x));
            aglomerateBlocks.AddRange(CreateAglomerate(y, x + 1));
            aglomerateBlocks.AddRange(CreateAglomerate(y, x - 1));
        }

        return aglomerateBlocks;
    }

    #endregion

    #region Misc

    /// <summary>
    /// V�rifie si il y a un bloc � la position indiqu�e
    /// </summary>
    /// <param name="x">Coordon�e X du bloc</param>
    /// <param name="y">Coordon�e Y du bloc</param>
    /// <returns>True si il y a un bloc, False autrement</returns>
    public bool IsABlockPresent(int x, int y)
    {
        return _BlockGrid[y, x] != null;
    }

    /// <summary>
    /// G�re la retenue de pi�ce, lorsque cette m�thode est appel�e elle essaiera d'�changer le <c>Tetromino</c> actuel avec celui retenu.
    /// </summary>
    public void Hold()
    {
        try 
        {
            Tetromino newTetromino = _TetrominoHold.Switch(CurrentTetromino);

            SetFallingTetromino(newTetromino);

            _AudioPlayer.PlaySoundEffect(ESoundEffects.Hold);
        }
        catch (Exception) { } 
    }

    #endregion
}