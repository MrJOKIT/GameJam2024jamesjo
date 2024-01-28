using System.Collections;
using System.Collections.Generic;
using Jo.scripts;
using UnityEngine;

public class TutorialManagerScene : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMusic("Tutorial");
    }
}
