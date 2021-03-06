﻿using System;
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

    public GameObject sheepCaughtPrefab;


    public GameObject popUpLocation;

    void OnTriggerStay2D(Collider2D collider)
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
        sheep.DisableUI();
        sheep.GetComponent<SheepComponent>().AIenabled = false;

        Game.Instance.sfx.PlaySaveMouton();
        Sequence sqc = DOTween.Sequence();
        MoveToCheckpoint(sheep.transform, (Vector2)plateformCheckpoint.transform.position, sqc);
        sqc.AppendCallback(() =>
        {
            Instantiate(sheepCaughtPrefab, popUpLocation.transform.position, Quaternion.identity);
        });
        MoveToCheckpoint(sheep.transform, (Vector2)camionCheckpoint.transform.position, sqc);
        sqc.OnComplete(()=>
        {
            sheep.Evac(null);
            OnComplete.Invoke();
        });
        
    }

    public void MoveToCheckpoint(Transform sheep, Vector2 checkpoint, Sequence sqc)
    {
        float rotationAngle = (checkpoint - (Vector2)sheep.transform.position).ToAngle();
        Vector3 rotation = Vector3.forward * rotationAngle;
        Debug.Log(rotation);
        sheep.GetComponent<CharacterOrientation>().enabled = false;
        sheep.rotation = Quaternion.Euler(rotation);


        float distance = (checkpoint - (Vector2)sheep.position).magnitude;
        float animationLength = distance / EvacSpeed;

        sqc.Append(sheep.DOMove((Vector3)checkpoint, animationLength));
    }
}
