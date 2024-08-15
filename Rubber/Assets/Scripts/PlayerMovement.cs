using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed;
    public float jumpPower;
    public int maxJumps;
    private int availableJumps;

    float moveDirection;

    public InputActionReference move;
    public InputActionReference jump;

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();
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
}
