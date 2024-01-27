using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private float shootPower = 5;
    public float shootTimeCounter;
    private float shootTimer;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootTimeCounter)
        {
            shootPower = 0;
            transform.gameObject.tag = "Banana";
            _animator.SetBool("Active",true);
        }

        transform.Translate(Vector3.up * shootPower * Time.deltaTime);

        if (shootTimer > 6)
        {
            Destroy(gameObject);
        }
    }

    
}
