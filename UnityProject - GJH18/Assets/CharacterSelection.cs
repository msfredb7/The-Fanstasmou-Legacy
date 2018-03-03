using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    InputPlayerButton buttons;

	void Start () {
        buttons = GetComponent<InputPlayerButton>();	
	}
	
	void FixedUpdate () {
		if (buttons.GetPlayerStart())
        {
            StartGame();
        };
	}

    public void StartGame()
    {
        foreach (Transform child in transform)
        {
            SelectionInputs playerSelectionInputs = child.GetComponent<SelectionInputs>();
            InputPlayerAxis playerAxis = child.GetComponent<InputPlayerAxis>();

            PlayerPrefs.SetString("Player" + playerAxis.player + "Team", playerSelectionInputs.team.ToString());
            Debug.Log("Player " + playerAxis.player + " team " + PlayerPrefs.GetString("Player" + playerAxis.player +"Team"));
        }
    }
}
