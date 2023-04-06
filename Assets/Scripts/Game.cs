using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Score score;

    private Playfield playfield;

    private InGameControls InGameControls;

    private Coroutine GameLoopCoroutine, MovementCoroutine, FallCoroutine;

    private bool WantToSoftDrop;

    private void Awake()
    {
        InGameControls = new InGameControls();

        InGameControls.Enable();

        InGameControls.Movement.RotateLeft.performed += _ => playfield.CurrentFallingTetromino.RotateLeft();

        InGameControls.Movement.RotateRight.performed += _ => playfield.CurrentFallingTetromino.RotateRight();

        InGameControls.Movement.MoveRight.performed += _ =>
        {
            playfield.TryMoveRight();
            StopCoroutine(MovementCoroutine);
            MovementCoroutine = StartCoroutine(MovementTick());
        };

        InGameControls.Movement.MoveLeft.performed += _ =>
        {
            playfield.TryMoveLeft();
            StopCoroutine(MovementCoroutine);
            MovementCoroutine = StartCoroutine(MovementTick());
        };

        InGameControls.Movement.SoftDrop.performed += _ =>
        {
            playfield.TryMoveDown();

            Debug.Log("rest");

            StopCoroutine(GameLoopCoroutine);
            GameLoopCoroutine = StartCoroutine(GameTick());

            StopCoroutine(FallCoroutine);
            FallCoroutine = StartCoroutine(FallTick());
        };
    }

    void Start()
    {
        score = Score.Instance;
        playfield = Playfield.Instance;

        GameLoopCoroutine = StartCoroutine(GameTick());

        MovementCoroutine = StartCoroutine(MovementTick());

        FallCoroutine = StartCoroutine(FallTick());

    }

    IEnumerator MovementTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);

            if (InGameControls.Movement.MoveLeft.IsPressed())
            {
                playfield.TryMoveLeft();
            }

            if (InGameControls.Movement.MoveRight.IsPressed())
            {
                playfield.TryMoveRight();
            }
        }
    }

    IEnumerator FallTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (InGameControls.Movement.SoftDrop.IsPressed())
            {
                playfield.TryMoveDown();
                StopCoroutine(GameLoopCoroutine);
                GameLoopCoroutine = StartCoroutine(GameTick());

                WantToSoftDrop = false;
            }
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
