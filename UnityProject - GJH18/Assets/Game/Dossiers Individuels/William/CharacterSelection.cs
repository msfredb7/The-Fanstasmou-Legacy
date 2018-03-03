using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    public SceneInfo GameScene;

    [SerializeField]
    [Header("À METTRE À 'VRAI' QUAND ON BUILD")]
    bool FourPlayers;
    InputPlayerButton buttons;
    bool isStartPressed = false;
    int wolfDogDifference;

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
                    AddPlayerToTeam(child, playerSelectionInputs);
                }
                else
                {
                    allSelected = false;
                    break;
                }
            }
        }
        if (!allSelected && FourPlayers)
        {
            MessagePopup.DisplayMessage("Veuillez tous sélectionner un camp");
            isStartPressed = false;
        }
        else
        {
            if (FourPlayers && !twoShepherdTwoWolf())
            {
                MessagePopup.DisplayMessage("Il faut avoir 2 bergers et 2 loups pour pouvoir jouer");
                
            }
            else
            {
                LoadingScreen.TransitionTo(GameScene.SceneName, null);
                isStartPressed = true;
            }
        }
        wolfDogDifference = 0;
    }

    private void AddPlayerToTeam(Transform _player, SelectionInputs _playerSelectionInputs)
    {
        InputPlayerAxis playerAxis = _player.GetComponent<InputPlayerAxis>();
        if (_playerSelectionInputs.team == SelectionInputs.Team.Shepherd) wolfDogDifference++;
        else if (_playerSelectionInputs.team == SelectionInputs.Team.Wolf) wolfDogDifference--;
        PlayerPrefs.SetInt(playerAxis.player + " team", (int)_playerSelectionInputs.team);
        Debug.Log("Player " + playerAxis.player + " team: " + PlayerPrefs.GetInt(playerAxis.player + " team"));
    }

    private bool twoShepherdTwoWolf()
    {
        return wolfDogDifference == 0;
    }
}
