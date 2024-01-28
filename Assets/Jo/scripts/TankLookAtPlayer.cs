using System;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using Jo.scripts;


public class TankLookAtPlayer : MonoBehaviour
{
    public GameObject tank;
    public Transform player;
    public Transform turret;
    private float moveSpeed = 3f;
    private float shootingCooldown = 3f;
    private float lastShotTime;
    private int hpTank = 15;

    public GameObject explode;

    public GameObject bulletPrefab;
    public GameObject firePoint;

    private bool onDied;

    void Start()
    {
        lastShotTime = Time.time;
        float cooldown = Convert.ToSingle(GameManager.instance.tankLevel) / 10;
        Debug.Log(cooldown);
        shootingCooldown -= cooldown;
        hpTank *= GameManager.instance.tankLevel;
    }

    void Update()
    {
        if (player != null && !onDied && GameManager.instance.isGameOver == false)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += 90f;
            turret.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            MoveToPosition(new Vector3(0, 4.6f, 0));

            if (Time.time - lastShotTime >= shootingCooldown)
            {
                
                if (transform.position == new Vector3(0, 4.6f, 0))
                {
                    Shoot();
                    
                    lastShotTime = Time.time;
                }
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        
        if (transform.position == targetPosition)
        {
            moveSpeed = 0f;
        }
    }
    void Shoot()
    {
        SoundManager.Instance.PlaySfx("Explosion");
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Banana"))
        {
            hpTank -= 5;
            //GameManager.instance.HpDecrease();
            ProCamera2DShake.Instance.Shake(1);
            Destroy(col.gameObject);
            if (hpTank <= 0)
            {
                StartCoroutine(TankDestroy());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Banana"))
        {
            hpTank -= 5;
            //GameManager.instance.HpDecrease();
            ProCamera2DShake.Instance.Shake(1);
            Destroy(other.gameObject);
            if (hpTank <= 0)
            {
                StartCoroutine(TankDestroy());
            }
        }
    }

    IEnumerator TankDestroy()
    {
        onDied = true;
        explode.gameObject.SetActive(true);
        ProCamera2DShake.Instance.Shake(0);
        SoundManager.Instance.PlaySfx("TankDestroy");
        WorkerSpawnerManager.instance.AddWorker( (GameManager.instance.tankLevel * 20));
        WorkerSpawnerManager.instance.SetUpSpawner();
        GameManager.instance.ShowReward();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}