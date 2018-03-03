using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionInputs : MonoBehaviour {
    public enum Team
    {
        None,
        Shepherd,
        Wolf
    }
    InputPlayerAxis axis;
    InputPlayerButton buttons;
    RectTransform rect;

    public Team team;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        axis = GetComponent<InputPlayerAxis>();
        buttons = GetComponent<InputPlayerButton>();
    }

    private void FixedUpdate()
    {
        if (axis.GetPlayerHorizontal() > 0)
            changeTeam("wolfs");
        else if (axis.GetPlayerHorizontal() < 0)
        {
            changeTeam("shepherds");
        }
        
        if (buttons != null)
        {
            if (buttons.GetPlayerStart())
            {
                //PlayerPrefs.SetString("player" + axis.player + "team", )
            }
        }
    }

    private void changeTeam(string _teamName)
    {
        if (_teamName == "wolfs")
        {
            team = Team.Wolf;
            rect.anchoredPosition = new Vector3(250, rect.anchoredPosition.y);
        }
        else if (_teamName == "shepherds")
        {
            team = Team.Shepherd;
            rect.anchoredPosition = new Vector3(-250, rect.anchoredPosition.y);
        }
    }
}
