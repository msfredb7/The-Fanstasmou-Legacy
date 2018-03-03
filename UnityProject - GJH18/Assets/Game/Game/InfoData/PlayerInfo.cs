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
                if (PlayerPrefs.GetInt("") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Two:
                if (PlayerPrefs.GetInt("") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Three:
                if (PlayerPrefs.GetInt("") == 1)
                    isWolf = true;
                else
                    isWolf = false;
                break;
            case PlayerNumber.Four:
                if (PlayerPrefs.GetInt("") == 1)
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
            Instantiate(Game.Instance.wolfPrefab, transform).GetComponent<WolfInfo>().Init();
        }
        else
        {
            Instantiate(Game.Instance.bergerPrefab, transform).GetComponent<BergerInfo>().Init();
        }
    }
}
