using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int ScoreCount { private set; get; }

    public int ClearedLineCount { private set; get; }

    public int Level { private set; get; }

    public bool HasLeveledUp { private set; get; }

    [SerializeField]
    private GameObject TextObject;

    private TextMeshProUGUI DisplayedText;

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

        DisplayedText = TextObject.GetComponent<TextMeshProUGUI>();

        RefreshText();
    }

    public void AddScore(int numberOfClearedLines)
    {
        int scoreBase = BaseScoreForBreakedLine[numberOfClearedLines];

        this.HasLeveledUp = false;

        this.ScoreCount += scoreBase * (Level + 1);

        this.ClearedLineCount += numberOfClearedLines;

        this.ComputeLevel();

        RefreshText();
    }

    public void ComputeLevel()
    {
        int temp = Math.Min(30, ClearedLineCount / 10);

        if (temp > Level)
            HasLeveledUp = true;

        Level = temp;
    }

    private void RefreshText()
    {
        DisplayedText.text = "Score: " + ScoreCount + "\n" + "Lines: " + ClearedLineCount + "\n" + "Level: " + Level;
        Debug.Log("Text refreshed");
    }
}
