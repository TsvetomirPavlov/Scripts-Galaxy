﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private GameObject _button;

    private void Update()
    {
        //if R key is pressed - restart current scene


#if UNITY_ANDROID
        _button.SetActive(true);
         if (CrossPlatformInputManager.GetButtonDown("Restart") && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene
        }

#elif UNITY_IOS
_button.SetActive(true);
         if (CrossPlatformInputManager.GetButtonDown("Restart") && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene
        }
#else
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene

        }
#endif

    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
