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
    private bool look_left = true;

    private void Start()
    {
        respawnPosition = this.transform.position;
        rb.GetComponent<Rigidbody>();
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
            //Camera.main.transform.Rotate(Vector3.right, -180.0f, Space.Self);
            
            /*if (rotating)
            {
                if (Vector3.Distance(transform.eulerAngles, new Vector3(180.0f, 0, 0)) > 0.1f)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(180.0f, 0, 0),
                        Time.deltaTime);
                }
                else
                {
                    transform.eulerAngles = new Vector3(180.0f, 0, 0);
                    rotating = false;
                }
            }*/

            animator.SetBool("IsWalking",true);
            animator.Play("walk");
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
