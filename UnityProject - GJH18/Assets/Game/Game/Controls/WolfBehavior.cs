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

    public int maxSheepEaten = 1;

    private InputPlayerButton inputButtons;

    SheepDetector sheepDetector;

    void Start()
    {
        if (inputButtons == null)
            GetInputButtonsRef();
        sheepDetector = GetComponent<SheepDetector>();
        if(sheepDetector != null)
        {
            if(sheepDetector.onSheepInRange != null)
            {
                sheepDetector.onSheepInRange.AddListener(delegate() {
                    Herd herd = sheepDetector.GetHerd();
                    if (herd != null && herd.MemberCount() <= maxSheepEaten)
                        Game.Instance.gameUI.buttonPopUp.FocusPopupOnPosition(Game.Instance.mainCamera.WorldToScreenPoint(transform.position), ButtonPopUp.ButtonType.A, 0.5f, "EAT");
                });
            }
        }
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
            EatSheep();
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
        transform.parent.GetComponent<PlayerMovement>().maximumSpeed = dashSpeed;
        transform.parent.GetComponent<PlayerMovement>().accelerationRate = dashAcceleration;
        this.DelayedCall(() =>
        {
            transform.parent.GetComponent<PlayerMovement>().maximumSpeed = GetComponent<WolfInfo>().maximumSpeed;
            transform.parent.GetComponent<PlayerMovement>().accelerationRate = GetComponent<WolfInfo>().accelerationRate;
        }, dashDuration);
    }

    void EatSheep()
    {
        if (sheepDetector != null)
        {
            if(sheepDetector.sheepsInRange.Count > 0 )
            {
                Herd herd = sheepDetector.GetHerd();
                if (herd.MemberCount() <= maxSheepEaten)
                    herd.Eat();
            }
        }
    }
}
