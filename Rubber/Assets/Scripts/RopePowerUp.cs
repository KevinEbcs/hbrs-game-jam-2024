using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class RopePowerUp : MonoBehaviour
{
    private RopeAnchor rope;

    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
       rope = FindAnyObjectByType<RopeAnchor>();
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
            rope.ropeAbility = true;
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.Play();
            this.gameObject.SetActive(false);
        }
    }
}
