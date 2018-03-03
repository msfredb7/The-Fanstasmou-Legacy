using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Repulse{

    public bool active;
    public float range;
    public float strength;
    public Rigidbody2D owner;

    Repulse(bool ac, float rg, float str, Rigidbody2D ow)
    {
        active = ac;
        range = rg;
        strength = str;
        owner = ow;
    }

    public Vector2 position
    {
        get { return (Vector2)owner.transform.position; }
    }
}
