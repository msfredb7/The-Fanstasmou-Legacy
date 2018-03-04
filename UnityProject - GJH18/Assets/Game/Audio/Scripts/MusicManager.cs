using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioAssetGroup music;

    public bool playOnStart = true;

    public float introDelay = 1.0f;

    private int musicCount = 0;
    private int musicCountMax;

    public void Start()
    {
        if (playOnStart)
        {
            PlayNextSong();
        } else
        {
            Game.OnGameStart += delegate ()
            {
                PlayNextSong();
            };
        }
    }

    void PlayNextSong()
    {
        musicCountMax = music.clips.Length;
        DefaultAudioSources.TransitionToMusic(music.clips[musicCount]);
        musicCount++;
        if (musicCount > musicCountMax)
            musicCount = 0;
    }
}
