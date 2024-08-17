using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
       player = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.keyFound = true;
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.Play();
            this.gameObject.SetActive(false);
        }
    }
}
