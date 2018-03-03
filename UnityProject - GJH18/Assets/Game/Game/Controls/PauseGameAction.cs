using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameAction : MonoBehaviour {

    public InputPlayerButton inputplayerbutton;
    public float pauseCooldown = 1;
    public bool canPause = true;
    private bool isPause;

	void Update ()
    {
        if (inputplayerbutton.GetPlayerStart())
        {
            if(Game.Instance.gameState == Game.GameState.Started)
            {
                if (canPause)
                {
                    if (!Game.Instance.gameUI.PauseGame())
                    {
                        if (!Game.Instance.gameUI.UnpauseGame())
                        {
                            Debug.Log("WTF");
                            return;
                        }
                    }
                    canPause = false;
                    this.DelayedCall(delegate ()
                    {
                        canPause = true;
                    }, pauseCooldown,true);
                }
            }
        }
	}
}
