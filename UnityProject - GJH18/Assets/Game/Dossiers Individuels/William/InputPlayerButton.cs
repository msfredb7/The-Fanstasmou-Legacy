﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerButton : MonoBehaviour {

    public PlayerInfo.PlayerNumber player;

	public bool GetPlayerStart()
    {
        return Input.GetButton("StartPlayer" + player);
    }
}
