using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeliverySpot : MonoBehaviour
{
    public BoxCollider2D collider;
    public GameObject Checkpoint1;
    public GameObject Checkpoint2;

    public float EvacSpeed = 1.25f;

    public bool trigger = false;

    public void Start()
    {
        collider.isTrigger = trigger;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            trigger = !trigger;
            collider.isTrigger = trigger;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("In");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Out");
    }

    public void EvacSheep(HerdMember sheep)
    {
        Sequence sqc = DOTween.Sequence();
        MoveToCheckpoint(sheep.transform, (Vector2)Checkpoint1.transform.position, sqc);
        MoveToCheckpoint(sheep.transform, (Vector2)Checkpoint2.transform.position, sqc);
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


