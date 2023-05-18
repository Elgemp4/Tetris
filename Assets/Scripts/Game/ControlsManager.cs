using System.Collections;
using UnityEngine;

/// <summary>
/// La class <c>ControlsManager</c> g�re les diff�rentes entr�es du joueur
/// </summary>
public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private Score Score;

    private Playfield Playfield;

    private GameMenuManager MenuManager;

    private InGameControls InGameControls;

    private void Awake()
    {
        Instance = this;

        InGameControls = new InGameControls();

        InGameControls.Enable();

        InGameControls.Movement.RotateLeft.performed += _ => Playfield.TryRotate(Tetromino.LEFT);

        InGameControls.Movement.RotateRight.performed += _ => Playfield.TryRotate(Tetromino.RIGHT);

        InGameControls.Movement.MoveRight.performed += _ => StartUniqueCoroutine("MovementTick");

        InGameControls.Movement.MoveLeft.performed += _ => StartUniqueCoroutine("MovementTick");

        InGameControls.Movement.SoftDrop.performed += _ => StartUniqueCoroutine("FallTick");

        InGameControls.Movement.HardDrop.performed += _ => Playfield.HardDrop();

        InGameControls.Movement.Hold.performed += _ => Playfield.Hold();

        InGameControls.Movement.Pause.performed += _ =>
        {
            PauseGame();

            MenuManager.OpenPauseMenu();
        };
    }

    void Start()
    {
        Score = Score.Instance;

        Playfield = Playfield.Instance;

        MenuManager = GameMenuManager.Instance;

        StartUniqueCoroutine("GameTick");
    }

    /// <summary>
    /// Met en pause le jeu
    /// </summary>
    public void PauseGame()
    {
        InGameControls.Disable();

        StopCoroutine("GameTick");
    }

    /// <summary>
    /// Reprend le jeu
    /// </summary>
    public void ResumeGame()
    {
        InGameControls.Enable();

        StartUniqueCoroutine("GameTick");
    }

    /// <summary>
    /// D�marre la coroutine donn�e en param�tre et termine la coroutine pr�c�dente si elle existe de mani�re � n'avoir qu'une coroutine s'�xecutant � la fois
    /// </summary>
    /// <param name="coroutine">KLa coroutine que l'on souhaite d�marrer</param>
    private void StartUniqueCoroutine(string coroutine)
    {
        StopCoroutine(coroutine);

        StartCoroutine(coroutine);
    }

    /// <summary>
    /// Coroutine li�e au d�placement lat�ral du tetromino � un interval r�gulier
    /// </summary>
    /// <returns>Le temps devant �tre attendu avant la prochaine execution de la coroutine</returns>
    IEnumerator MovementTick()
    {
        bool isTheFirstMove = true;

        while (InGameControls.Movement.MoveLeft.IsPressed() || InGameControls.Movement.MoveRight.IsPressed())
        {
            if (InGameControls.Movement.MoveLeft.IsPressed())
            {
                Playfield.TryMove(Vector2.left);
            }
            else if (InGameControls.Movement.MoveRight.IsPressed())
            {
                Playfield.TryMove(Vector2.right);
            }

            yield return new WaitForSeconds(isTheFirstMove ? 0.13f : 0.1f);

            isTheFirstMove = false;
        }
    }

    /// <summary>
    /// Coroutine li�e � la chute d�cid�e par le jouer du tetromino � un interval r�gulier
    /// </summary>
    /// <returns>Le temps devant �tre attendu avant la prochaine execution de la coroutine</returns>
    IEnumerator FallTick()
    {
        while (InGameControls.Movement.SoftDrop.IsPressed())
        {
            Playfield.TryMoveDown();

            StartUniqueCoroutine("GameTick");

            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// Coroutine g�rant le rythme du jeu en fonction du niveau
    /// </summary>
    /// <returns>Le temps devant �tre attendu avant la prochaine execution de la coroutine</returns>
    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds((60f - Score.Level * 2) / 60);

            Playfield.TryMoveDown();
        }
    }
}