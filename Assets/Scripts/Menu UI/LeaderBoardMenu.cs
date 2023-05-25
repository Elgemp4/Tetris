using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardMenu : Menu
{
    public static LeaderBoardMenu Instance { get; private set; }

    [SerializeField]
    private GameObject TextObject;

    private TextMeshProUGUI DisplayedText;


    private LeaderBoard _LeaderBoard;

    protected override void PostStart()
    {
        Instance = this;

        _LeaderBoard = LeaderBoard.Instance;

        DisplayedText = TextObject.GetComponent<TextMeshProUGUI>();
    }

    protected override void OnShow()
    {
        DisplayedText.text = _LeaderBoard.ToString();
    }

}
