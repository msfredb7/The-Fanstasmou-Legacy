using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    // Players
    public Transform spawnpointPlayerOne;
    public Transform spawnpointPlayerTwo;
    public Transform spawnpointPlayerThree;
    public Transform spawnpointPlayerFour;
    public Transform spawnpointWolfEnter1;
    public Transform spawnpointWolfEnter2;
    public Transform spawnpointDog1;
    public Transform spawnpointDog2;

    // Cars
    public List<Transform> carSpawnPoint;
    public List<Transform> carEnterPoint;

    public List<Transform> spawnpointWolfEnter;
    public List<Transform> dogSpawnPoints;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnpointPlayerOne.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerTwo.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerThree.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerFour.position, 0.25f);
        Gizmos.DrawSphere(spawnpointWolfEnter1.position, 0.25f);
        Gizmos.DrawSphere(spawnpointWolfEnter2.position, 0.25f);
        Gizmos.DrawSphere(spawnpointDog1.position, 0.25f);
        Gizmos.DrawSphere(spawnpointDog2.position, 0.25f);
    }
}
