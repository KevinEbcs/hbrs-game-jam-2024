using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private PlayerMovement player;

    private TextHandler text;
    // Start is called before the first frame update
    void Start()
    {
       player = FindAnyObjectByType<PlayerMovement>();
       text = FindAnyObjectByType<TextHandler>();
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
            text.jumpMessage();
            this.gameObject.SetActive(false);
        }
    }
}
