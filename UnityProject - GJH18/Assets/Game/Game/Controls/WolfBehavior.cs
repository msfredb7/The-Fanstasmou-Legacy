using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject scratchAnimation;

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

    public Repulse repulse;

    void Start()
    {
        PlayerContainer.Instance.AddWolves(this);
        repulse.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;

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
            dash();
            canDash = false;
            this.DelayedCall(() => { canDash = true; }, dashCooldown);
        }

        if (IsBumped)
        {
            if (GetComponentInParent<Rigidbody2D>().velocity.magnitude < reactivVelocity)
                ReactivatePlayerMovement();

            reactivationTimer -= Time.deltaTime;
            if (!IsBumped)
                ReactivatePlayerMovement();
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
                {
                    herd.Eat();
                    GameObject instantiatedScratch = Instantiate(scratchAnimation);
                    instantiatedScratch.transform.position = transform.position + (transform.right);
                }
            }
        }
    }

    // BUMP DE OCEAN EMPIRE
    [Header("Settings"), SerializeField]
    float bumpedLinearDrag = 5;
    [SerializeField]
    float reactivVelocity = 0.5f;
    [SerializeField]
    float reactivMaxDelay = 2;

    private const string CAN_ACCELERATE_KEY = "bmp";

    private float reactivationTimer = 0;
    private float standardDrag = 0;

    void Awake()
    {
        standardDrag = GetComponentInParent<Rigidbody2D>().drag;
    }

    public void Bump(Vector2 force)
    {
        GetComponentInParent<Rigidbody2D>().drag = bumpedLinearDrag;

        GetComponentInParent<PlayerMovement>().enabled = false;
        reactivationTimer = reactivMaxDelay;

        GetComponentInParent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    public bool IsBumped
    {
        get { return reactivationTimer > 0; }
    }

    void ReactivatePlayerMovement()
    {
        reactivationTimer = -1;

        GetComponentInParent<Rigidbody2D>().drag = standardDrag;
        GetComponentInParent<PlayerMovement>().enabled = true;
    }
}
