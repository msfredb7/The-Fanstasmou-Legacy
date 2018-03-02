using System;
using UnityEngine;

public partial class Game
{
    public SceneInfo uiScene;
    [ReadOnly]
    public GameUI gameUI;

    void FetchAllReferences(Action onComplete)
    {
        // C'est ici qu'on peut aller chercher tous les références.

        int scenesToLoad = 1;
        int loadCount = 0;

        Scenes.Load(uiScene, (scene) =>
        {
            gameUI = scene.FindRootObject<GameUI>();

            loadCount++;
            if (scenesToLoad == loadCount)
                onComplete();
        });
    }
}
