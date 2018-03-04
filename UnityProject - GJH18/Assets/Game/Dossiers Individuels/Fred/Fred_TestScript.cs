using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fred_TestScript : MonoBehaviour
{
    public float strength = 1.2f;
    public float duration = 0.2f;
    public VectorShaker shaker;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            shaker.Shake(strength, duration);
        }
    }
}
