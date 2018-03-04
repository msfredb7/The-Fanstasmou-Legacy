using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioAssetGroup dogBark;
    public AudioAssetGroup dogGrowl;
    public AudioAssetGroup dogWarCry;
    public AudioAsset wolfHowl;
    public AudioAssetGroup dogHurt;
    public AudioAssetGroup wolfDash;
    public AudioAsset wolfBite;
    public AudioAssetGroup GrassExplode;
    public AudioAsset dogUltimate;
    public AudioAsset TruckArrive;
    public AudioAsset dogHowl;
    public AudioAsset Wind;
    public AudioSource AmbianceLoopSource;

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

    public void PlayDogWarCry()
    {
        DefaultAudioSources.PlaySFX(dogWarCry.clips[Random.Range(0,dogGrowl.clips.Length-1)]);
    }

    public void PlayWolfDashSound()
    {
        DefaultAudioSources.PlaySFX(wolfDash.clips[Random.Range(0, wolfDash.clips.Length - 1)]);
    }

    public void PlayDogHurtSound()
    {
        DefaultAudioSources.PlaySFX(dogHurt.clips[Random.Range(0, dogHurt.clips.Length - 1)],0,0.5f);
    }

    public void PlayWolfBiteSound()
    {
        DefaultAudioSources.PlaySFX(wolfBite);
    }

    public void PlayGrassExplode()
    {
        DefaultAudioSources.PlaySFX(GrassExplode.clips[Random.Range(0, GrassExplode.clips.Length - 1)]);
    }

    public void PlayDogUltimate()
    {
        DefaultAudioSources.PlaySFX(dogUltimate);
    }

    public void PlayTruckArrive()
    {
        DefaultAudioSources.PlaySFX(TruckArrive);
    }

    public void PlayDogHowl()
    {
        DefaultAudioSources.PlaySFX(dogHowl);
    }

    public void PlayWind()
    {
        DefaultAudioSources.PlaySFX(Wind, 0.0f, 1.0f, AmbianceLoopSource);
    }
}
