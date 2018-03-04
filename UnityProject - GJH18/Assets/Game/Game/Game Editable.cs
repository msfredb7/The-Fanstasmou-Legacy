using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Game
{
    public SceneInfo uiScene;
    [ReadOnly]
    public GameUI gameUI;

    public PlayerIntro intros;

    public Camera mainCamera;

    public SceneInfo tutorial;

    public Map map;
    public Transform unitContainer;

    public MusicManager music;
    public SFXManager sfx;
    public SpawnerSheep SheepSpawn;

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
        playerOne.GetComponent<PlayerMovement>().enabled = false;

        playerTwo = Instantiate(playerPrefab, map.spawnpointPlayerTwo.position, Quaternion.identity, unitContainer);
        playerTwo.player = PlayerInfo.PlayerNumber.Two;
        playerTwo.GetComponent<PlayerMovement>().enabled = false;

        playerThree = Instantiate(playerPrefab, map.spawnpointPlayerThree.position, Quaternion.identity, unitContainer);
        playerThree.player = PlayerInfo.PlayerNumber.Three;
        playerThree.GetComponent<PlayerMovement>().enabled = false;

        playerFour = Instantiate(playerPrefab, map.spawnpointPlayerFour.position, Quaternion.identity, unitContainer);
        playerFour.player = PlayerInfo.PlayerNumber.Four;
        playerFour.GetComponent<PlayerMovement>().enabled = false;
    }

    public List<PlayerInfo> GetWolves()
    {
        List<PlayerInfo> result = new List<PlayerInfo>();
        if (playerOne.isWolf)
            result.Add(playerOne);
        if (playerTwo.isWolf)
            result.Add(playerTwo);
        if (playerThree.isWolf)
            result.Add(playerThree);
        if (playerFour.isWolf)
            result.Add(playerFour);
        return result;
    }

    public List<PlayerInfo> GetDoggies()
    {
        List<PlayerInfo> result = new List<PlayerInfo>();
        if (!playerOne.isWolf)
            result.Add(playerOne);
        if (!playerTwo.isWolf)
            result.Add(playerTwo);
        if (!playerThree.isWolf)
            result.Add(playerThree);
        if (!playerFour.isWolf)
            result.Add(playerFour);
        return result;
    }
}
