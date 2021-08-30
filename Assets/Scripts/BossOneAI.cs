using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The AI engine of the level one boss
/// </summary>
public class BossOneAI : MonoBehaviour
{
    public float jumpSpeed;
    public int startJumpingAt;
    public int jumpDelay;
    public int health;
    public Slider bossHealth;
    public GameObject bossBullet;
    public float delayBeforeFiring;
    public Transform bulletSpawnPos;
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    bool canFire, isJumping;

    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        canFire = false;

        

        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire)
        {
            FireBullets();
            canFire = false;
        }
    }

    void Reload()
    {
        canFire = true;
    }
    void FireBullets()
    {
        Instantiate(bossBullet, bulletSpawnPos.position, Quaternion.identity);

        Invoke("Reload", delayBeforeFiring);

        if(health < startJumpingAt && !isJumping)
        {
            InvokeRepeating("Jump", 0, jumpDelay);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            if(health == 0)
            {
                GameCtrl.instance.BulletHitEnemy(gameObject.transform);
                GameCtrl.instance.bossKey.SetActive(true);// activates the bosskey when the boss is defeated
                SFXCtrl.instance.ShowPlayerLanding(GameCtrl.instance.bossKey.transform.position);
            }
            if (health > 0)
            {
                health--;
                bossHealth.value = (float) health;

                sr.color = Color.red;

                Invoke("RestoreColor", 0.1f);
            }
        }
       
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }
}
