using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parallax effect
/// </summary>
public class Parallax : MonoBehaviour
{
    public GameObject player;
    public float speed;

    float offSetx;
    Material mat;
    PlayerCtrl playerCtrl;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerCtrl = player.GetComponent<PlayerCtrl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // offSetx += 0.005f;

        if(!playerCtrl.isStuck)
        {
            // handles the keyboard and joystick parallax
            offSetx += Input.GetAxisRaw("Horizontal") * speed;
            mat.SetTextureOffset("_MainTex", new Vector2(offSetx, 0));

            // handles the mobile parallax
            if (playerCtrl.leftPressed)
                offSetx += -speed;
            else if (playerCtrl.rightPressed)
                offSetx += speed;
        }
    }
}
