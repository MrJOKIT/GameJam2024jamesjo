using System;
using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUp;
    private int index;
    private bool canClick = false;
    public GameObject ready;

    

    void Update()
    {
        if (index == 3)
        {
            ready.gameObject.SetActive(true);
        }
    }
    public void Onclick()
    {
        if (canClick == false && index < 3)
        {
            index++;
            for (int i = 0; i < popUp.Length; i++)
            {
                if (i == index && canClick == false)
                {
                    popUp[i].SetActive(true);
                }
            }
            StartCoroutine(StartTimer());
        }
    } 
    IEnumerator StartTimer(int timeRemaining = 2)
    {
        for (int i = timeRemaining; i > 0; i--)
        {
            canClick = true;
            yield return new WaitForSeconds(1);
        }
        canClick = false;
    }
}