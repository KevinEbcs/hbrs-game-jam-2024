using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLinkLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform linkStart;
    [SerializeField] private Transform linkEnd;
    
    
    private LineRenderer _lineRenderer;
    
    
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, linkStart.position);
        _lineRenderer.SetPosition(1, linkEnd.position);
        //_lineRenderer.widthMultiplier = transform.lossyScale.x;
    }
}
