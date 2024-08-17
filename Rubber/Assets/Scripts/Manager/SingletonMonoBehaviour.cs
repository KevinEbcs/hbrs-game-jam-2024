using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
    private static float _oldestTimeOfCreation;
    private float _timeOfCreation;
    private static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindAnyObjectByType<T>();
            if (_instance == null)
            {
                throw new Exception($"No instance of {typeof(T).Name} found");
            }
        }
        return _instance;
    }

    public void Awake()
    {
        _timeOfCreation = Time.fixedTime;
        SceneManager.sceneLoaded += SceneLoaded;
        _oldestTimeOfCreation = _timeOfCreation;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if ((_instance != null && _instance != this) || FindObjectsByType<T>(FindObjectsSortMode.None).Length > 1)
        {
            if (_timeOfCreation > _oldestTimeOfCreation)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
