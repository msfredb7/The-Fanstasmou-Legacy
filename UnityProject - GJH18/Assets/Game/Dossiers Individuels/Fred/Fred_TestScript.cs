using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fred_TestScript : MonoBehaviour
{
    public void LoadScene(SceneInfo sceneInfo)
    {
        //PersistentLoader.LoadIfNotLoaded();
        LoadingScreen.TransitionTo(sceneInfo.SceneName, null);
    }
}
