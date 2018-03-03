using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float sensitivity = 0.15f;
    [HideInInspector]
    public bool movementEnable = false;
    public bool IsMoving { get; private set; }
    [ReadOnly]
    public Vector2 LastPlayerInput;

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
        IsMoving = false;
        Vector2 newTarget = new Vector2();
        if (inputs.GetPlayerHorizontal().Abs() > sensitivity)
        {
            LastPlayerInput.x = inputs.GetPlayerHorizontal();
            newTarget.x = transform.position.x + (LastPlayerInput.x * targetDisplacement);
            IsMoving = true;
        }
        else
        {
            LastPlayerInput.x = 0;
            newTarget.x = transform.position.x;
        }

        if (inputs.GetPlayerVertical().Abs() > sensitivity)
        {
            LastPlayerInput.y = inputs.GetPlayerVertical();
            newTarget.y = transform.position.y + (LastPlayerInput.y * targetDisplacement);
            IsMoving = true;
        }
        else
        {
            LastPlayerInput.y = 0;
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
