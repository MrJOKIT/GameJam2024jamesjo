using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private float shootPower = 5;
    public float shootTimeCounter;
    private float shootTimer;

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootTimeCounter)
        {
            shootPower = 0;
        }

        transform.Translate(Vector3.up * shootPower * Time.deltaTime);

        if (shootTimer > 10)
        {
            Destroy(gameObject);
        }
    }
}
