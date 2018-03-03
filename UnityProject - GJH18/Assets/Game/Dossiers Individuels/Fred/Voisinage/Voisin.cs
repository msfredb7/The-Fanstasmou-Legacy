using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VoisinInfo
{
    public Voisin intance;
    public float distance;
}

public class Voisin : MonoBehaviour
{
    public List<VoisinInfo> otherVoisins = new List<VoisinInfo>();

    public Transform tr;

    void Awake()
    {
        tr = transform;
    }

    void Update()
    {
        UpdateDistances();
    }

    void UpdateDistances()
    {
        for (int i = 0; i < otherVoisins.Count; i++)
        {
            var v = otherVoisins[i];
            v.distance = (v.intance.tr.position - tr.position).magnitude;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        var v = other.GetComponentInParent<Voisin>();
        if (v != null && !other.isTrigger)
        {
            otherVoisins.Add(new VoisinInfo()
            {
                intance = v,
                distance = (tr.position - v.tr.position).magnitude
            });
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        var v = other.GetComponentInParent<Voisin>();
        if (v != null && other.isTrigger)
        {
            for (int i = 0; i < otherVoisins.Count; i++)
            {
                if(otherVoisins[i].intance == v)
                {
                    otherVoisins.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
