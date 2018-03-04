using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

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
                isWolf = PlayerPrefs.GetInt(PlayerNumber.One + " team") == 1;
                break;
            case PlayerNumber.Two:
                isWolf = PlayerPrefs.GetInt(PlayerNumber.Two + " team") == 1;
                break;
            case PlayerNumber.Three:
                isWolf = PlayerPrefs.GetInt(PlayerNumber.Three + " team") == 1;
                break;
            case PlayerNumber.Four:
                isWolf = PlayerPrefs.GetInt(PlayerNumber.Four + " team") == 1;
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
