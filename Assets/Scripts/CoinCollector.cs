using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives the coins which fly toward it when the player pick them
/// </summary>
public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
