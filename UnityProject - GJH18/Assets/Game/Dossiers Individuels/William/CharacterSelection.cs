using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    public SceneInfo GameScene;

    InputPlayerButton buttons;

    bool isStartPressed = false;

	void Start () {
        buttons = GetComponent<InputPlayerButton>();	
	}
	
	void FixedUpdate () {
		if (buttons.GetPlayerStart() && !isStartPressed)
        {
            StartGame();
            isStartPressed = true;
        };
	}

    public void StartGame()
    {
        foreach (Transform child in transform)
        {
            SelectionInputs playerSelectionInputs = child.GetComponent<SelectionInputs>();
            if (playerSelectionInputs != null)
            {
                InputPlayerAxis playerAxis = child.GetComponent<InputPlayerAxis>();

                PlayerPrefs.SetString("Player" + playerAxis.player + "Team", playerSelectionInputs.team.ToString());
                Debug.Log("Player " + playerAxis.player + " team " + PlayerPrefs.GetString("Player" + playerAxis.player + "Team"));
            }
        }
        LoadingScreen.TransitionTo(GameScene.SceneName, null);
    }
}
