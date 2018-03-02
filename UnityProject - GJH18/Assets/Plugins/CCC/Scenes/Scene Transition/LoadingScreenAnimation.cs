using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoadingScreenAnimation : MonoBehaviour
{
    public Image bg;
    public bool handleCameras = true;
    public Camera cam;

    public Action onCancel;
    private bool animationComplete = false;

    public void Intro(Action onComplete)
    {
        bg.DOFade(1, 1).OnComplete(delegate ()
        {
            if (handleCameras)
            {
                Camera currentCam = Camera.main;
                if (currentCam != null)
                    currentCam.gameObject.SetActive(false);
                cam.gameObject.SetActive(true);
            }
            if (onComplete != null)
                onComplete();
        }).SetUpdate(true);
    }

    public void Outro(Action onComplete)
    {
        if (handleCameras)
            cam.gameObject.SetActive(false);
        bg.DOFade(0, 1).OnComplete(delegate ()
        {
            animationComplete = true;
            if (onComplete != null)
                onComplete();
        }).SetUpdate(true);
    }

    void OnDestroy()
    {
        if (!animationComplete && onCancel != null)
            onCancel();
    }

    public void OnNewSceneLoaded()
    {
        if (handleCameras)
            cam.gameObject.SetActive(false);
    }
}