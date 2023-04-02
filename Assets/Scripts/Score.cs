using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int ScoreCount { private set; get; }

    public int ClearedLineCount { private set; get; }

    public int Level { private set; get; }

    public bool HasLeveledUp { private set; get; }

    public static Dictionary<int, int> BaseScoreForBreakedLine = new Dictionary<int, int>()
    {
        { 1,40 },
        { 2, 100 },
        { 3, 300 },
        { 4, 1200 }
    };


    void Start()
    {
        Instance = this;
    }

    public void AddScore(int level, int numberOfClearedLines)
    {
        int scoreBase = BaseScoreForBreakedLine[numberOfClearedLines];

        this.HasLeveledUp = false;

        this.ScoreCount += scoreBase * (level + 1);

        this.ClearedLineCount += numberOfClearedLines;

        this.ComputeLevel();
    }

    public void ComputeLevel()
    {
        int temp = Math.Min(30, ClearedLineCount / 10);

        if (temp > Level)
            HasLeveledUp = true;

        Level = temp;
    }
}
