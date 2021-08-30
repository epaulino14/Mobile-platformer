using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Activates the Bee when the player is near it.
/// </summary>
public class BeeActivator : MonoBehaviour
{
    public GameObject bee;
    BeeAI beeAI;
    void Start()
    {
        beeAI = bee.GetComponent<BeeAI>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            beeAI.ActivateBee(other.gameObject.transform.position);
        }
    }
}
