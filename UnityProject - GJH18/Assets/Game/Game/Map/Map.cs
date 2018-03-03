using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public Transform spawnpointPlayerOne;
    public Transform spawnpointPlayerTwo;
    public Transform spawnpointPlayerThree;
    public Transform spawnpointPlayerFour;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnpointPlayerOne.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerTwo.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerThree.position, 0.25f);
        Gizmos.DrawSphere(spawnpointPlayerFour.position, 0.25f);
    }
}
