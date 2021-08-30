﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly

    }
    public CoinFX coinFX;
    public float speed;
    public bool startFlying;

    GameObject coinMeter;

    private void Start()
    {
        startFlying = false;

        if(coinFX == CoinFX.Fly)
        {
            coinMeter = GameObject.Find("img_Coin_Count");
        }
    }

    private void Update()
    {
        if(startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (coinFX == CoinFX.Vanish)
                Destroy(gameObject); // destroys the coins
            else if (coinFX == CoinFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }
        }
    }
}
