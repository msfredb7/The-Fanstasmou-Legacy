using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    public Animator animatorController;
    public Transform rotator;
    public bool useRigidbody = false;

    
    [ReadOnly]
    public PlayerMovement playerMovement;
    [ReadOnly]
    public Rigidbody2D rb;

    private int runHash = Animator.StringToHash("running");

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        animatorController.SetBool(runHash, playerMovement.IsMoving);

        if (useRigidbody)
        {
            if (rb.velocity.magnitude > 0.1f)
            {
                SetAngle(rb.velocity);
            }
        }
        else
        {
            if (playerMovement.IsMoving && playerMovement.LastPlayerInput.magnitude > 0.4f)
            {
                SetAngle(playerMovement.LastPlayerInput);
            }
        }
    }

    void SetAngle(Vector2 moveVector)
    {
        SetAngle(playerMovement.LastPlayerInput.ToAngle());
    }
    void SetAngle(float angle)
    {
        rotator.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
