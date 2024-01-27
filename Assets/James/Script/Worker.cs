using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DamageNumbersPro;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private float workerSpeed;
    private bool onWorkerInLine;
    public Transform checkPoint;
    public LayerMask checkLayer;
    private float workerPoint = 1000f;
    [SerializeField] private float scoreMultiper;
    [SerializeField] private DamageNumber scoreNoti;
    
    
    private void Update()
    {
        onWorkerInLine = Physics2D.Raycast(checkPoint.position, Vector2.right, 2.5f,checkLayer);

        if (!onWorkerInLine)
        {
            transform.Translate(Vector3.right * workerSpeed * Time.deltaTime);
        }

        workerPoint -= Time.deltaTime * scoreMultiper;


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Goal"))
        {
            //into game over
            GameManager.instance.HpDecrease();
            Destroy(gameObject);
        }
        else if (col.CompareTag("Banana"))
        {
            //Play Animate Died
            ProCamera2DShake.Instance.Shake(0);
            PlayerController.instance.Haha();
            DamageNumber damageNumber = scoreNoti.Spawn(transform.position, workerPoint);
            GameManager.instance.AddScore(workerPoint);
            Destroy(col.gameObject);
            Destroy(gameObject);
            
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(checkPoint.position, Vector3.right * 2.5f);
    }
}
