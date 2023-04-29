using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private Score Score;

    private Playfield Playfield;

    private GameMenuManager MenuManager;

    private InGameControls InGameControls;

    private Coroutine GameLoopCoroutine;

    private void Awake()
    {
        Instance = this;

        InGameControls = new InGameControls();

        InGameControls.Enable();

        InGameControls.Movement.RotateLeft.performed += _ => Playfield.TryRotate(Tetromino.LEFT);

        InGameControls.Movement.RotateRight.performed += _ => Playfield.TryRotate(Tetromino.RIGHT);

        InGameControls.Movement.MoveRight.performed += _ => StartMoving();

        InGameControls.Movement.MoveLeft.performed += _ => StartMoving();

        InGameControls.Movement.SoftDrop.performed += _ => StartFalling();

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
                Playfield.TryMove(Vector2.left);
            }

            if (InGameControls.Movement.MoveRight.IsPressed())
            {
                Playfield.TryMove(Vector2.right);
            }

            yield return new WaitForSeconds(isTheFirstMove ? 0.1f : 0.1f);

            isTheFirstMove = false;
        }
    }

    IEnumerator FallTick()
    {
        while (InGameControls.Movement.SoftDrop.IsPressed())
        {
            Playfield.TryMoveDown();

            StopCoroutine(GameLoopCoroutine);
            GameLoopCoroutine = StartCoroutine(GameTick());

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds( (60f-Score.Level*2) / 60);

            Playfield.TryMoveDown();
        }
    }
}
