using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public DefaultAudioSourcesRemote sources;
    public AudioAssetGroup dogBark;
    public AudioAssetGroup dogGrowl;
    public AudioAsset wolfHowl;

    public void PlayDogBark()
    {
        sources.PlaySFX_AudioPlayable(dogBark.clips[Random.Range(0, dogBark.clips.Length - 1)]);
    }

    public void PlayDogGrowl()
    {
        sources.PlaySFX_AudioPlayable(dogGrowl.clips[Random.Range(0,dogGrowl.clips.Length-1)]);
    }

    public void PlayWolfHowl()
    {
        sources.PlaySFX_AudioPlayable(wolfHowl);
    }
}
