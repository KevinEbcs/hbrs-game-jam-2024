using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputActionReference mousePosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mousePosition.action.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.value;//mousePosition.action.ReadValue<Vector2>();
        
        //Debug.Log(mouseScreenPosition);
        
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 5));
        //Debug.Log(worldPosition);
        
        worldPosition.z = -5;
        transform.position = worldPosition;
    }
}
