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
}
