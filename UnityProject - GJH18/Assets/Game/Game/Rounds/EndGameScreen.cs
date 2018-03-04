using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour {

    public Text WinningText;

    

    // Use this for initialization
    void Start () {
		if (Rounds.Instance.TeamOne.NBSheepRescued > Rounds.Instance.TeamTwo.NBSheepRescued)
        {
            //TeamOne wins
            WriteWinningMessage(Rounds.Instance.TeamOne);
        }
        else if (Rounds.Instance.TeamOne.NBSheepRescued < Rounds.Instance.TeamTwo.NBSheepRescued)
        {
            //TeamTwo wins
            WriteWinningMessage(Rounds.Instance.TeamTwo);
        }
        else
        {
            if (Rounds.Instance.TeamOne.NbSheepEaten > Rounds.Instance.TeamTwo.NbSheepEaten)
            {
                //TeamOne wins
                WriteWinningMessage(Rounds.Instance.TeamOne);
            }
            else if (Rounds.Instance.TeamOne.NbSheepEaten > Rounds.Instance.TeamTwo.NbSheepEaten)
            {
                //TeamOne wins
                WriteWinningMessage(Rounds.Instance.TeamTwo);
            }
            else
            {
                //Tie
                WriteWinningMessage();
            }
        }
	}
	
    private void WriteWinningMessage(Team winningTeam)
    {
        WinningText.text += "Player " + winningTeam.PlayersInfo[0].player + " et " + "Player " + winningTeam.PlayersInfo[1].player;
    }

    private void WriteWinningMessage()
    {
        WinningText.text = "Égalité";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
