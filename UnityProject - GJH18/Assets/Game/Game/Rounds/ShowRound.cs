using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CCC.UI;

public class ShowRound : MonoBehaviour {

    public SceneInfo RoundSceneInfo;
    public WindowAnimation windowAnimation;
    public Action onComplete;

    public float WindowDuration = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(Action _onComplete)
    {
        this.onComplete = _onComplete;
        windowAnimation.Open();

        this.DelayedCall(() =>
        {
            windowAnimation.Close(delegate ()
            {
                Scenes.UnloadAsync(RoundSceneInfo);
                _onComplete();
            });
        }, WindowDuration);
    }
}
