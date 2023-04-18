using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Score score;

    private Playfield playfield;

    private InGameControls InGameControls;

    private Coroutine GameLoopCoroutine;

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
            StopCoroutine("FallTick");
            
            StartCoroutine(FallTick());
        };

        InGameControls.Movement.HardDrop.performed += _ => playfield.HardDrop();

        InGameControls.Movement.Hold.performed += _ => playfield.Hold();
    }

    void Start()
    {
        score = Score.Instance;
        playfield = Playfield.Instance;

        GameLoopCoroutine = StartCoroutine(GameTick());
    }


    private void StartMoving()
    {
        StopCoroutine("MovementTick");

        StartCoroutine("MovementTick");
    }

    IEnumerator MovementTick()
    {
        bool isTheFirstMove = true;

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
            yield return new WaitForSeconds( (60f-score.Level) / 60);

            playfield.TryMoveDown();
        }
    }
}
