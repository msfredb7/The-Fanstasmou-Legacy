using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    public Animator animatorController;
    public Transform rotator;
    public bool useRigidbody = false;
    public float sensitivity = 0.4f;
    public bool setSpeed = false;
    public float speedScale = 0.4f;


    [ReadOnly]
    public PlayerMovement playerMovement;
    [ReadOnly]
    public Rigidbody2D rb;

    private int runHash = Animator.StringToHash("running");
    private int speedHash = Animator.StringToHash("speed");

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float speed = 0;
        if (useRigidbody)
        {
            speed = rb.velocity.magnitude;
            if (speed > sensitivity)
            {
                animatorController.SetBool(runHash, true);
                SetAngle(rb.velocity);
            }
            else
            {
                animatorController.SetBool(runHash, false);
            }
        }
        else
        {
            speed = playerMovement.LastPlayerInput.magnitude;
            animatorController.SetBool(runHash, playerMovement.IsMoving);
            if (playerMovement.IsMoving && speed > sensitivity)
            {
                SetAngle(playerMovement.LastPlayerInput);
            }
        }

        if (setSpeed)
        {
            animatorController.SetFloat(speedHash, speed * speedScale);
        }
    }

    void SetAngle(Vector2 moveVector)
    {
        SetAngle(moveVector.ToAngle());
    }
    void SetAngle(float angle)
    {
        rotator.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
