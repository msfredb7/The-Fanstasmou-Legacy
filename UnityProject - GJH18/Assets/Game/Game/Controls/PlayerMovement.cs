using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]
    public bool movementEnable = false;

    [HideInInspector]
    public float accelerationRate;
    [HideInInspector]
    public float maximumSpeed;
    [HideInInspector]
    public float targetDisplacement = 10;
    [HideInInspector]
    public Vector2 currentTarget;

    private InputPlayerAxis inputs;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Game.OnGameStart += Init;
        Game.OnGameStart += EnableMovement;
    }

    void Init()
    {
        inputs = GetComponent<InputPlayerAxis>();
    }

    void Update()
    {
        UpdateTargetPosition();
    }

    public void UpdateTargetPosition()
    {
        Vector2 newTarget = new Vector2();
        if (inputs.GetPlayerHorizontal() != 0)
        {
            newTarget.x = transform.position.x + (inputs.GetPlayerHorizontal() * targetDisplacement);
        }
        else
        {
            newTarget.x = transform.position.x;
        }

        if (inputs.GetPlayerVertical() != 0)
        {
            newTarget.y = transform.position.y + (inputs.GetPlayerVertical() * targetDisplacement);
        }
        else
        {
            newTarget.y = transform.position.y;
        }

        currentTarget = newTarget;
    }

    void FixedUpdate()
    {
        if (movementEnable == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 distance = currentTarget - (Vector2)transform.position;
        Vector2 direction = distance.normalized;
        Vector2 Speed = Vector2.Lerp(
            rb.velocity,
            direction * maximumSpeed,
            FixedLerp.Fix(0.1f * accelerationRate));
        rb.velocity = Speed;
    }

    private void EnableMovement()
    {
        movementEnable = true;
    }
}
