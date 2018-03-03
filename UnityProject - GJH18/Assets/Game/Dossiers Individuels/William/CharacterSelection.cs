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
        };
	}

    public void StartGame()
    {
        bool allSelected = true;
        foreach (Transform child in transform)
        {
            SelectionInputs playerSelectionInputs = child.GetComponent<SelectionInputs>();
            if (playerSelectionInputs != null)
            {
                if (playerSelectionInputs.team != SelectionInputs.Team.None)
                {
                    InputPlayerAxis playerAxis = child.GetComponent<InputPlayerAxis>();
                    PlayerPrefs.SetInt(playerAxis.player + " team", (int)playerSelectionInputs.team);
                    Debug.Log("Player " + playerAxis.player + " team: " + PlayerPrefs.GetInt(playerAxis.player + " team"));
                }
                else
                {
                    allSelected = false;
                    break;
                }
            }
        }
        if (!allSelected)
        {
            MessagePopup.DisplayMessage("Veuillez tous sélectionner un camp");
            isStartPressed = false;
        }
        else
        {
            LoadingScreen.TransitionTo(GameScene.SceneName, null);
            isStartPressed = true;
        }
    }
}
