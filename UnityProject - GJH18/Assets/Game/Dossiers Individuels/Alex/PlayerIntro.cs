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
                Game.Instance.sfx.PlayWolfHowl();
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
        List<PlayerInfo> wolfies = Game.Instance.GetWolves();
        Sequence sqc = DOTween.Sequence();
        int doSpawnCount = 0;
        for (int i = 0; i < wolfies.Count; i++)
        {
            wolfies[i].GetComponent<PlayerMovement>().enabled = false;
            wolfies[i].GetComponentInChildren<CharacterOrientation>().enabled = false;
            wolfies[i].GetComponentInChildren<CharacterOrientation>().GetComponentInChildren<Animator>().SetBool("running", true);

            float rotationAngle = ((Vector2)(Game.Instance.map.spawnpointWolfEnter[doSpawnCount].position - wolfies[i].transform.position)).ToAngle();
            Vector3 rotation = transform.forward * rotationAngle;
            wolfies[i].GetComponentInChildren<WolfInfo>().gameObject.transform.rotation = Quaternion.Euler(rotation);

            sqc.Join(wolfies[i].transform.DOMove(Game.Instance.map.spawnpointWolfEnter[doSpawnCount].position, animSpeed));
            doSpawnCount++;
        }
        sqc.OnComplete(() => {
            for (int i = 0; i < wolfies.Count; i++)
            {
                wolfies[i].GetComponent<PlayerMovement>().enabled = true;
                wolfies[i].GetComponentInChildren<CharacterOrientation>().enabled = true;
            }
            onComplete();
        });
    }

    public void IntroTheDoggies(Action onComplete)
    {
        List<PlayerInfo> doggies = Game.Instance.GetDoggies();
        Sequence sqc = DOTween.Sequence();
        int doSpawnCount = 0;
        for (int i = 0; i < doggies.Count; i++)
        {
            doggies[i].GetComponent<PlayerMovement>().enabled = false;
            doggies[i].GetComponentInChildren<CharacterOrientation>().enabled = false;
            doggies[i].GetComponentInChildren<CharacterOrientation>().GetComponentInChildren<Animator>().SetBool("running", true);

            float rotationAngle = ((Vector2)(Game.Instance.map.dogSpawnPoints[doSpawnCount].position - doggies[i].transform.position)).ToAngle();
            Vector3 rotation = transform.forward * rotationAngle;
            doggies[i].GetComponentInChildren<BergerBehavior>().gameObject.transform.rotation = Quaternion.Euler(rotation);

            sqc.Join(doggies[i].transform.DOMove(Game.Instance.map.dogSpawnPoints[doSpawnCount].position, animSpeed));
            doSpawnCount++;
        }
        sqc.OnComplete(() => {
            for (int i = 0; i < doggies.Count; i++)
            {
                doggies[i].GetComponent<PlayerMovement>().enabled = true;
                doggies[i].GetComponentInChildren<CharacterOrientation>().enabled = true;
            }
            onComplete();
        });
    }
}
