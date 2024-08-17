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

    public bool ropeAbility=false;
    public bool keyFound = false;
    public bool end;
    public Transform endpoint;
    public Door EndDoor;

    public float moveSpeed;
    public float jumpPower;
    public int maxJumps;
    private int availableJumps;
    public Animator animator;

    float moveDirection;

    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference slingshot;

    private Vector3 respawnPosition;
    private bool look_left = true;

    private void Start()
    {
        respawnPosition = this.transform.position;
        rb.GetComponent<Rigidbody>();
        slingshot.action.Enable();
        slingshot.action.started += Slingshot;
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>(); // -1 -> links, 1 -> rechts
        if (moveDirection==0)
        {
            animator.SetBool("IsWalking",false);
            animator.Play("Idle");
        }
        else if (moveDirection <= 0)
        {
            if (!look_left)
            {
                rb.transform.Rotate(Vector3.up, -180.0f, Space.Self);
                look_left = true;
            }
            animator.SetBool("IsWalking",true);
            animator.Play("walk");
            /*if (move.action.ReadValue<float>() == 0)
            {

            }*/
        }
        else if (moveDirection >= 0)
        {
            if (look_left)
            {
                rb.transform.Rotate(Vector3.up, 180.0f, Space.Self);
                look_left = false;
            }

            animator.SetBool("IsWalking",true);
            animator.Play("walk");
        }

        if (end)
        {
            float step = moveSpeed/3 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endpoint.position, step);
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
        slingshot.action.started -= Slingshot;
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        
        if (availableJumps > 0)
        {
            rb.AddForce(0,jumpPower,0);
            animator.SetBool("isJumping",true);
            animator.Play("jump");
            availableJumps--;
        }
        Debug.Log("Jump"+availableJumps);
    }

    public void Slingshot(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Slingshot");
        if (rope.isAttached && ropeAbility)
        {
            float power = 250;
            rb.AddForce(power*(rope.transform.position-this.transform.position));
            rope.ThrowArm();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Jumps refilled");
            animator.SetBool("isJumping", false);
            animator.Play("Idle");
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

    public void End()
    {
        rb.transform.Rotate(new Vector3(0,1,0),90);
        move.action.Disable();
        jump.action.Disable();
        EndDoor.openDoor();
    }
}
