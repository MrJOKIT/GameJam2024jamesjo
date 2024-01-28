using System;
using System.Collections;
using System.Collections.Generic;
using Jo.scripts;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public string name;

    private void Start()
    {
        SoundManager.Instance.PlayMusic("Menu");
    }

    public void ChangeScene()
    {
        SoundManager.Instance.SetupForNewScene();
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
            
    }
}
