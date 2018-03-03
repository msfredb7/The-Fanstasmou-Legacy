using System;
using UnityEngine;

public partial class Game
{
    public SceneInfo uiScene;
    [ReadOnly]
    public GameUI gameUI;

    public Map map;

    [Header("PREFABS")]
    public PlayerInfo playerPrefab;
    public GameObject wolfPrefab;
    public GameObject bergerPrefab;

    [HideInInspector]
    public PlayerInfo playerOne;
    [HideInInspector]
    public PlayerInfo playerTwo;
    [HideInInspector]
    public PlayerInfo playerThree;
    [HideInInspector]
    public PlayerInfo playerFour;

    void Start()
    {
        SpawnPlayers();

        ReadyGame();
        StartGame();
    }

    void FetchAllReferences(Action onComplete)
    {
        // C'est ici qu'on peut aller chercher tous les références.

        int scenesToLoad = 1;
        int loadCount = 0;

        Scenes.Load(uiScene, (scene) =>
        {
            gameUI = scene.FindRootObject<GameUI>();

            loadCount++;
            if (scenesToLoad == loadCount)
                onComplete();
        });
    }

    void SpawnPlayers()
    {
        playerOne = Instantiate(playerPrefab,map.spawnpointPlayerOne.position,Quaternion.identity);
        playerOne.player = PlayerInfo.PlayerNumber.One;

        playerTwo = Instantiate(playerPrefab, map.spawnpointPlayerTwo.position, Quaternion.identity);
        playerTwo.player = PlayerInfo.PlayerNumber.Two;

        playerThree = Instantiate(playerPrefab, map.spawnpointPlayerThree.position, Quaternion.identity);
        playerThree.player = PlayerInfo.PlayerNumber.Three;

        playerFour = Instantiate(playerPrefab, map.spawnpointPlayerFour.position, Quaternion.identity);
        playerFour.player = PlayerInfo.PlayerNumber.Four;
    }
}
