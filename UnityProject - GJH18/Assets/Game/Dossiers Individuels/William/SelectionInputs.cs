using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionInputs : MonoBehaviour {
    public enum Team
    {
        None = -1,
        Shepherd = 0,
        Wolf = 1
    }
    InputPlayerAxis axis;
    RectTransform playerRect;

    [SerializeField]
    private RectTransform menuTransform;

    [SerializeField]
    private GameObject menuAudioObject;

    private MenuAudio menuAudio;

    private bool teamSelected = false;
    private bool soundOnCooldown = false;

    public Team team;

    private void Start()
    {
        playerRect = GetComponent<RectTransform>();
        axis = GetComponent<InputPlayerAxis>();
        menuAudio = menuAudioObject.GetComponent<MenuAudio>();

    }

    private void FixedUpdate()
    {
        if (axis.GetPlayerHorizontal() > 0.50f)
        {
            if (team != Team.Wolf)
            changeTeam(1);
        }
        else if (axis.GetPlayerHorizontal() < -0.50f)
        {
            if (team != Team.Shepherd)
                changeTeam(0);
        }
    }
    
    private void changeTeam(int _teamName)
    {
        if (!teamSelected)
        {
            if (!soundOnCooldown)
            {
                menuAudio.PlayNavigationSound();
                soundOnCooldown = true;
                this.DelayedCall(() => 
                {
                    soundOnCooldown = false;
                }, 0.3f);
            }

            float playerSpritePosition = menuTransform.rect.width / 4;
            if (_teamName == 1)
            {
                team = Team.Wolf;
                playerRect.anchoredPosition = new Vector3(playerSpritePosition + playerRect.rect.width, playerRect.anchoredPosition.y);
            }
            else if (_teamName == 0)
            {
                team = Team.Shepherd;
                playerRect.anchoredPosition = new Vector3(-playerSpritePosition, playerRect.anchoredPosition.y);
            }
            teamSelected = true;

            this.DelayedCall(() =>
            {
                teamSelected = false;
            }, 0.3f);
        }
    }
}
