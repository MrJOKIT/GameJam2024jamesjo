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
    private bool onDie;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;
    public Transform checkPoint;
    public LayerMask checkLayer;
    private float workerPoint = 1000f;
    [SerializeField] private float scoreMultiper;
    [SerializeField] private DamageNumber scoreNoti;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        onWorkerInLine = Physics2D.Raycast(checkPoint.position, Vector2.right, 2.5f,checkLayer);

        if (!onWorkerInLine && !onDie)
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
            Destroy(col.gameObject);
            StartCoroutine(WorkerDied());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Banana"))
        {
            Destroy(other.gameObject);
            StartCoroutine(WorkerDied());
        }
    }

    IEnumerator WorkerDied()
    {
        PlayerController.instance.Haha();
        onDie = true;
        _capsuleCollider2D.enabled = false;
        _animator.SetBool("Dead",true);
        ProCamera2DShake.Instance.Shake(0);
        DamageNumber damageNumber = scoreNoti.Spawn(transform.position, workerPoint);
        GameManager.instance.AddScore(workerPoint);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(checkPoint.position, Vector3.right * 2.5f);
    }
}
