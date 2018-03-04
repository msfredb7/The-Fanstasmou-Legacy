using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camion : MonoBehaviour {

    public int amountOfSheep = 1;


    //public BoxCollider2D collsiion;
    public GameObject plateformCheckpoint;
    public GameObject camionCheckpoint;
    public float EvacSpeed = 1.25f;


    void OnTriggerEnter2D(Collider2D collider)
    {
        HerdMember sheep = collider.GetComponent<HerdMember>();
        if (sheep != null)
        {
            EvacSheep(sheep);
            Game.Instance.carManager.ExitCar(gameObject);
        }
    }

    public void EvacSheep(HerdMember sheep)
    {
        Sequence sqc = DOTween.Sequence();
        MoveToCheckpoint(sheep.transform, (Vector2)plateformCheckpoint.transform.position, sqc);
        MoveToCheckpoint(sheep.transform, (Vector2)camionCheckpoint.transform.position, sqc);
        sheep.Evac();
    }

    public void MoveToCheckpoint(Transform sheep, Vector2 checkpoint, Sequence sqc)
    {
        float rotationAngle = ((Vector2)sheep.transform.position - checkpoint).ToAngle();
        Vector3 rotation = transform.forward * rotationAngle;
        sheep.rotation = Quaternion.Euler(rotation);

        float distance = (checkpoint - (Vector2)sheep.position).magnitude;
        float animationLength = distance / EvacSpeed;

        sqc.Append(sheep.DOMove((Vector3)checkpoint, animationLength));
    }
}
