using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public InputActionReference pause;
    public InputActionReference mouse;

    private CanvasGroup _pauseCanvas;
    private LevelLoader _levelLoader;

    private Crosshair _cross;
    private PlayerInput _player;
    private RopeAnchor _rope;
    
    private static GameObject _instance;
    // Start is called before the first frame update
    void Start()
    {
        _pauseCanvas = gameObject.GetComponent<CanvasGroup>();
        _levelLoader = LevelLoader.GetInstance();

        if (FindAnyObjectByType<Crosshair>())
        {
            _cross = FindAnyObjectByType<Crosshair>();   
        }
        if (FindAnyObjectByType<PlayerInput>())
        {
            _player = FindAnyObjectByType<PlayerInput>();   
        }
        if (FindAnyObjectByType<RopeAnchor>())
        {
            _rope = FindAnyObjectByType<RopeAnchor>();
        }
    }

    private void OnEnable()
    {
        pause.action.started += Pause;
        mouse.action.started += MousePress;
    }
    
    private void OnDisable()
    {
        pause.action.started -= Pause;
        mouse.action.started -= MousePress;
    }

    private void Pause(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("pause");
        if (_pauseCanvas.blocksRaycasts)
        {
            _pauseCanvas.blocksRaycasts = false;
            _pauseCanvas.alpha = 0.0f;
            _pauseCanvas.interactable = false;

            UnpauseGame();
        }
        else
        {
            _pauseCanvas.blocksRaycasts = true;
            _pauseCanvas.alpha = 1.0f;
            _pauseCanvas.interactable = true;
            
            PauseGame();
        }
    }

    private void MousePress(InputAction.CallbackContext callbackContext)
    {
        
    }
    
    void PauseGame(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (_cross.gameObject)
        {
            Debug.Log("Pause");
            _cross.gameObject.SetActive(false);
        }
        if (_player.gameObject)
        {
            _player.enabled = false;
        }
        if (_rope.gameObject)
        {
            _rope.enabled = false;
        }
    }
    
    void UnpauseGame(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        if (_cross.gameObject)
        {
            Debug.Log("Unpause");
            _cross.gameObject.SetActive(true);
        }
        if (_player.gameObject)
        {
            _player.enabled = true;
        }
        if (_rope.gameObject)
        {
            _rope.enabled = true;
        }
    }

    public void Continue()
    {
        _pauseCanvas.blocksRaycasts = false;
        _pauseCanvas.alpha = 0.0f;
        _pauseCanvas.interactable = false;

        UnpauseGame();
    }

    public void Menu()
    {
        _levelLoader.LoadNextLevel("Main_Menu");
    }
}
