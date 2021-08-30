using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;
   
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        //transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);  
         
    }
}
