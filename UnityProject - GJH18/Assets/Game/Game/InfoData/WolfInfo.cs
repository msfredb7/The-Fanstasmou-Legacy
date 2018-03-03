using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfInfo : MonoBehaviour {

    public float accelerationRate;
    public float maximumSpeed;

    public void Init()
    {
        GetComponentInParent<PlayerMovement>().accelerationRate = accelerationRate;
        GetComponentInParent<PlayerMovement>().maximumSpeed = maximumSpeed;
    }
}
