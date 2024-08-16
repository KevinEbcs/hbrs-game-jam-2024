using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManagerLineRenderer : MonoBehaviour
{
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private Vector3 distanceBetweenLinks;
    [SerializeField] private int ropeLinkNumber;
    [SerializeField] private Rigidbody fixedPoint;
    
    private List<GameObject> rope = new List<GameObject>();
    private Rigidbody handRigidbody;

   
    
    // Start is called before the first frame update
    void Start()
    {
        CharacterJoint currentCharacterJoint = null;
        for (int i = 0; i < ropeLinkNumber; i++)
        {
            GameObject currentRopeLink = Instantiate(ropePrefab, this.transform);
            currentRopeLink.transform.position += (i+1) * distanceBetweenLinks;
            //Debug.Log(currentRopeLink.transform.position);
            //currentRopeLink.transform.localScale = transform.localScale;
            //currentRopeLink.transform.localEulerAngles = Vector3.zero;
            rope.Add(currentRopeLink);
            
            if (i > 0)
            {
                try
                {
                    currentCharacterJoint.connectedBody = currentRopeLink.GetComponent<Rigidbody>();
                    currentCharacterJoint.enableCollision = false;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                /*foreach (var characterJoint in GetComponents<CharacterJoint>())
                *{
                *    if (characterJoint.connectedBody == null)
                *        characterJoint.connectedBody = currentRopeLink.GetComponent<Rigidbody>();
                *}
                */

                FixedJoint _fixedJoint = currentRopeLink.AddComponent<FixedJoint>();
                _fixedJoint.connectedBody = fixedPoint;
            }
            
            
            try
            {
                currentCharacterJoint = currentRopeLink.GetComponent<CharacterJoint>();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        Destroy(currentCharacterJoint);
        handRigidbody = rope[rope.Count - 1].GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rope[0].transform.eulerAngles = new Vector3(0, 0, 90);
    }
}
