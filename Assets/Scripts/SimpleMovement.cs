using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the enemy move in a straight line
/// it also checks for collisions to change the enemy's direction.
/// </summary>
public class SimpleMovement : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetStartingDirection()
    {
        if(speed > 0)
        {
            sr.flipX = true;
        }
        else if(speed < 0)
        {
            sr.flipX = false;
        }
    }
    void FlipOnCollision()
    {
        speed = -speed;
        SetStartingDirection();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            FlipOnCollision();
        }
    }
}
