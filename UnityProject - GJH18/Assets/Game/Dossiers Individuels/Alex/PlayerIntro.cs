using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerIntro : MonoBehaviour {

    public float wolfIntroDelay;

    public float animSpeed = 1;

    void Start()
    {
        Game.OnGameStart += () =>
        {
            // Start UI Countdown HERE
            this.DelayedCall(IntroTheWolves, wolfIntroDelay);
        };
    }

    public void IntroTheWolves()
    {
        Debug.Log("Wolf intro");
        List<PlayerInfo> wolfies = Game.Instance.GetWolves();
        Sequence sqc = DOTween.Sequence();
        int doSpawnCount = 0;
        for (int i = 0; i < wolfies.Count; i++)
        {
            wolfies[i].GetComponent<PlayerMovement>().enabled = false;
            sqc.Join(wolfies[i].transform.DOMove(Game.Instance.map.dogSpawnPoints[doSpawnCount].position, animSpeed));
            doSpawnCount++;
        }
        sqc.OnComplete(() => {
            for (int i = 0; i < wolfies.Count; i++)
                wolfies[i].GetComponent<PlayerMovement>().enabled = true;
        });
    }

    public void IntroTheDoggies(Action onComplete)
    {
        Debug.Log("Doggy intro");
        List<PlayerInfo> doggies = Game.Instance.GetDoggies();
        Sequence sqc = DOTween.Sequence();
        int doSpawnCount = 0;
        for (int i = 0; i < doggies.Count; i++)
        {
            doggies[i].GetComponent<PlayerMovement>().enabled = false;
            sqc.Join(doggies[i].transform.DOMove(Game.Instance.map.dogSpawnPoints[doSpawnCount].position, animSpeed));
            doSpawnCount++;
        }
        sqc.OnComplete(() => {
            for (int i = 0; i < doggies.Count; i++)
                doggies[i].GetComponent<PlayerMovement>().enabled = true;
            onComplete();
        });
    }
}
