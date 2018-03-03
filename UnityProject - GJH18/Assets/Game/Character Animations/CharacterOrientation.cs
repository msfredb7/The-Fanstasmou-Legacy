using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    public Animator animatorController;
    public Transform rotator;


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

        if (playerMovement.IsMoving && playerMovement.LastPlayerInput.magnitude > 0.4f)
        {
            rotator.rotation = Quaternion.Euler(Vector3.forward * playerMovement.LastPlayerInput.ToAngle());
        }
    }
}
