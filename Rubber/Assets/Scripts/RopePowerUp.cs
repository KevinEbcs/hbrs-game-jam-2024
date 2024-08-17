using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePowerUp : MonoBehaviour
{
    private RopeAnchor rope;

    private PlayerMovement player;

    private TextHandler text;
    // Start is called before the first frame update
    void Start()
    {
       rope = FindAnyObjectByType<RopeAnchor>();
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
            player.ropeAbility = true;
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.Play();
            text.ropeMessage();
            this.gameObject.SetActive(false);
        }
    }
}
