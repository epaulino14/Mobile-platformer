using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// make the  player move left/right, jump, flip    
/// </summary>  
public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("this is a positive interget that speeds up the player movement")]
    public int speedBoost;
    public int jumpSpeed;
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public LayerMask whatIsGround;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool SFXOn;
    public bool canFire;
    public bool isJumping;
    public bool isStuck;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    bool canDoubleJump;
    public bool leftPressed, rightPressed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x,feet.position.y), new Vector2(boxWidth,boxHeight), 360.0f, whatIsGround);


        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value 1, -1 or 0
        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            FireBullets();

        ShowFalling();

        if (leftPressed)
            MoveHorizontal(-speedBoost);
        if (rightPressed)
            MoveHorizontal(speedBoost);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);
        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }
    void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        if (playerSpeed < 0)
            sr.flipX = true;
        else if (playerSpeed > 0)
            sr.flipX = false;
        if(!isJumping)
            anim.SetInteger("State", 1);
    }


    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        
        if(!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if(rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }    
    }
    void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed));// player jump on the y axis
            anim.SetInteger("State", 2);

            AudioCtrl.instance.PlayerJump(gameObject.transform.position); // plays the jump sound

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }
        if(canDoubleJump && !isGrounded)
        {
            anim.SetInteger("State", 4);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed));// player jump on the y axis
            
            AudioCtrl.instance.PlayerJump(gameObject.transform.position); // plays the jump sound

            canDoubleJump = false;
        }
    }

    void FireBullets()
    {
       if(canFire)
        {
            // makes the player fire bullet on the left
            if (sr.flipX)
            {
                Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
            }
            // makes the player fire bullet on the right
            if (!sr.flipX)
            {
                Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
            }
            AudioCtrl.instance.FireBullets(gameObject.transform.position);
        }
        
    }
    public void MobileMoveLeft()
    {
        leftPressed = true;
    }
    public void MobileMoveRight()
    {
        rightPressed = true;
    }
    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;
    }
    public void MobileFireBullets()
    {
        FireBullets();
    }
    public void MobileJump()
    {
        Jump();
    }
    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);

            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        } 
        if(other.gameObject.CompareTag("RewardCoin"))
        {
            GameCtrl.instance.UpdateCoinCount();

            SFXCtrl.instance.ShowPowerupSparkle(other.gameObject.transform.position);

            Destroy(other.gameObject);

            GameCtrl.instance.UpdateScore(GameCtrl.Item.RewardCoin);
            AudioCtrl.instance.CoinPickup(gameObject.transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "coin":
                if(SFXOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
                    GameCtrl.instance.UpdateCoinCount();
                    GameCtrl.instance.UpdateScore(GameCtrl.Item.Coin);

                    AudioCtrl.instance.CoinPickup(gameObject.transform.position);
                }
                break;
            case "Water":
                SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);
                AudioCtrl.instance.WaterSplash(gameObject.transform.position);

                break;
                
            case "Powerup_Bullet":
                canFire = true;
                Vector3 powerupPos = other.gameObject.transform.position;
                AudioCtrl.instance.Powerup(gameObject.transform.position);
                Destroy(other.gameObject);
                if(SFXOn)
                    SFXCtrl.instance.ShowPowerupSparkle(powerupPos);
                break;
            case "Enemy":
                GameCtrl.instance.PlayerDiedAnimation(gameObject);

                AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                break;
            case "BossKey":
                GameCtrl.instance.ShowLever();
                break;
            default:
                break;
        }
    }
}
