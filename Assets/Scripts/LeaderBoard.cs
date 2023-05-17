using System;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard Instance { get; private set; }

    private (string date, int score)[] LeaderBoardData = new (string date, int score)[10];

    
    void Start()
    {
        Instance = this;

        LoadLeaderBoard();
    }

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

    private void InsertAt(int index, int score)
    {
        for (int i = LeaderBoardData.Length-1; i > index; i--)
        {
            LeaderBoardData[i] = LeaderBoardData[i - 1];
        }

        LeaderBoardData[index] = (System.DateTime.Now.ToString(), score);
    }

    private void LoadLeaderBoard()
    {
        for (int i = 0; i < LeaderBoardData.Length; i++)
        {
            LeaderBoardData[i] = (PlayerPrefs.GetString("Date_" + i), PlayerPrefs.GetInt("Score_" + i)); ;
        }
    }

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
