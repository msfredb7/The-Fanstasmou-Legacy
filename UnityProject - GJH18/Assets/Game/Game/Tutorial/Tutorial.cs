using CCC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public SceneInfo tutorial;
    public WindowAnimation windowAnim;
    public InputPlayerButton playerInput;
    public Action onComplete;
    private bool canExit = false;

	public void Init(Action onComplete)
    {
        this.onComplete = onComplete;
        canExit = true;
        windowAnim.Open();
    }

    void Update()
    {
        if (playerInput.GetPlayerStart())
        {
            windowAnim.Close(delegate ()
            {
                Scenes.UnloadAsync(tutorial);
                onComplete();
            });
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            windowAnim.Close(delegate ()
            {
                Scenes.UnloadAsync(tutorial);
                onComplete();
            });
        }
    }
}
