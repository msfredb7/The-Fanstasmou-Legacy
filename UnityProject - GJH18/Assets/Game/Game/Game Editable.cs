using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Game
{
    public SceneInfo uiScene;
    [ReadOnly]
    public GameUI gameUI;

    public SceneInfo tutorial;

    public Map map;
    public Transform unitContainer;

    public MusicManager music;
    public SFXManager sfx;

    [Header("PREFABS")]
    public PlayerInfo playerPrefab;
    public GameObject wolfPrefab;
    public GameObject bergerPrefab;

    [ReadOnly]
    public PlayerInfo playerOne;
    [ReadOnly]
    public PlayerInfo playerTwo;
    [ReadOnly]
    public PlayerInfo playerThree;
    [ReadOnly]
    public PlayerInfo playerFour;

    [Header("INTRO")]
    public float tutorialAppearanceDelay = 2.0f;

    [Header("DEBUG")]
    public bool debugStart = false;

    void FetchAllReferences(Action onComplete)
    {
        SpawnPlayers();

        // C'est ici qu'on peut aller chercher tous les références.

        // UI
        Scenes.Load(uiScene, (uiScene) =>
        {
            gameUI = uiScene.FindRootObject<GameUI>();
            onComplete();
        });
    }

    void SpawnPlayers()
    {
        playerOne = Instantiate(playerPrefab, map.spawnpointPlayerOne.position, Quaternion.identity, unitContainer);
        playerOne.player = PlayerInfo.PlayerNumber.One;

        playerTwo = Instantiate(playerPrefab, map.spawnpointPlayerTwo.position, Quaternion.identity, unitContainer);
        playerTwo.player = PlayerInfo.PlayerNumber.Two;

        playerThree = Instantiate(playerPrefab, map.spawnpointPlayerThree.position, Quaternion.identity, unitContainer);
        playerThree.player = PlayerInfo.PlayerNumber.Three;

        playerFour = Instantiate(playerPrefab, map.spawnpointPlayerFour.position, Quaternion.identity, unitContainer);
        playerFour.player = PlayerInfo.PlayerNumber.Four;
    }
}
