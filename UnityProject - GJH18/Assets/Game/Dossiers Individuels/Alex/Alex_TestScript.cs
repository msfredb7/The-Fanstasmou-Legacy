using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_TestScript : MonoBehaviour
{
    void Start()
    {
        Game.Instance.gameAudio.sfxManager.PlayWolfHowl();
    }  
}