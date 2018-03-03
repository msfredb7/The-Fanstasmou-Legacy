using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public enum PlayerNumber
    {
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3
    }

    public PlayerNumber player;

    public bool isWolf = false;

    void Awake()
    {
        Game.OnGameReady += OnGameReady;
    }

    public void OnGameReady()
    {
        LoadPlayerInfo();
        LoadPlayerCharacterParams();
    }

    private void LoadPlayerInfo()
    {
        switch (player)
        {
            default:
            case PlayerNumber.One:
                if (PlayerPrefs.GetInt("PlayerOneTeam") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Two:
                if (PlayerPrefs.GetInt("PlayerTwoTeam") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Three:
                if (PlayerPrefs.GetInt("PlayerThreeTeam") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Four:
                if (PlayerPrefs.GetInt("PlayerFourTeam") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
        }
    }
	
    private void LoadPlayerCharacterParams()
    {
        if (isWolf)
        {
            var child = Instantiate(Game.Instance.wolfPrefab, transform);
            child.GetComponent<WolfInfo>().Init();
            child.transform.localPosition = Vector3.zero;
        }
        else
        {
            var child = Instantiate(Game.Instance.bergerPrefab, transform);
            child.GetComponent<BergerInfo>().Init();
            child.transform.localPosition = Vector3.zero;
        }
    }
}
