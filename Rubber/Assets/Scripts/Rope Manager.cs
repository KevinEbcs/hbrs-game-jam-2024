using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{ 
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private float distanceBetweenLinks;
    [SerializeField] private int ropeLinkNumber;
    [SerializeField] private Rigidbody fixedPoint;
    
    private List<GameObject> rope = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        HingeJoint currentHingeJoint = null;
        for (int i = 0; i < ropeLinkNumber; i++)
        {
            GameObject currentRopeLink = Instantiate(ropePrefab, this.transform);
            currentRopeLink.transform.position += new Vector3(0, -(0.11f * i), 0);
            
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
