using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Score score;

    private Playfield playfield;

    void Start()
    {
        score = Score.Instance;
        playfield = Playfield.Instance;

        StartCoroutine(GameTick());
    }

    IEnumerator GameTick()
    {
        while (true)
        {
            yield return new WaitForSeconds( (60f-score.Level) / 60);
            playfield.ExecuteGameTick();
        }
    }
}
