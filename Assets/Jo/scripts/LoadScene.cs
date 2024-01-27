using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public string name;

    private void Start()
    {
        SoundManager.instance.PlayMusic("Menu");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
            
    }
}
