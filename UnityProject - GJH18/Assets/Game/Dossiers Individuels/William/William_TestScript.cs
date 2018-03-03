using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William_TestScript : MonoBehaviour
{
    InputPlayerAxis input;
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        input = GetComponent<InputPlayerAxis>();
    }

    private void FixedUpdate()
    {
        if (input.GetPlayerHorizontal() > 0)
            changeTeam("wolfs");
        else if (input.GetPlayerHorizontal() < 0)
        {
            changeTeam("shepherds");
        }
    }

    private void changeTeam(string _teamName)
    {
        if (_teamName == "wolfs")
        {
            rect.anchoredPosition = new Vector3(250, rect.anchoredPosition.y);
        }
        else if (_teamName == "shepherds")
        {
            rect.anchoredPosition = new Vector3(-250, rect.anchoredPosition.y);
        }
    }
}