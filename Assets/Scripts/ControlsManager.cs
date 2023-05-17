using System.Collections;
using UnityEngine;

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

    public void PauseGame()
    {
        InGameControls.Disable();

        StopCoroutine("GameTick");
    }

    public void ResumeGame()
    {
        InGameControls.Enable();

        StartUniqueCoroutine("GameTick");
    }


    private void StartUniqueCoroutine(string coroutine)
    {
        StopCoroutine(coroutine);

        StartCoroutine(coroutine);
    }

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

    IEnumerator FallTick()
    {
        while (InGameControls.Movement.SoftDrop.IsPressed())
        {
            Playfield.TryMoveDown();

            StartUniqueCoroutine("GameTick");

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds((60f - Score.Level * 2) / 60);

            Playfield.TryMoveDown();
        }
    }
}
