using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int hp;
    public Image[] hpImage;

    private void Awake()
    {
        instance = this;
        hp = hpImage.Length;
    }

    private void Update()
    {
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
}
