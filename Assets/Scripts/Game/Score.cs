using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Classe <c>Score</c> g�rant le score, le niveau du jeu et le nombre de lignes nettoy�es.
/// </summary>
public class Score : MonoBehaviour
{
    public static Score Instance;

    public int ScoreCount { private set; get; }

    public int ClearedLineCount { private set; get; }

    public int Level { private set; get; }

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

    /// <summary>
    /// Ajoute le score en fonction de nombre de lignes supprim�es et du niveau
    /// </summary>
    /// <param name="numberOfClearedLines">Nombre de lignes supprim�es</param>
    public void AddScore(int numberOfClearedLines)
    {
        int scoreBase = BaseScoreForBreakedLine[numberOfClearedLines];

        this.ScoreCount += scoreBase * (Level + 1);

        this.ClearedLineCount += numberOfClearedLines;

        this.ComputeLevel();

        RefreshText();
    }

    /// <summary>
    /// Calcule le niveau en fonction du nombre de lignes nettoy�es
    /// </summary>
    public void ComputeLevel()
    {
        int temp = Math.Min(30, ClearedLineCount / 10);

        Level = temp;
    }

    /// <summary>
    /// Rafraichit le texte affich�
    /// </summary>
    private void RefreshText()
    {
        DisplayedText.text = "Score: " + ScoreCount + "\n" + "Lignes: " + ClearedLineCount + "\n" + "Niveau: " + Level;
    }
}
