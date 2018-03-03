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

    public Attract attract;
    public Repulse repulse;

    public float changeModeCooldown = 0.5f;
    private bool canChange = true;

    private InputPlayerButton inputButtons;
	
    void Start()
    {
        PlayerContainer.Instance.AddBerger(this);

        attract.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        repulse.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;

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

                attract.active = true;
                repulse.active = false;

                canChange = false;
                this.DelayedCall(() => { canChange = true; },changeModeCooldown);
            } else
            {
                currentMode = BergerMode.Repulse;

                attract.active = false;
                repulse.active = true;

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

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.GetComponent<WolfInfo>() != null)
                Repulse(GetComponentInParent<Rigidbody2D>(), contact.otherCollider.GetComponentInParent<Rigidbody2D>().position);
        }
    }

    protected void Repulse(Rigidbody2D target, Vector2 myPosition)
    {
        var repulsionDirection = (target.position - myPosition).normalized;
        target.AddForce(repulsionDirection * repulsionStrength, ForceMode2D.Impulse);
    }
}
