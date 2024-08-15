using System.Collections;
using System.Collections.Generic;
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
        if (this.transform.position.x > -0.003)
        {
            door.openDoor();
        }
        else
        {
            door.closeDoor();
        }
    }
}
