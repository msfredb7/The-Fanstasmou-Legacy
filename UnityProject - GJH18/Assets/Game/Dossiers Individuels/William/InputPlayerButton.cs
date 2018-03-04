using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerButton : MonoBehaviour {

    public PlayerInfo.PlayerNumber player;

	public bool GetPlayerStart()
    {
        return Input.GetButton("StartPlayer" + GetPlayerInfo());
    }

    public bool GetPlayerA()
    {
        return Input.GetButton("APlayer" + GetPlayerInfo());
    }

    public bool GetPlayerB()
    {
        return Input.GetButton("BPlayer" + GetPlayerInfo());
    }

    public bool GetPlayerX()
    {
        return Input.GetButton("XPlayer" + GetPlayerInfo());
    }

    public bool GetPlayerY()
    {
        return Input.GetButton("YPlayer" + GetPlayerInfo());
    }

    public PlayerInfo.PlayerNumber GetPlayerInfo()
    {
        PlayerInfo.PlayerNumber realPlayerNumber;
        if (Game.Instance != null)
        {
            if (GetComponent<PlayerInfo>() != null)
            {
                realPlayerNumber = GetComponent<PlayerInfo>().player;
                player = realPlayerNumber;
            }
            else
                realPlayerNumber = player;
        }
        else
            realPlayerNumber = player;
        return realPlayerNumber;
    }
}
