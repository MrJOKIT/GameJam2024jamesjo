using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScene : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayMusic("Tutorial");
    }
}
