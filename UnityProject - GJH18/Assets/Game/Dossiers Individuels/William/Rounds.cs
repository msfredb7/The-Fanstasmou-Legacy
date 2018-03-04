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
        CurrentRound++;
    }
}
