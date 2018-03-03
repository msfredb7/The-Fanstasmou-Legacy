using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameUI : MonoBehaviour {

    public ButtonPopUp buttonPopUp;

    public GameObject countdown1;
    public GameObject countdown2;
    public GameObject countdown3;
    public float countdownFadeDuration = 1;

    public GameObject wolfTimer;

    [SerializeField]
    private GameObject pauseOverlay;

    private bool gameIsPaused = false;

    public bool PauseGame()
    {
        if (gameIsPaused)
            return false;
        Time.timeScale = 0;
        pauseOverlay.SetActive(true);
        gameIsPaused = true;
        return true;
    }

    public bool UnpauseGame()
    {
        if (!gameIsPaused)
            return false;
        Time.timeScale = 1;
        pauseOverlay.SetActive(false);
        gameIsPaused = false;
        return true;
    }

    public void Countdown(Action onComplete)
    {
        countdown3.GetComponent<Text>().color = countdown3.GetComponent<Text>().color.ChangedAlpha(0);
        countdown2.GetComponent<Text>().color = countdown2.GetComponent<Text>().color.ChangedAlpha(0);
        countdown1.GetComponent<Text>().color = countdown1.GetComponent<Text>().color.ChangedAlpha(0);
        countdown3.SetActive(true);
        countdown2.SetActive(true);
        countdown1.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdown3.GetComponent<Text>().DOFade(1, countdownFadeDuration));
        sequence.Append(countdown3.GetComponent<Text>().DOFade(0, countdownFadeDuration));
        sequence.Append(countdown2.GetComponent<Text>().DOFade(1, countdownFadeDuration));
        sequence.Append(countdown2.GetComponent<Text>().DOFade(0, countdownFadeDuration));
        sequence.Append(countdown1.GetComponent<Text>().DOFade(1, countdownFadeDuration));
        sequence.Append(countdown1.GetComponent<Text>().DOFade(0, countdownFadeDuration));
        sequence.OnComplete(() => { onComplete(); });
    }
}
