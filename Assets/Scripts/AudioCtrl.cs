using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Handles the audio in the game
/// </summary>
public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl instance;
    public PlayerAudio playerAudio;
    public AudioFX AudioFX;
    public Transform player;
    public GameObject bgMusic;
    public GameObject btnSound, btnMusic;
    public bool bgMusicOn;
    public Sprite imgSoundOn, imgSoundOff;
    public Sprite imgMusicOn, imgMusicOff;

    [Tooltip("soundOn is use to switch the sound on/off fromt the inspector")]
    public bool soundOn;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        if (DataCtrl.instance.data.playMusic)
        {
            bgMusic.SetActive(true);

            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        }
        else
        {
            bgMusic.SetActive(false);

            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
        }
        if (DataCtrl.instance.data.playSound)
        {
            soundOn = true;

            btnSound.GetComponent<Image>().sprite = imgSoundOn;
        }
        else
        {
            soundOn = false;

            btnSound.GetComponent<Image>().sprite = imgSoundOff;
        }

    }

   public void PlayerJump(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerJump, playerPos);
        }
    }
    public void CoinPickup(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }
    public void FireBullets(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.fireBullets, playerPos);
        }
    }
    public void EnemyExplosion(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyExplosion, playerPos);
        }
    }
    public void BreakableCrates(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.breakCrate, playerPos);
        }
    }
    public void WaterSplash(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.waterSflash, playerPos);
        }
    }
    public void Powerup(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.powerUp, playerPos);
        }
    }
    public void KeyFound(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.keyFound, playerPos);
        }
    }
    public void EnemyHit(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyHit, playerPos);
        }
    }
    public void PlayerDied(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }

    public void ToggleSound()
    {
        if (DataCtrl.instance.data.playSound)
        {
            soundOn = false;

            btnSound.GetComponent<Image>().sprite = imgSoundOff;

            DataCtrl.instance.data.playSound = false;
        }
        else
        {
            soundOn = true;

            btnSound.GetComponent<Image>().sprite = imgSoundOn;

            DataCtrl.instance.data.playSound = true;
        }
    }

    public void ToggleMusic()
    {
        if(DataCtrl.instance.data.playMusic)
        {
            bgMusic.SetActive(false);

            btnMusic.GetComponent<Image>().sprite = imgMusicOff;

            DataCtrl.instance.data.playMusic = false;
        }
        else
        {
            bgMusic.SetActive(true);

            btnMusic.GetComponent<Image>().sprite = imgMusicOn;

            DataCtrl.instance.data.playMusic = true;
        }
    }
}
