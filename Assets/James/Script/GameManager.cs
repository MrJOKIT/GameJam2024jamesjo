using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject tank;
    public TextAnimator_TMP scoreText;
    public int hp;
    public Image[] hpImage;
    public GameObject gameOverCanvas;
    public float score;
    public bool onTankSpawn;
    public int tankLevel = 1;
    public Transform spawnTankPos;
    public float scoreToSpawnTank;

    private void Awake()
    {
        instance = this;
        hp = hpImage.Length;
        scoreText.SetText($"<bounce a=0.2>{Convert.ToInt64(score).ToString()}");
    }

    private void Start()
    {
        SoundManager.instance.PlayMusic("BG");
    }

    private void Update()
    {
        if (score > scoreToSpawnTank && !onTankSpawn)
        {
            Instantiate(tank, spawnTankPos.position, spawnTankPos.rotation);
            scoreToSpawnTank *= 2;
            tankLevel += 1;
            onTankSpawn = true;
        }
        UpdateHp();
        
    }

    public void HpDecrease()
    {
        if (hp > 0)
        {
            hp -= 1;
        }
    }

    private void UpdateHp()
    {
        switch (hp)
        {
            case 3: hpImage[0].enabled = true; 
                    hpImage[1].enabled = true;
                    hpImage[2].enabled = true;
                    break;
            case 2: hpImage[0].enabled = true; 
                    hpImage[1].enabled = true;
                    hpImage[2].enabled = false;
                    break;
            case 1: hpImage[0].enabled = true; 
                    hpImage[1].enabled = false;
                    hpImage[2].enabled = false;
                    break;
            default:hpImage[0].enabled = false; 
                    hpImage[1].enabled = false;
                    hpImage[2].enabled = false;
                    break;
        }
    }

    public void AddScore(float scorePoint)
    {
        score += scorePoint;
        scoreText.SetText($"<bounce a=0.2>{Convert.ToInt64(score).ToString()}");
    }
}
