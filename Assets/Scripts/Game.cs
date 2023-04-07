using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Score score;

    private Playfield playfield;

    private InGameControls InGameControls;

    private Coroutine GameLoopCoroutine, MovementCoroutine, FallCoroutine;

    private void Awake()
    {
        InGameControls = new InGameControls();

        InGameControls.Enable();

        InGameControls.Movement.RotateLeft.performed += _ => playfield.TryRotateLeft();

        InGameControls.Movement.RotateRight.performed += _ => playfield.TryRotateRight();

        InGameControls.Movement.MoveRight.performed += _ => StartMoving();

        InGameControls.Movement.MoveLeft.performed += _ => StartMoving();

        InGameControls.Movement.SoftDrop.performed += _ =>
        {
            if (FallCoroutine != null)
            {
                StopCoroutine(FallCoroutine);
            }
            FallCoroutine = StartCoroutine(FallTick());
        };

        InGameControls.Movement.HardDrop.performed += _ => playfield.HardDrop();
    }

    void Start()
    {
        score = Score.Instance;
        playfield = Playfield.Instance;

        GameLoopCoroutine = StartCoroutine(GameTick());

        MovementCoroutine = StartCoroutine(MovementTick());

        FallCoroutine = StartCoroutine(FallTick());

    }

    private void StartMoving()
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementTick());
        }

        MovementCoroutine = StartCoroutine(MovementTick());
    }

    IEnumerator MovementTick()
    {
        while (InGameControls.Movement.MoveLeft.IsPressed() || InGameControls.Movement.MoveRight.IsPressed())
        {
            if (InGameControls.Movement.MoveLeft.IsPressed())
            {
                playfield.TryMoveLeft();
            }

            if (InGameControls.Movement.MoveRight.IsPressed())
            {
                playfield.TryMoveRight();
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator FallTick()
    {
        while (InGameControls.Movement.SoftDrop.IsPressed())
        {
            playfield.TryMoveDown();

            StopCoroutine(GameLoopCoroutine);
            GameLoopCoroutine = StartCoroutine(GameTick());

            yield return new WaitForSeconds(0.2f);
        }
    }


    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds( (60f-score.Level) / 60);

            playfield.TryMoveDown();
        }
    }
}
