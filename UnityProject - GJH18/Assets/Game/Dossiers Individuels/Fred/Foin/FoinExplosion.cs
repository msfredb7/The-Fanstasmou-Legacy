using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoinExplosion : MonoBehaviour
{
    public GameObject prefab;
    void OnDestroy()
    {
        if (Application.isPlaying)
        {
            prefab.Duplicate(transform.position, Quaternion.identity);
        }
    }
}
