using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.right * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Goal"))
        {
            //into game over
            Destroy(gameObject);
        }
    }
}
