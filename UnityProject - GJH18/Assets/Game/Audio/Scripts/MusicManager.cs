using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public DefaultAudioSourcesRemote sources;
    public AudioAssetGroup music;

    private int musicCount = 0;
    private int musicCountMax;

    public void Start()
    {
        PlayNextSong();
    }

    void PlayNextSong()
    {
        musicCountMax = music.clips.Length;
        sources.PlayMusic_AudioPlayable(music.clips[musicCount]);
        musicCount++;
        if (musicCount > musicCountMax)
            musicCount = 0;
    }
}
