using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string _nameScene;

    public string NameScene
    {
        get { return _nameScene; }
        set { _nameScene = value; }
    }
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
