using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class RotateLever : MonoBehaviour
{
    public Door door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            door.openDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            door.closeDoor();
        }
    }
}
