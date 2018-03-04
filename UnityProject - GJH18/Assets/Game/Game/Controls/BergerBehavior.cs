﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergerBehavior : MonoBehaviour {

    public enum BergerMode
    {
        Repulse = 0,
        Attract = 1
    }

    public BergerMode currentMode = BergerMode.Repulse;

    public float repulsionStrength = 2.5f;

    public Attract attract;
    public Repulse repulse;

    public GameObject RepulseAnimation;
    public GameObject AttractAnimation;

    public float changeModeCooldown = 0.5f;
    private bool canChange = true;

    public float knockbackCooldown = 0.5f;
    private bool canKockback = true;

    private InputPlayerButton inputButtons;
	
    void Start()
    {
        PlayerContainer.Instance.AddBerger(this);
        attract.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        repulse.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        SetAttract();



        if (inputButtons == null)
            GetInputButtonsRef();

    }

	void Update ()
    {
        if (inputButtons == null)
            GetInputButtonsRef();

        if (inputButtons.GetPlayerA())
        {
            if (!canChange)
            {
                return;
            }
            if(currentMode == BergerMode.Repulse)
            {
                currentMode = BergerMode.Attract;

                Game.Instance.sfx.PlayDogBark();

                SetAttract();

                canChange = false;
                this.DelayedCall(() => { canChange = true; },changeModeCooldown);
            } else
            {
                currentMode = BergerMode.Repulse;

                Game.Instance.sfx.PlayDogGrowl();

                SetRepulse();

                canChange = false;
                this.DelayedCall(() => { canChange = true; }, changeModeCooldown);
            }
        }
    }

    void GetInputButtonsRef()
    {
        inputButtons = GetComponentInParent<InputPlayerButton>();
        if (inputButtons == null)
            Debug.Log("wtf doggy");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.GetComponent<WolfInfo>() != null)
                Repulse(contact.collider.GetComponentInParent<Rigidbody2D>(), contact.otherCollider.GetComponentInParent<Rigidbody2D>().position);
        }
    }

    protected void Repulse(Rigidbody2D target, Vector2 myPosition)
    {
        if (canKockback)
        {
            Game.Instance.sfx.PlayDogWarCry();
            target.GetComponentInChildren<WolfBehavior>().Bump(target.GetComponentInChildren<WolfInfo>().transform.right * -1 * repulsionStrength);
            canKockback = false;
            this.DelayedCall(delegate ()
            {
                canKockback = true;
            }, knockbackCooldown);
        }
    }

    private void ToggleBurgerMode()
    {
        if (currentMode == BergerMode.Attract)
            SetRepulse();
        else
            SetAttract();

    }

    private void SetAttract()
    {
        attract.active = true;
        repulse.active = false;

        AttractAnimation.transform.localScale =  Vector3.one * attract.range * 0.7f;

        AttractAnimation.SetActive(true);
        RepulseAnimation.SetActive(false);
    }

    private void SetRepulse()
    {
        attract.active = false;
        repulse.active = true;

        RepulseAnimation.transform.localScale = Vector3.one * repulse.range * 0.7f;

        AttractAnimation.SetActive(false);
        RepulseAnimation.SetActive(true);
    }
}
