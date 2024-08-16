using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticProjectile : MonoBehaviour
{
    private PlayerMovement player;
    public Vector3 direction;
    private Vector3 originPos;

    private bool active = true;

    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originPos = this.transform.position;
        player = FindAnyObjectByType<PlayerMovement>();
        shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        rb.AddForce(direction*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.respawn();
        }
        this.transform.position = originPos;
        rb.velocity=Vector3.zero;
        shoot();

    }
}
