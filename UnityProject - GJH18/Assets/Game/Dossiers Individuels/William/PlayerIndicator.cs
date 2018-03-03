using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerIndicator : MonoBehaviour {

    private PlayerInfo playerInfo;
    [SerializeField] private GameObject PlayerOneInidicator;
    [SerializeField] private GameObject PlayerTwoInidicator;
    [SerializeField] private GameObject PlayerThreeInidicator; 
    [SerializeField] private GameObject PlayerFourInidicator;

    private GameObject indicator;

    void Start () {
        playerInfo = GetComponent<PlayerInfo>();
    }

    public void ShowPlayerIndicator()
    {
        switch (playerInfo.player)
        {
            default:
            case PlayerInfo.PlayerNumber.One:
                indicator = Instantiate(PlayerOneInidicator, transform);
                break;
            case PlayerInfo.PlayerNumber.Two:
                indicator = Instantiate(PlayerTwoInidicator, transform);
                break;
            case PlayerInfo.PlayerNumber.Three:
                indicator = Instantiate(PlayerThreeInidicator, transform);
                break;
            case PlayerInfo.PlayerNumber.Four:
                indicator = Instantiate(PlayerFourInidicator, transform);
                break;
        }
        indicator.GetComponent<SpriteRenderer>().DOFade(1, 1f);
    }

    public void HidePlayerIndicator()
    {
        indicator.GetComponent<SpriteRenderer>().DOFade(0, 1f).onComplete = onFadeComplet;
    }

    public void ShowWolfIndicator()
    {
        if (playerInfo.isWolf)
        {
            ShowPlayerIndicator();
        }
    }
   
    private void getPlayerIndicator(PlayerInfo.PlayerNumber _playerNumber)
    {
    }

    private void onFadeComplet()
    {
        switch (playerInfo.player)
        {
            default:
            case PlayerInfo.PlayerNumber.One:
                indicator.SetActive(false);
                break;
            case PlayerInfo.PlayerNumber.Two:
                indicator.SetActive(false);
                break;
            case PlayerInfo.PlayerNumber.Three:
                indicator.SetActive(false);
                break;
            case PlayerInfo.PlayerNumber.Four:
                indicator.SetActive(false);
                break;

        }
    }
	
}
