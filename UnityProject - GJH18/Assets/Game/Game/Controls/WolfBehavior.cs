using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehavior : MonoBehaviour {

    public float dashCooldown = 0.5f;
    [SerializeField]
    private float dashSpeed = 10f;
    [SerializeField]
    private float dashAcceleration = 10f;
    [SerializeField]
    private float dashDuration = 0.2f;
    private bool canDash = true;

    public float eatCooldown = 0.5f;
    private bool canEat = true;

    private InputPlayerButton inputButtons;

    void Start()
    {
        if (inputButtons == null)
            GetInputButtonsRef();
    }

    void Update()
    {
        if (inputButtons == null)
            GetInputButtonsRef();

        if (inputButtons.GetPlayerA())
        {
            if (!canEat)
            {
                return;
            }
            Debug.Log("EAT THE SHEEP");
            canEat = false;
            this.DelayedCall(() => { canEat = true; }, eatCooldown);
        } else if (inputButtons.GetPlayerB())
        {
            if (!canDash)
            {
                return;
            }
            Debug.Log("DASH WOLFIE");
            dash();
            canDash = false;
            this.DelayedCall(() => { canDash = true; }, dashCooldown);
        }
    }

    void GetInputButtonsRef()
    {
        inputButtons = GetComponentInParent<InputPlayerButton>();
        if (inputButtons == null)
            Debug.Log("wtf doggy");
    }

    void dash()
    {
        //Ça serait peut-être mieux de faire un vrai dash qu'un simple mouvement speed boost de 0.1 secondes
        transform.parent.GetComponent<PlayerMovement>().maximumSpeed = dashSpeed;
        transform.parent.GetComponent<PlayerMovement>().accelerationRate = dashAcceleration;
        this.DelayedCall(() => {
            transform.parent.GetComponent<PlayerMovement>().maximumSpeed = GetComponent<WolfInfo>().maximumSpeed;
            transform.parent.GetComponent<PlayerMovement>().accelerationRate = GetComponent<WolfInfo>().accelerationRate;
        }, dashDuration);
        Debug.Log(dashCooldown);
    }
}
