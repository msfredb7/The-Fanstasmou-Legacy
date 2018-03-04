using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterSelection : MonoBehaviour {

    public SceneInfo GameScene;

    public AudioSource music;

    [SerializeField]
    [Header("À METTRE À 'VRAI' QUAND ON BUILD")]
    bool FourPlayers;
    InputPlayerButton buttons;
    bool isStartPressed = false;
    int wolfDogDifference;

	void Start () {
        buttons = GetComponent<InputPlayerButton>();	
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
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
                loadGameScene();
            }
        }
        wolfDogDifference = 0;
    }

    private void loadGameScene()
    {
        foreach (Transform child in transform)
        {
            SelectionInputs playerSelectionInputs = child.GetComponent<SelectionInputs>();

            if (playerSelectionInputs.team == SelectionInputs.Team.None)
            {
                AddPlayerToTeam(child, playerSelectionInputs);
            }
        }

        music.DOFade(0, 1).OnComplete(null);
        LoadingScreen.TransitionTo(GameScene.SceneName, null);

        isStartPressed = true;
    }

    private void AddPlayerToTeam(Transform _player, SelectionInputs _playerSelectionInputs)
    {
        InputPlayerAxis playerAxis = _player.GetComponent<InputPlayerAxis>();
        if (_playerSelectionInputs.team == SelectionInputs.Team.Shepherd) wolfDogDifference++;
        else if (_playerSelectionInputs.team == SelectionInputs.Team.Wolf) wolfDogDifference--;
        PlayerPrefs.SetInt(playerAxis.player + " team", (int)_playerSelectionInputs.team);
    }

    private void AddPlayerToTeam(Transform _player, SelectionInputs.Team _team)
    {
        PlayerPrefs.SetInt(GetComponent<InputPlayerAxis>().player + " team", (int)_team);
    }

    private bool twoShepherdTwoWolf()
    {
        return wolfDogDifference == 0;
    }
}
