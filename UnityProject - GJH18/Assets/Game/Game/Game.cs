using UnityEngine;
using CCC.DesignPattern;
using System.Collections.Generic;

public delegate void SimpleEvent();

public partial class Game : PublicSingleton<Game>
{
    // GAME STATE
    [System.NonSerialized, HideInInspector]
    public GameState gameState = GameState.NotReady;
    static private event SimpleEvent onGameReady;
    static private event SimpleEvent onGameStart;

    public enum GameState { NotReady, Ready, Started, Over }

    [HideInInspector]
    public Locker gameRunning = new Locker();

    static public event SimpleEvent OnGameReady
    {
        add
        {
            if (instance != null && instance.gameState >= GameState.Ready)
                value();
            else
                onGameReady += value;
        }
        remove { onGameReady -= value; }
    }

    static public event SimpleEvent OnGameStart
    {
        add
        {
            if (instance != null && instance.gameState >= GameState.Started)
                value();
            else
                onGameStart += value;
        }
        remove { onGameStart -= value; }
    }

    private void GameRunning_onLockStateChange(bool state)
    {
        if (state)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onGameReady = null;
        onGameStart = null;
    }

    protected override void Awake()
    {
        base.Awake();
        gameRunning.onLockStateChange += GameRunning_onLockStateChange;

        PersistentLoader.LoadIfNotLoaded(InitGame);
    }

    void InitGame()
    {
        FetchAllReferences(()=>
        {
            ReadyGame();
            // DEBUG START
            if (debugStart)
            {
                intros.IntroTheDoggies(StartGame);
            }
            else
            {
                // TUTORIAL
                this.DelayedCall(() =>
                {
                    if (Rounds.Instance.CurrentRound > 1)
                    {
                        Rounds.Instance.BeginNextRound(delegate ()
                        {
                            gameUI.DogsArrive(delegate () {
                                intros.IntroTheDoggies(delegate () {
                                    //Indicators
                                    playerOne.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                    playerTwo.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                    playerThree.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                    playerFour.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                    this.DelayedCall(delegate ()
                                    {
                                        playerOne.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                        playerTwo.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                        playerThree.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                        playerFour.GetComponent<PlayerIndicator>().HidePlayerIndicator();

                                    }, 2);
                                    StartGame();
                                });
                            });
                        });
                    }
                    else
                    {
                        Scenes.Load(tutorial, (tutorialScene) =>
                        {
                            tutorialScene.FindRootObject<Tutorial>().Init(delegate ()
                            {
                                // DOGS ARRIVE
                                Rounds.Instance.BeginNextRound(delegate ()
                                {
                                    gameUI.DogsArrive(delegate () {
                                        intros.IntroTheDoggies(delegate () {
                                            List<PlayerInfo> doogies = GetDoggies();
                                            for (int i = 0; i < doogies.Count; i++)
                                            {
                                                doogies[i].GetComponentInChildren<BergerBehavior>().ActivateFeedbacks();
                                                doogies[i].GetComponent<PlayerMovement>().enabled = true;
                                            }

                                            //Indicators
                                            playerOne.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                            playerTwo.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                            playerThree.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                            playerFour.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                                            this.DelayedCall(delegate ()
                                            {
                                                playerOne.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                                playerTwo.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                                playerThree.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                                                playerFour.GetComponent<PlayerIndicator>().HidePlayerIndicator();

                                            }, 2);
                                            StartGame();
                                        });
                                    });
                                });
                            });
                        });
                    }
                }, tutorialAppearanceDelay);
                
            }
        });
    }

    void ReadyGame()
    {
        if (gameState >= GameState.Ready)
            return;

        gameState = GameState.Ready;

        LoadingScreen.OnNewSetupComplete();

        if (onGameReady != null)
        {
            onGameReady();
            onGameReady = null;
        }
    }
    void StartGame()
    {
        if (gameState >= GameState.Started)
            return;

        gameState = GameState.Started;

        // Init Game Start Events
        if (onGameStart != null)
        {
            onGameStart();
            onGameStart = null;
        }
    }

    public void EndGame()
    {
        // End Game Screen
        if (gameState >= GameState.Over)
            return;

        gameState = GameState.Over;
    }
}
