using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CCC.DesignPattern;

public class Rounds : PublicSingleton<Rounds>
{

    [HideInInspector]
    public int CurrentRound;

    public Action onComplet;

    public SceneInfo BeginRoundScene;
    public SceneInfo EndRoundScene;
    public SceneInfo GameSceneInfo;

    public List<PlayerInfo> TeamOne;
    public List<PlayerInfo> TeamTwo;

    void Start () {
        CurrentRound = 1;
    }
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            EndRound(OnRoundEnd);
        }
	}

    public void BeginNextRound(Action _onComplete)
    {
        if (CurrentRound % 2 == 1)
        {
            TeamOne = new List<PlayerInfo>();
            TeamTwo = new List<PlayerInfo>();

            TeamOne = Game.Instance.GetDoggies();
            TeamTwo = Game.Instance.GetWolves();
        }
        else if (CurrentRound % 2 == 0)
        {
            TeamOne = Game.Instance.GetWolves();
            TeamTwo = Game.Instance.GetDoggies();
        }
        Scenes.Load(BeginRoundScene, (RoundScene) => 
        {
            RoundScene.FindRootObject<ShowRound>().Show(_onComplete);
        });
    }

    public void EndRound(Action _onComplete)
    {
        Scenes.Load(EndRoundScene, (EndRoundScene) =>
        {
            EndRoundScene.FindRootObject<ShowRound>().Show(_onComplete);
        });
    }

    public void OnRoundEnd()
    {
        LoadingScreen.TransitionTo(GameSceneInfo.SceneName, null);
        SwapTeams();
        CurrentRound++;
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
