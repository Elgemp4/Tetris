using System;
using UnityEngine;

/// <summary>
/// La class <c>LeaderBoard</c> g�re le tableau des scores et leurs stockage en m�moire
/// </summary>
public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard Instance { get; private set; }

    private (string date, int score)[] LeaderBoardData = new (string date, int score)[10];

    
    void Start()
    {
        Instance = this;

        LoadLeaderBoard();
    }

    /// <summary>
    /// Ins�re le nouveau score dans le tableau LeaderBoardData
    /// </summary>
    /// <param name="score">Le score devant �tre ajout�</param>
    public void AddNewScore(int score)
    {
        for (int i = 0; i < LeaderBoardData.Length; i++)
        {
            if (score > LeaderBoardData[i].score)
            { 
                InsertAt(i, score);
                break;
            }
        }

        SaveLeaderBoard();
    }

    /// <summary>
    /// Ins�re le score � l'index donn�
    /// </summary>
    /// <param name="index">L'index o� le score doit �tre ins�r�</param>
    /// <param name="score">Le score devant �tre ins�r�</param>
    private void InsertAt(int index, int score)
    {
        for (int i = LeaderBoardData.Length-1; i > index; i--)
        {
            LeaderBoardData[i] = LeaderBoardData[i - 1];
        }

        LeaderBoardData[index] = (System.DateTime.Now.ToString(), score);
    }

    /// <summary>
    /// Charge le tableau des score depuis <c>PlayerPrefs</c>
    /// </summary>
    private void LoadLeaderBoard()
    {
        for (int i = 0; i < LeaderBoardData.Length; i++)
        {
            LeaderBoardData[i] = (PlayerPrefs.GetString("Date_" + i), PlayerPrefs.GetInt("Score_" + i)); ;
        }
    }

    /// <summary>
    /// Sauvegarde le tableau des scores dans <c>PlayerPrefs</c>
    /// </summary>
    private void SaveLeaderBoard()
    { 
        for(int i = 0; i < LeaderBoardData.Length; i++) 
        {
            (string date, int score) = LeaderBoardData[i];
            PlayerPrefs.SetString("Date_" + i, date);
            PlayerPrefs.SetInt("Score_" + i, score);
        }
    }

    public override string ToString()
    {
        string returnedString = "";

        for(int i = 0; i < LeaderBoardData.Length; i++)
        {
            (string date, int score) = LeaderBoardData[i];
            if (!string.IsNullOrEmpty(date))
            {
                returnedString += $"{i + 1}) {date} : {score} \n";
            }
        }

        return returnedString;
    }
}
