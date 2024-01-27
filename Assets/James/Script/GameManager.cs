using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject tank;
    public TextAnimator_TMP scoreText;
    public int hp;
    public Image[] hpImage;
    public float score;
    public bool onTankSpawn;
    public int tankLevel = 1;
    public Transform spawnTankPos;
    public float scoreToSpawnTank;
    public bool isGameOver;
    
    [Header("Canvas")]
    public GameObject gameOverCanvas;
    public GameObject playerCanvas;
    public Volume volume;

    [Header("Menu")] 
    private GameObject menuCanvas;
    private bool onMenu;

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

        if (hp <= 0 && !isGameOver)
        {
            SoundManager.instance.PlaySfx("GAMEOVER");
            StartCoroutine(GameOver());
            isGameOver = true;
        }

        if (isGameOver)
        {
            SoundManager.instance.StopMusic();
        }
        
    }

    public void HpDecrease()
    {
        if (hp > 0)
        {
            hp -= 1;
        }
    }

    IEnumerator GameOver()
    {
        playerCanvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        gameOverCanvas.SetActive(true);
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

    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onMenu)
        {
            menuCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && onMenu)
        {
            menuCanvas.SetActive(false);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
