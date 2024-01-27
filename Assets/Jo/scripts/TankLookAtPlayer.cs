using UnityEngine;

public class TankLookAtPlayer : MonoBehaviour
{
    public Transform player;
    public Transform turret;
    private float moveSpeed = 3f;
    private float shootingCooldown = 3f;
    private float lastShotTime;

    public GameObject bulletPrefab;
    public GameObject firePoint;

    void Start()
    {
        lastShotTime = Time.time;
    }

    void Update()
    {
        if (player != null)
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
        SoundManager.instance.PlaySfx("Explosion");
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }
}