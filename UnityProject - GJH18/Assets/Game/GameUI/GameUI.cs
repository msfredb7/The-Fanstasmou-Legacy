using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameUI : MonoBehaviour {

    public ButtonPopUp buttonPopUp;

    public AudioMixerSaver audioMixerSaver;
    public float pauseMenuVolume = -40;

    public Timer timer;

    public Image dogsArrive;
    public Image wolfsArrive;
    public float animalArriveAnimDuration = 1;

    [SerializeField]
    private GameObject pauseOverlay;

    private bool gameIsPaused = false;

    public bool PauseGame()
    {
        if (gameIsPaused)
            return false;

        // Change Volume in Pause menu
        DOTween.To(() => audioMixerSaver.GetVolume(AudioMixerSaver.ChannelType.Music), (x) =>
        {
            audioMixerSaver.SetVolume(AudioMixerSaver.ChannelType.Music, x);
        }, pauseMenuVolume, 1);

        Time.timeScale = 0;
        pauseOverlay.SetActive(true);
        gameIsPaused = true;
        return true;
    }

    public bool UnpauseGame()
    {
        if (!gameIsPaused)
            return false;

        // Change Volume in Pause menu
        DOTween.To(() => audioMixerSaver.GetVolume(AudioMixerSaver.ChannelType.Music), (x) =>
        {
            audioMixerSaver.SetVolume(AudioMixerSaver.ChannelType.Music, x);
        }, 0, 1);

        Time.timeScale = 1;
        pauseOverlay.SetActive(false);
        gameIsPaused = false;
        return true;
    }

    public void DogsArrive(Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(dogsArrive.DOFade(1, animalArriveAnimDuration / 2));
        sequence.Join(dogsArrive.transform.DOScale(1.5f,animalArriveAnimDuration / 2));
        sequence.Append(dogsArrive.DOFade(0, animalArriveAnimDuration / 2));
        sequence.OnComplete(() => { onComplete(); });
    }

    public void WolfsArrive(Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(wolfsArrive.DOFade(1, animalArriveAnimDuration / 2));
        sequence.Join(wolfsArrive.transform.DOScale(1.5f, animalArriveAnimDuration / 2));
        sequence.Append(wolfsArrive.DOFade(0, animalArriveAnimDuration / 2));
        sequence.OnComplete(() => { onComplete(); });
    }
}
