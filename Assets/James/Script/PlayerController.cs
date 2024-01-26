using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Image chargeImage;
    public Transform throwPoint;
    public Transform bananaPrefab;

    [Header("ThrowSetting")] 
    private float throwTimeCounter = 1.5f;
    private float throwTime;
    

    private void Update()
    {
        PlayerMovement();
        PlayerLookAt();
        PlayerThrow();
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, y);

        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    private void PlayerLookAt()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        Vector2 mouseDirection =
            new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = mouseDirection;
    }

    private void PlayerThrow()
    {
        if (Input.GetMouseButton(0))
        {
            throwTime += Time.deltaTime;
            if (throwTime >= throwTimeCounter)
            {
                throwTime = throwTimeCounter;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (throwTime > 0.25f)
            {
                ThrowBanana(throwTime * throwTimeCounter / 2);
                throwTime = 0;
                //throw banana
            }
            else
            {
                throwTime = 0;
            }
        }

        float chargeCount = throwTime / throwTimeCounter;
        chargeImage.fillAmount = chargeCount;
    }

    private void ThrowBanana(float power)
    {
         Transform banana = Instantiate(bananaPrefab,throwPoint.position,throwPoint.rotation);
         banana.GetComponent<Banana>().shootTimeCounter = power;
    }
}
