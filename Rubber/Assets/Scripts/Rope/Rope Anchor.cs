using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private LineRenderer ropeVisual;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private LayerMask isPlayer;

    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerRigidbody.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(playerTransform.position, transform.position-playerTransform.position, 
                (transform.position - playerTransform.position).magnitude, ~isPlayer))
        {
            transform.position = playerTransform.position;
        }
        
        ropeVisual.SetPosition(0, playerTransform.position);
        ropeVisual.SetPosition(1, transform.position);
    }
}
