using System.Collections;
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
    public float triggerSensitivity = 0.2f;

    public Attract attract;
    public Repulse repulse;

    public Attract barkAttract;

    public GameObject RepulseAnimation;
    public GameObject AttractAnimation;

    public float changeModeCooldown = 0.5f;
    private bool canChange = true;

    public float knockbackCooldown = 0.5f;
    private bool canKockback = true;

    public float callingBarkCooldown = 20.0f;
    private bool canCallingBark = true;
    public float callingBarkEffectLength = 3f;

    private InputPlayerButton inputButtons;
    private InputPlayerAxis inputAxis;

    void Start()
    {
        PlayerContainer.Instance.AddBerger(this);
        attract.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        repulse.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        barkAttract.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;

        RepulseAnimation.transform.SetParent(transform.parent);
        AttractAnimation.transform.SetParent(transform.parent);

        AttractAnimation.SetActive(false);
        RepulseAnimation.SetActive(false);

        barkAttract.active = false;



        if (inputButtons == null)
            GetInputButtonsRef();
        if (inputAxis == null)
            GetInputAxisRef();
    }

    void GetInputAxisRef()
    {
        inputAxis = GetComponentInParent<InputPlayerAxis>();
        if (inputAxis == null)
            Debug.Log("wtf wolfy");
    }


    /*
         public float callingBarkCooldown = 8.0f;
    private bool canCallingBark = true;
         */

    void Update ()
    {
        if (inputButtons == null)
            GetInputButtonsRef();
        if (inputAxis == null)
            GetInputAxisRef();

        if (inputAxis.GetPlayerTriggerAxis() > triggerSensitivity)
        {
            if (!canCallingBark)
            {
                return;
            }
            CallingBark();
            canCallingBark = false;
            this.DelayedCall(() => { canCallingBark = true; }, callingBarkCooldown);
        }
        if (inputButtons.GetPlayerA())
        {
            if (!canChange)
            {
                return;
            }
            if(currentMode == BergerMode.Repulse)
            {  
                Game.Instance.sfx.PlayDogBark();
                SetAttract();

            } else
            {
                Game.Instance.sfx.PlayDogGrowl();
                SetRepulse();
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


    private void SetAttract()
    {
        currentMode = BergerMode.Attract;

        attract.active = true;
        repulse.active = false;

        AttractAnimation.transform.localScale =  Vector3.one * attract.range * 0.7f;

        AttractAnimation.SetActive(true);
        RepulseAnimation.SetActive(false);

        canChange = false;
        this.DelayedCall(() => { canChange = true; }, changeModeCooldown);
    }

    private void SetRepulse()
    {
        currentMode = BergerMode.Repulse;

        attract.active = false;
        repulse.active = true;

        RepulseAnimation.transform.localScale = Vector3.one * repulse.range * 0.7f;

        AttractAnimation.SetActive(false);
        RepulseAnimation.SetActive(true);

        canChange = false;
        this.DelayedCall(() => { canChange = true; }, changeModeCooldown);
    }

    public void CallingBark()
    {
        barkAttract.active = true;
        Game.Instance.sfx.PlayDogUltimate();
        this.DelayedCall(() => { barkAttract.active = false; }, callingBarkEffectLength);
    }

    public void ActivateFeedbacks()
    {
        SetAttract();
    }
}
