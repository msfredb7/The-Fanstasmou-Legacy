using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float sensitivity = 0.15f;
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
        Game.OnGameStart += OnGameStart;
    }

    void OnGameStart()
    {
        inputs = GetComponent<InputPlayerAxis>();
        EnableMovement();
    }

    void Update()
    {
        UpdateTargetPosition();
    }

    public void UpdateTargetPosition()
    {
        Vector2 newTarget = new Vector2();
        if (inputs.GetPlayerHorizontal().Abs() > sensitivity)
        {
            newTarget.x = transform.position.x + (inputs.GetPlayerHorizontal() * targetDisplacement);
        }
        else
        {
            newTarget.x = transform.position.x;
        }

        if (inputs.GetPlayerVertical().Abs() > sensitivity)
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
