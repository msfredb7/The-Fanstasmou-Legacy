using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioAssetGroup dogBark;
    public AudioAssetGroup dogGrowl;
    public AudioAsset wolfHowl;

    void Start()
    {
        Game.OnGameReady += delegate ()
        {
            PlayWolfHowl();
        };
    }

    public void PlayDogBark()
    {
        DefaultAudioSources.PlaySFX(dogBark.clips[Random.Range(0, dogBark.clips.Length - 1)]);
    }

    public void PlayDogGrowl()
    {
        DefaultAudioSources.PlaySFX(dogGrowl.clips[Random.Range(0,dogGrowl.clips.Length-1)]);
    }

    public void PlayWolfHowl()
    {
        DefaultAudioSources.PlaySFX(wolfHowl);
    }
}
