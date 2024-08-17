using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public RopeAnchor rope;

    public float moveSpeed;
    public float jumpPower;
    public int maxJumps;
    private int availableJumps;
    public Animator animator;

    float moveDirection;

    public InputActionReference move;
    public InputActionReference jump;

    private Vector3 respawnPosition;

    private void Start()
    {
        respawnPosition = this.transform.position;
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();
        if (move.action.ReadValue<float>()==0)
        {
            animator.SetBool("IsWalking",false);
        }
        else
        {
            animator.SetBool("IsWalking",true);
            if (move.action.ReadValue<float>() == 0)
            {
                
            }
        }
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveDirection*moveSpeed,0,0));
    }

    private void OnEnable()
    {
        jump.action.started += Jump;
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        
        if (availableJumps > 0)
        {
            rb.AddForce(0,jumpPower,0);
            availableJumps--;
        }
        Debug.Log("Jump"+availableJumps);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Jumps refilled");
            availableJumps = maxJumps;
        }
    }

    public void respawn()
    {
        rb.MovePosition(respawnPosition);
        rb.velocity=Vector3.zero;
        rope.ThrowArm();
    }

    public void setRespawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }
}
