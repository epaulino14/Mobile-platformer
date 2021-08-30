using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCtrl : MonoBehaviour
{
    public int keyNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.UpdateKeyCount(keyNumber);

            AudioCtrl.instance.KeyFound(other.gameObject.transform.position);

            SFXCtrl.instance.ShowKeySparkle(keyNumber);

            Destroy(gameObject);
        }
    }
}
