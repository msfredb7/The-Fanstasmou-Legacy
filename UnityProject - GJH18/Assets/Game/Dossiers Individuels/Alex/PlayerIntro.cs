using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerIntro : MonoBehaviour {

    public int wolfIntroDelay = 5;

    public float animSpeed = 1;
    public float indicatorDelay = 1;

    void Start()
    {
        Game.OnGameStart += () =>
        {
            Game.Instance.gameUI.wolfTimer.GetComponent<WolfTimer>().SetCounterValue(wolfIntroDelay);
            Game.Instance.gameUI.wolfTimer.GetComponent<CanvasGroup>().DOFade(1, animSpeed);
            this.DelayedCall(delegate () {
                Game.Instance.gameUI.wolfTimer.GetComponent<CanvasGroup>().DOFade(0, animSpeed);
                IntroTheWolves(delegate () {
                    Game.Instance.playerOne.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                    Game.Instance.playerTwo.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                    Game.Instance.playerThree.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                    Game.Instance.playerFour.GetComponent<PlayerIndicator>().ShowPlayerIndicator();
                    this.DelayedCall(delegate ()
                    {
                        Game.Instance.playerOne.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                        Game.Instance.playerTwo.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                        Game.Instance.playerThree.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                        Game.Instance.playerFour.GetComponent<PlayerIndicator>().HidePlayerIndicator();
                    }, indicatorDelay);
                });
            }, wolfIntroDelay);
        };
    }

    public void IntroTheWolves(Action onComplete)
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
            onComplete();
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
