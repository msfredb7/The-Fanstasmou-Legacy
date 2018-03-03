using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (indicator == null)
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
        }
        else
        {
            SetIndicatorsActive(true);
        }
       
    }

    public void HidePlayerIndicator()
    {
        SetIndicatorsActive(false);
    }

    private void SetIndicatorsActive(bool _isActive)
    {
        switch (playerInfo.player)
        {
            default:
            case PlayerInfo.PlayerNumber.One:
                indicator.SetActive(_isActive);
                break;
            case PlayerInfo.PlayerNumber.Two:
                indicator.SetActive(_isActive);
                break;
            case PlayerInfo.PlayerNumber.Three:
                indicator.SetActive(_isActive);
                break;
            case PlayerInfo.PlayerNumber.Four:
                indicator.SetActive(_isActive);
                break;

        }
    }
	
}
