using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCtrl : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.StopCameraFollow();

            GameCtrl.instance.ActivateEnemySpawner();
        }
    }
}
