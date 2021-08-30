using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shows the level complete menu
/// </summary>
public class LevelDone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.LevelComplete();
        }
        
    }
}
