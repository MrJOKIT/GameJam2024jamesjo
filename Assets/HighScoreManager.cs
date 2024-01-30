using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private float hightScore;
    private float lastScore;
    public TextAnimator_TMP highScoreText;

    private void Start()
    {
        lastScore = PlayerPrefs.GetFloat("PlayerScore", 0);

        if (lastScore >= hightScore)
        {
            hightScore = lastScore;
        }
        highScoreText.SetText(Convert.ToInt64(hightScore).ToString());
        
    }
}
