using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private Score score;

    private Playfield playfield;

    private IShowable pauseMenu;

    private InGameControls InGameControls;

    private Coroutine GameLoopCoroutine;

    private void Awake()
    {
        Instance = this;

        InGameControls = new InGameControls();

        InGameControls.Enable();

        InGameControls.Movement.RotateLeft.performed += _ => playfield.TryRotate(Tetromino.LEFT);

        InGameControls.Movement.RotateRight.performed += _ => playfield.TryRotate(Tetromino.RIGHT);

        InGameControls.Movement.MoveRight.performed += _ => StartMoving();

        InGameControls.Movement.MoveLeft.performed += _ => StartMoving();

        InGameControls.Movement.SoftDrop.performed += _ => StartFalling();

        InGameControls.Movement.HardDrop.performed += _ => playfield.HardDrop();

        InGameControls.Movement.Hold.performed += _ => playfield.Hold();

        InGameControls.Movement.Pause.performed += _ =>
        {
            PauseGame();

            pauseMenu.ShowMenu();
        };
    }

    void Start()
    {
        score = Score.Instance;

        playfield = Playfield.Instance;

        pauseMenu = PauseMenu.Instance;

        Debug.Log(pauseMenu);

        GameLoopCoroutine = StartCoroutine(GameTick());
    }

    public void PauseGame()
    {
        InGameControls.Disable();

        StopCoroutine(GameLoopCoroutine);
    }

    public void ResumeGame()
    {
        InGameControls.Enable();

        GameLoopCoroutine = StartCoroutine(GameTick());
    }

    private void StartMoving()
    {
        StopCoroutine("MovementTick");

        StartCoroutine("MovementTick");
    }

    private void StartFalling()
    {
        StopCoroutine("FallTick");

        StartCoroutine("FallTick");
    }

    IEnumerator MovementTick()
    {
        bool isTheFirstMove = true;

        while (InGameControls.Movement.MoveLeft.IsPressed() || InGameControls.Movement.MoveRight.IsPressed())
        {
            if (InGameControls.Movement.MoveLeft.IsPressed())
            {
                playfield.TryMove(Vector2.left);
            }

            if (InGameControls.Movement.MoveRight.IsPressed())
            {
                playfield.TryMove(Vector2.right);
            }

            yield return new WaitForSeconds(isTheFirstMove ? 0.1f : 0.1f);

            isTheFirstMove = false;
        }
    }

    IEnumerator FallTick()
    {
        while (InGameControls.Movement.SoftDrop.IsPressed())
        {
            playfield.TryMoveDown();

            StopCoroutine(GameLoopCoroutine);
            GameLoopCoroutine = StartCoroutine(GameTick());

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds( (60f-score.Level*2) / 60);

            playfield.TryMoveDown();
        }
    }
}
