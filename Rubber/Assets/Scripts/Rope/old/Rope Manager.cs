using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class old_RopeManager : MonoBehaviour
{ 
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private Vector3 distanceBetweenLinks;
    [SerializeField] private int ropeLinkNumber;
    [SerializeField] private Rigidbody fixedPoint;
    [SerializeField] private InputActionReference throwHand;
    [SerializeField] private float throwHandForceAmount;
    
    private List<GameObject> rope = new List<GameObject>();
    private Rigidbody handRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        HingeJoint currentHingeJoint = null;
        for (int i = 0; i < ropeLinkNumber; i++)
        {
            GameObject currentRopeLink = Instantiate(ropePrefab, this.transform);
            currentRopeLink.transform.position += distanceBetweenLinks * i;
            rope.Add(currentRopeLink);
            
            if (i > 0)
            {
                try
                {
                    currentHingeJoint.connectedBody = currentRopeLink.GetComponent<Rigidbody>();
                    currentHingeJoint.enableCollision = false;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                FixedJoint _fixedJoint = currentRopeLink.AddComponent<FixedJoint>();
                _fixedJoint.connectedBody = fixedPoint;
            }
            
            try
            {
                currentHingeJoint = currentRopeLink.GetComponent<HingeJoint>();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        Destroy(currentHingeJoint);
        handRigidbody = rope[rope.Count - 1].GetComponent<Rigidbody>();
        throwHand.action.started += ThrowHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ThrowHand(InputAction.CallbackContext callbackContext)
    {
        
        handRigidbody.AddForce(new Vector3(1,1,0) * throwHandForceAmount, ForceMode.Impulse);
        Debug.Log($"Click Registered. Hand velocity = {handRigidbody.velocity}");
    }
}
