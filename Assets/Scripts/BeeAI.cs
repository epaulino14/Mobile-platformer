using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// AI engine for the bee
/// </summary>
public class BeeAI : MonoBehaviour
{
    public float destroyBeeDelay;
    public float beeSpeed;

    public void ActivateBee(Vector3 playerPos)
    {
        transform.DOMove(playerPos, beeSpeed, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);

            Destroy(gameObject);
        }
              
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
