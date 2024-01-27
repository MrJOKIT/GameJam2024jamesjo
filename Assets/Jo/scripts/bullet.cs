using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void Update()
    {
       
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.instance.HpDecrease();
            ProCamera2DShake.Instance.Shake(1);
            Destroy(gameObject);
        }
    }
    
}