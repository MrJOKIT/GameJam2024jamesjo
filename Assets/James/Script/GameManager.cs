using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using Jo.scripts;
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
    public GameObject menuCanvas;
    //public GameObject optionCanvas;
    public bool onMenu;
    public GameObject pauseIcon, resumeIcon;

    [Header("Ability")] 
    public GameObject abilityCanvas;
    public bool onAbility;
    public Animator[] animatorsCard;
    

    private void Awake()
    {
        instance = this;
        hp = hpImage.Length;
        scoreText.SetText($"<bounce a=0.2>{Convert.ToInt64(score).ToString()}");
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic("BG");
        
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
        Menu();

        if (hp <= 0 && !isGameOver)
        {
            SoundManager.Instance.PlaySfx("GAMEOVER");
            StartCoroutine(GameOver());
            isGameOver = true;
        }

        if (isGameOver)
        {
            SoundManager.Instance.StopMusic();
        }
        
    }

    public void HpDecrease()
    {
        if (hp > 0 && !onAbility)
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
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && onMenu)
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        onMenu = false;
        //optionCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        pauseIcon.SetActive(true);
        resumeIcon.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        menuCanvas.SetActive(true);
        pauseIcon.SetActive(false);
        resumeIcon.SetActive(true);
        onMenu = true;
        Time.timeScale = 0f;
    }

    public void LoadSceneAsync(string sceneName)
    {
        Time.timeScale = 1f;
        SoundManager.Instance.SetupForNewScene();
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ShowReward()
    {
        onAbility = true;
        playerCanvas.SetActive(false);
        abilityCanvas.SetActive(true);
        StartCoroutine(ShowCard());
    }

    public void SelectRewardUpPower()
    {
        SoundManager.Instance.PlaySfx("CLICK");
        Banana.damage += 2.5f;
        DissapearCard();
        abilityCanvas.SetActive(false);
        playerCanvas.SetActive(true);
        onTankSpawn = false;
        onAbility = false;
    }
    
    public void SelectRewardUpSpeed()
    {
        SoundManager.Instance.PlaySfx("CLICK");
        PlayerController.instance.movementSpeed += 0.3f;
        DissapearCard();
        abilityCanvas.SetActive(false);
        playerCanvas.SetActive(true);
        onTankSpawn = false;
        onAbility = false;
    }
    
    public void SelectRewardUpThrow()
    {
        SoundManager.Instance.PlaySfx("CLICK");
        PlayerController.instance.chargeThrowSpeed += 0.1f;
        DissapearCard();
        abilityCanvas.SetActive(false);
        playerCanvas.SetActive(true);
        onTankSpawn = false;
        onAbility = false;
    }

    IEnumerator ShowCard()
    {
        animatorsCard[0].SetBool("Show",true);
        SoundManager.Instance.PlaySfx("DROP");
        yield return new WaitForSeconds(0.5f);
        animatorsCard[1].SetBool("Show",true);
        SoundManager.Instance.PlaySfx("DROP");
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.PlaySfx("DROP");
        animatorsCard[2].SetBool("Show",true);
    }

    private void DissapearCard()
    {
        animatorsCard[0].SetBool("Show",false);
        animatorsCard[1].SetBool("Show",false);
        animatorsCard[2].SetBool("Show",false);
    }

    
}
