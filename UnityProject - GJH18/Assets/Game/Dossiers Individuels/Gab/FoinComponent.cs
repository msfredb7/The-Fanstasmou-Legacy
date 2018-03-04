using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoinComponent : MonoBehaviour
{

    public Transform tr;

    bool m_contact;

    SpawnerSheep m_SpawnS;

    // Use this for initialization
    void Start()
    {
        m_contact = false;
        tr = GetComponent<Transform>() as Transform;
        if (Game.Instance != null)
        {
            m_SpawnS = Game.Instance.SheepSpawn;
        }
        else
        {
            m_SpawnS = transform.Find("SpawnZoneSheep").GetComponent<SpawnerSheep>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sheep") && m_contact == false)
        {
            m_contact = true;

            m_SpawnS.SpawnPrefAtLocation(tr.position);

            gameObject.GetComponentInParent<SpawnerFoin>().FoinRemove(tr);

            Destroy(gameObject);
        }
    }
}
