using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// the AI engine for the fish
/// </summary>
public class FishAI : MonoBehaviour
{
    public float jumpSpeed;
    SpriteRenderer sr;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        FishJump();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y > 0)
        {
            sr.flipX = false; // face up
        }
        else
        {
            sr.flipX = true; // face down
        }
    }

    public void FishJump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }
}
