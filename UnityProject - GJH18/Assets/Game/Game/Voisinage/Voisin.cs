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

    public Vector2 myDirection;
    public float mySpeed;

    [HideInInspector]
    public Transform tr;
    [HideInInspector]
    public Rigidbody2D rb;

    void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateDistances();
        myDirection = tr.right;
        mySpeed = rb.velocity.magnitude;
    }

    void UpdateDistances()
    {
        for (int i = 0; i < otherVoisins.Count; i++)
        {
            var v = otherVoisins[i];
            v.distance = (v.intance.tr.position - tr.position).magnitude;
        }
    }

    public List<Voisin> GetVoisinsInRange(float range)
    {
        List<Voisin> voisins = new List<Voisin>();
        for (int i = 0; i < otherVoisins.Count; i++)
        {
            if (otherVoisins[i].distance < range)
                voisins.Add(otherVoisins[i].intance);
        }
        return voisins;
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
        if (v != null && !other.isTrigger)
        {
            for (int i = 0; i < otherVoisins.Count; i++)
            {
                if (otherVoisins[i].intance == v)
                {
                    otherVoisins.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
