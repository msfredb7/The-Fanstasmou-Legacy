﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_SpeedReverse : MonoBehaviour
{
    void Awake()
    {
        var anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetFloat("speed", -1);
    }
}
