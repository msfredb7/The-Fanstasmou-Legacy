using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CCC.DesignPattern;

public class Rounds : PublicSingleton<Rounds>
{
    [HideInInspector]
    public int CurrentRound;

    public int nbRounds = 4;

    public Action onComplet;

    public SceneInfo BeginRoundScene;
    public SceneInfo EndRoundScene;
    public SceneInfo GameSceneInfo;
    public SceneInfo EndGameSceneInfo;
    public SceneInfo CharacterSelectSceneInfo;

    public Team TeamOne;
    public Team TeamTwo;

    private bool roundEnded;

    void Start () {
        CurrentRound = 1;
        TeamOne = new Team();
        TeamTwo = new Team();
    }
	
	void Update () {
        if (!roundEnded && HerdList.Instance != null && HerdList.Instance.GetSheepCount() == 0)
        {
            roundEnded = true;
            EndRound(OnRoundEnd);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            EndRound(OnRoundEnd);
        }

	}

    public void BeginNextRound(Action _onComplete)
    {
        if (CurrentRound % 2 == 1)
        {
            TeamOne.PlayersInfo = Game.Instance.GetDoggies();
            TeamTwo.PlayersInfo = Game.Instance.GetWolves();
        }
        else if (CurrentRound % 2 == 0)
        {
            TeamOne.PlayersInfo = Game.Instance.GetWolves();
            TeamTwo.PlayersInfo = Game.Instance.GetDoggies();
        }
        Scenes.Load(BeginRoundScene, (RoundScene) => 
        {
            RoundScene.FindRootObject<ShowRound>().Show(_onComplete);
        });
    }

    public void EndRound(Action _onComplete)
    {
        if (CurrentRound == nbRounds)
        {
            Scenes.Load(EndRoundScene, (EndRoundScene) =>
            {
                EndRoundScene.FindRootObject<ShowRound>().Show(OnGameEnd);
            });
        }
        else
        {
            Scenes.Load(EndRoundScene, (EndRoundScene) =>
            {
                EndRoundScene.FindRootObject<ShowRound>().Show(_onComplete);
            });
        }
    }

    public void EndGame(Action _onComplete)
    {
        Scenes.Load(EndGameSceneInfo, (EndGameScene) =>
        {
            EndGameScene.FindRootObject<ShowRound>().Show(_onComplete);
        });
    }

    public void OnRoundEnd()
    {
        LoadingScreen.TransitionTo(GameSceneInfo.SceneName, null);
        SwapTeams();
        CurrentRound++;
        roundEnded = false;
    }

    public void OnGameEnd()
    {
        Scenes.Load(EndGameSceneInfo, (EndGameScene) =>
        {
            EndGameScene.FindRootObject<ShowRound>().Show(delegate()
            {
                LoadingScreen.TransitionTo(CharacterSelectSceneInfo.SceneName, null);
            });
        });
    }

    public void AddSheepRescued(int _nbSheepRescued, PlayerInfo _playerInfo)
    {
        if (TeamOne.PlayersInfo.Contains(_playerInfo))
        {
            TeamOne.NBSheepRescued += _nbSheepRescued;
        }
        else if (TeamTwo.PlayersInfo.Contains(_playerInfo))
        {
            TeamTwo.NBSheepRescued += _nbSheepRescued;
        }
    }

    public void AddSheepEaten(int _nbSheepEaten, PlayerInfo _playerInfo)
    {
        if (TeamOne.PlayersInfo.Contains(_playerInfo))
        {
            TeamOne.NbSheepEaten += _nbSheepEaten;
        }
        else if (TeamTwo.PlayersInfo.Contains(_playerInfo))
        {
            TeamTwo.NbSheepEaten += _nbSheepEaten;
        }
    }

    private void SwapTeams()
    {
        foreach (PlayerInfo dog in Game.Instance.GetDoggies())
        {
            dog.SwapTeamsAtEndRound();
        }
        foreach (PlayerInfo wolf in Game.Instance.GetWolves())
        {
            wolf.SwapTeamsAtEndRound();
        }
    }
}
