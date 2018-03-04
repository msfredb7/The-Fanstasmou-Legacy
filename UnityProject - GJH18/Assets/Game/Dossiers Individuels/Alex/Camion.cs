using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camion : MonoBehaviour {

    public int MaxSheepEvac = 1;
    private int SheepEvacuated = 0;

    public BoxCollider2D triggerBox;
    public GameObject plateformCheckpoint;
    public GameObject camionCheckpoint;
    public float EvacSpeed = 1.25f;


    void OnTriggerEnter2D(Collider2D collider)
    {
        HerdMember sheep = collider.GetComponent<HerdMember>();
        if (sheep != null && SheepEvacuated < MaxSheepEvac)
        {
            SheepEvacuated++;
            if(SheepEvacuated == MaxSheepEvac)
                triggerBox.isTrigger = false;

            EvacSheep(sheep, ()=>
            {
                Game.Instance.carManager.ExitCar(gameObject);
            });
           
        }
    }

    public void EvacSheep(HerdMember sheep, Action OnComplete)
    {
        Sequence sqc = DOTween.Sequence();
        MoveToCheckpoint(sheep.transform, (Vector2)plateformCheckpoint.transform.position, sqc);
        MoveToCheckpoint(sheep.transform, (Vector2)camionCheckpoint.transform.position, sqc);
        sqc.OnComplete(()=>
        {
            sheep.Evac();
            OnComplete.Invoke();
        });
        
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
