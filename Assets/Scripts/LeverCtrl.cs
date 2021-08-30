using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Releases the friends from the cage
/// </summary>
public class LeverCtrl : MonoBehaviour
{
    public GameObject friend;

    public Vector2 jumpSpeed;

    public GameObject[] stairs;

    public Sprite leverPulled;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = friend.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(jumpSpeed);

            foreach(GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }
            SFXCtrl.instance.ShowPlayerLanding(gameObject.transform.position);
            sr.sprite = leverPulled;

            Invoke("ShowLevelCompleteMenu", 3f);
        }


    }

    void ShowLevelCompleteMenu()
    {
        GameCtrl.instance.LevelComplete();
    }
}