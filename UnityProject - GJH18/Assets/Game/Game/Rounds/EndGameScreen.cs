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
        WinningText.text += "Joueur " + convertToFrench(winningTeam.PlayersInfo[0].player) + " et " + "Joueur " + convertToFrench(winningTeam.PlayersInfo[1].player);
    }

    private void WriteWinningMessage()
    {
        WinningText.text = "Égalité";
    }

    private string convertToFrench(PlayerInfo.PlayerNumber _playerNumber)
    {
        switch (_playerNumber)
        {
            default:
            case PlayerInfo.PlayerNumber.One:
                return "Un";
            case PlayerInfo.PlayerNumber.Two:
                return "Deux";
            case PlayerInfo.PlayerNumber.Three:
                return "Trois";
            case PlayerInfo.PlayerNumber.Four:
                return "Quatre";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
