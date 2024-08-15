using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform target;
    private Vector3 originPos;

    private bool open = false;

    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originPos, step);
        }
    }

    public void openDoor()
    {
        open = true;
    }

    public void closeDoor()
    {
        open = false;
    }
}
