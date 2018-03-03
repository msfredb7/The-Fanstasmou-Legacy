using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerAxis : MonoBehaviour {

    public PlayerInfo.PlayerNumber player;
	
	public float GetPlayerAxis(string direction)
    {
        PlayerInfo.PlayerNumber realPlayerNumber;
        if (Game.Instance != null)
            realPlayerNumber = GetComponent<PlayerInfo>().player;
        else
            realPlayerNumber = player;

        float result = 0;

        switch (realPlayerNumber)
        {
            default:
            case PlayerInfo.PlayerNumber.One:
                result = Input.GetAxis(direction + "PlayerOne");
                break;
            case PlayerInfo.PlayerNumber.Two:
                result = Input.GetAxis(direction + "PlayerTwo");
                break;
            case PlayerInfo.PlayerNumber.Three:
                result = Input.GetAxis(direction + "PlayerThree");
                break;
            case PlayerInfo.PlayerNumber.Four:
                result = Input.GetAxis(direction + "PlayerFour");
                break;
        }
        return result;
    }

    public float GetPlayerHorizontal()
    {
        return GetPlayerAxis("Horizontal");
    }

    public float GetPlayerVertical()
    {
        return GetPlayerAxis("Vertical");
    }
}
