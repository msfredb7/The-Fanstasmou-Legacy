using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William_TestScript : MonoBehaviour
{
    public SceneInfo GameScene;

    private void Start()
    {
        LoadingScreen.TransitionTo(GameScene.SceneName, null);
    }
}