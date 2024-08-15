using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
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
            player.maxJumps++;
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.Play();
            this.gameObject.SetActive(false);
        }
    }
}
