using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;
    public GameObject sfx_coin_pickup; //the particle effect to show when the player picks coins
    public GameObject sfx_powerup_pickup; //the particle effect to show when the player picks bullet powerup
    public GameObject sfx_playerLands; //the particle effect to show when the player lands
    public GameObject sfx_fragment_1;
    public GameObject sfx_fragment_2;
    public GameObject sfx_splash;
    public GameObject sfx_explosion;
    public Transform key0, key1, key2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// shows the bullets sparkles when player collects the coins
    /// </summary>
    public void ShowCoinSparkle(Vector3 pos)
    {
        Instantiate(sfx_coin_pickup, pos, Quaternion.identity);
    }  
    public void EnemyExplosion(Vector3 pos)
    {
        Instantiate(sfx_explosion, pos, Quaternion.identity);
    } 
    public void ShowKeySparkle(int keyNumber)
    {
        Vector3 pos = Vector3.zero;
        if (keyNumber == 0)
            pos = key0.position;
        else if (keyNumber == 1)
            pos = key1.position;
        else if (keyNumber == 2)
            pos = key2.position;
        Instantiate(sfx_powerup_pickup, pos, Quaternion.identity);
    }
    public void ShowPowerupSparkle(Vector3 pos)
    {
        Instantiate(sfx_powerup_pickup, pos, Quaternion.identity);
    } 
    public void ShowPlayerLanding(Vector3 pos)
    {
        Instantiate(sfx_playerLands, pos, Quaternion.identity);
    } 
    public void ShowEnemyPoof(Vector3 pos)
    {
        Instantiate(sfx_playerLands, pos, Quaternion.identity);
    } 
    public void ShowSplash(Vector3 pos)
    {
        Instantiate(sfx_splash, pos, Quaternion.identity);
    }
    public void HandleBoxBreaking(Vector3 pos)
    {
        Vector3 pos1 = pos;
        pos1.x -= 0.3f;

        Vector3 pos2 = pos;
        pos2.x += 0.3f;

        Vector3 pos3 = pos;
        pos3.x -= 0.3f;
        pos3.y -= 0.3f;

        Vector3 pos4 = pos;
        pos4.x += 0.3f;
        pos4.y += 0.3f;

        Instantiate(sfx_fragment_1, pos1, Quaternion.identity);
        Instantiate(sfx_fragment_2, pos2, Quaternion.identity);
        Instantiate(sfx_fragment_2, pos3, Quaternion.identity);
        Instantiate(sfx_fragment_1, pos4, Quaternion.identity);

        AudioCtrl.instance.BreakableCrates(pos);
    }
}
