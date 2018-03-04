using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WolfBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject scratchAnimation;
    [SerializeField]
    private GameObject dashTrailPrefab;
    [SerializeField]
    private GameObject eyeBurst;
    [SerializeField]
    private Transform leftEye;
    [SerializeField]
    private Transform rightEye;

    [SerializeField]
    private Transform normalEyes;


    public GameObject RepusleAnimation;

    private GameObject leftTrail;
    private GameObject rightTrail;

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

    public const int maxSheepEaten = 1;

    public float triggerSensitivity = 0.2f;

    private InputPlayerButton inputButtons;
    private InputPlayerAxis inputAxis;

    SheepDetector sheepDetector;

    public Repulse repulse;

    void Start()
    {
        PlayerContainer.Instance.AddWolves(this);

        repulse.owner = GetComponentInParent<Rigidbody2D>() as Rigidbody2D;
        RepusleAnimation.transform.SetParent(transform.parent);
        RepusleAnimation.transform.localScale = Vector3.one * repulse.range * 0.7f;
        RepusleAnimation.SetActive(false);

        if (inputButtons == null)
            GetInputButtonsRef();
        if (inputAxis == null)
            GetInputAxisRef();
        sheepDetector = GetComponent<SheepDetector>();
        if(sheepDetector != null)
        {
            if(sheepDetector.onSheepInRange != null)
            {
                sheepDetector.onSheepInRange.AddListener(delegate() {
                    Herd herd = sheepDetector.GetHerd();
                    if (herd != null && herd.MemberCount() <= maxSheepEaten)
                        Game.Instance.gameUI.buttonPopUp.FocusPopupOnPosition(Game.Instance.mainCamera.WorldToScreenPoint(transform.position), ButtonPopUp.ButtonType.Trigger, 0.5f, "EAT");
                });
            }
        }
    }

    void Update()
    {
        if (inputButtons == null)
            GetInputButtonsRef();
        if (inputAxis.GetPlayerTriggerAxis() > triggerSensitivity)
        {
            if (!canEat)
            {
                return;
            }
            EatSheep();
            canEat = false;
            this.DelayedCall(() => { canEat = true; }, eatCooldown);
        } else if (inputButtons.GetPlayerA())
        {
            if (!canDash)
            {
                return;
            }
            Game.Instance.sfx.PlayWolfDashSound();
            dash();
            canDash = false;
            this.DelayedCall(() => 
            {
                canDash = true;
                normalEyes.GetComponent<SpriteRenderer>().DOFade(1, 0.1f).OnComplete(delegate 
                {
                    Instantiate(eyeBurst, leftEye.transform.position, Quaternion.identity, leftEye);
                    Instantiate(eyeBurst, rightEye.transform.position, Quaternion.identity, rightEye);
                });
            }, dashCooldown);
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
            Debug.Log("wtf wolfy");
    }

    void GetInputAxisRef()
    {
        inputAxis = GetComponentInParent<InputPlayerAxis>();
        if (inputAxis == null)
            Debug.Log("wtf wolfy");
    }

    void dash()
    {
        transform.parent.GetComponent<PlayerMovement>().maximumSpeed = dashSpeed;
        transform.parent.GetComponent<PlayerMovement>().accelerationRate = dashAcceleration;
        spawnTrail();
        this.DelayedCall(() =>
        {
            transform.parent.GetComponent<PlayerMovement>().maximumSpeed = GetComponent<WolfInfo>().maximumSpeed;
            transform.parent.GetComponent<PlayerMovement>().accelerationRate = GetComponent<WolfInfo>().accelerationRate;
            deleteTrail();
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
                    GameObject instantiatedScratch = Instantiate(scratchAnimation);
                    instantiatedScratch.transform.position = herd.GetMiddle();
                    herd.Eat();
                }
            }
        }
    }
    void spawnTrail()
    {
        leftTrail = Instantiate(dashTrailPrefab, leftEye.transform.position, Quaternion.identity, leftEye);
        rightTrail = Instantiate(dashTrailPrefab, rightEye.transform.position, Quaternion.identity, rightEye);

        leftTrail.GetComponent<TrailRenderer>().enabled = true;
        rightTrail.GetComponent<TrailRenderer>().enabled = true;
    }

    void deleteTrail()
    {
        leftTrail.transform.SetParent(null);
        rightTrail.transform.SetParent(null);
        normalEyes.GetComponent<SpriteRenderer>().DOFade(0, 0);
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
        Game.Instance.sfx.PlayDogHurtSound();
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

    public void ActivateFeedbacks()
    {
        RepusleAnimation.SetActive(true);
       
    }
}
