using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerAxis : MonoBehaviour {

    public enum PlayerNumber
    {
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3
    }

    public PlayerNumber player;
	
	public float GetPlayerAxis(string direction)
    {
        float result = 0;
        switch (player)
        {
            default:
            case PlayerNumber.One:
                result = Input.GetAxis(direction + "PlayerOne");
                break;
            case PlayerNumber.Two:
                result = Input.GetAxis(direction + "PlayerTwo");
                break;
            case PlayerNumber.Three:
                result = Input.GetAxis(direction + "PlayerThree");
                break;
            case PlayerNumber.Four:
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
