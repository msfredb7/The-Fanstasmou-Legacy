using CCC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public CanvasGroup part1;
    public CanvasGroup part2;
    public Image bg;
    public SceneInfo tutorial;
    public InputPlayerButton playerInput;
    public Action onComplete;
    private bool canExit = false;

    private bool p1Done = false;
    private bool canListen = false;

    public void Init(Action onComplete)
    {
        this.onComplete = onComplete;
        canExit = true;
        part1.DOFade(1, 0.35f).SetUpdate(true);
        bg.DOFade(0.75f, 0.5f).SetUpdate(true).onComplete = () => canListen = true;
    }

    void Awake()
    {
        bg.SetAlpha(0);
        part1.alpha = 0;
    }

    void Start()
    {
        part2.alpha = 0;
    }

    void Update()
    {
        if (!canListen)
            return;
        if (playerInput.GetPlayerStart() || Input.GetKeyDown(KeyCode.Space))
        {
            if (p1Done)
            {
                canListen = false;
                part2.DOFade(0, 0.35f).OnComplete(() =>
                {
                    bg.DOFade(0, 0.5f).SetUpdate(true).OnComplete(delegate ()
                    {
                        Scenes.UnloadAsync(tutorial);
                        onComplete();
                    }).SetUpdate(true);
                }).SetUpdate(true);
            }
            else
            {
                canListen = false;
                part1.DOFade(0, 0.35f).OnComplete(() =>
                {
                    p1Done = true;
                    part2.DOFade(1, 0.35f).OnComplete(() =>
                    {
                        print("wa");
                        canListen = true;
                    }).SetUpdate(true);
                }).SetUpdate(true);
            }
        }
    }
}
