using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the enemy behavior when the player jumps onto the enemy
/// </summary>
public class EnemyHeadCtrl : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            AudioCtrl.instance.EnemyHit(enemy.transform.position);

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);
        }
        
    }
}
