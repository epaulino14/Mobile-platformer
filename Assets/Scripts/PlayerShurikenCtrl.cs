using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShurikenCtrl : MonoBehaviour
{
    public Vector2 velocity;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);

            Destroy(gameObject);
            GameCtrl.instance.UpdateScore(GameCtrl.Item.Enemy);
        } 
        else if(!other.gameObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);

            Destroy(gameObject);
            GameCtrl.instance.UpdateScore(GameCtrl.Item.Enemy);
        }
    }
}
