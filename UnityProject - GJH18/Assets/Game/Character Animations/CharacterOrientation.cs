using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    public Animator animatorController;
    public Transform rotator;
    public bool useRigidbody = false;
    public float sensitivity = 0.4f;

    
    [ReadOnly]
    public PlayerMovement playerMovement;
    [ReadOnly]
    public Rigidbody2D rb;

    private int runHash = Animator.StringToHash("running");

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (useRigidbody)
        {
            if (rb.velocity.magnitude > sensitivity)
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
            animatorController.SetBool(runHash, playerMovement.IsMoving);
            if (playerMovement.IsMoving && playerMovement.LastPlayerInput.magnitude > sensitivity)
            {
                SetAngle(playerMovement.LastPlayerInput);
            }
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
