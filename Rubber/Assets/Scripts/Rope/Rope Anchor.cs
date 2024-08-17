using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class RopeAnchor : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private LineRenderer ropeVisual;
    
    [SerializeField] private LayerMask isPlayer;
    //[SerializeField] private LayerMask isGrabable;

    [SerializeField] private InputActionReference throwArm;

    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private float flightTime;
    [SerializeField] private float grabCooldown = 1;
    [SerializeField] private float throwForceAmount;

    private Transform playerTransform;
    private Rigidbody _rigidbody;

    private bool isInFlight = false;
    private float currentFlightTimer;
    private float currentGrabCooldownTimer;
    private bool isGrabCooldown;
    private bool isAttached;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerRigidbody.transform;
        throwArm.action.Enable();
        throwArm.action.performed += ThrowArm;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetRope();
        ManageFlight();
        ManageGrabCooldown();
        
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ThrowArm();
        }
        
        
    }

    private void ResetRope()
    {
        if (!isAttached && Physics.Raycast(playerTransform.position, transform.position-playerTransform.position, 
                (transform.position - playerTransform.position).magnitude, ~isPlayer))
        {
            transform.position = playerTransform.position;
            _rigidbody.velocity = Vector3.zero;
        }
        ropeVisual.SetPosition(0, playerTransform.position);
        ropeVisual.SetPosition(1, transform.position);
    }

    private void ManageFlight()
    {
        if (isInFlight)
        {
            currentFlightTimer += Time.deltaTime;
            if (currentFlightTimer >= flightTime)
            {
                isInFlight = false;
            }
        }
    }

    private void ManageGrabCooldown()
    {
        if (isGrabCooldown)
        {
            currentGrabCooldownTimer += Time.deltaTime;
            if (currentGrabCooldownTimer >= grabCooldown)
            {
                isGrabCooldown = false;
            }
        }
    }

    void ThrowArm(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("CLick");
        ThrowArm();
    }

    void ThrowArm()
    {
        if (isAttached)
        {
            _rigidbody.isKinematic = false;
            isAttached = false;
            isGrabCooldown = true;
            currentGrabCooldownTimer = 0;
            return;
        }

        if (!isGrabCooldown)
        {
            Vector3 throwDirection = (_crosshair.transform.position - transform.position).normalized;
            _rigidbody.AddForce(throwForceAmount * throwDirection);
            isInFlight = true;
            currentFlightTimer = 0;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(gameObject.layer);
        //Debug.Log(collision.gameObject.tag);
        if (isInFlight && collision.gameObject.layer == 18)
        {
            Debug.Log("Attach");
            _rigidbody.isKinematic = true;
            isAttached = true;
            isInFlight = false;
        }
    }
}
