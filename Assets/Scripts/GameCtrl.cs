using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using DG.Tweening;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    [HideInInspector]
    public GameData data;
    
    [Header("Texts")]
    public Text txtCoinCount;
    public Text txtScore;
    public Text txtTimer;
    
    [Header("Varible values")]
    public int coinValue;
    public int rewardCoinValue;
    public int enemyValue;
    public float maxTime;
    public float restartDelay;
    public enum Item
    {
        Coin,
        RewardCoin,
        Enemy
    }

    [Header("Images/Sprites")]
    public Image key0;
    public Image key1;
    public Image key2; 
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;
    public Image heart1;
    public Image heart2;
    public Image heart3; 
    public Sprite emptyHeart;
    public Sprite fullHeart;

    [Header("Game Objects")]
    public GameObject panelGameOver;
    public GameObject rewardCoin;
    public GameObject player;
    public GameObject lever;
    public GameObject enemySpawner;
    public GameObject bossKey;
    public GameObject unlockablePlatform;
    public GameObject levelCompleteMune;
    public GameObject pauseMenu;
    public GameObject mobileUI;
    public Slider bossHealth;
    public GameObject pauseBtn;

    string dataFilePath;
    BinaryFormatter bf;
    float timeLeft;
    bool timerOn;
    bool isPaused;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }
    void Start()
    {
        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        RefreshUI();

        timeLeft = maxTime;
        timerOn = true;

        isPaused = false;

        HandleFirstBoot();
        UpdateHearts();
        bossHealth.gameObject.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (timeLeft > 0 && timerOn)
            UpdateTimer();
    }
   
    public void RefreshUI()
    {
        txtCoinCount.text = " X " + data.coinCount;
        txtScore.text = "Score: " + data.score;
    }

    private void OnEnable()
    {
        Debug.Log("Data Loaded");
        RefreshUI();
    }
    private void OnDisable()
    {
        Debug.Log("Data Saved");
        DataCtrl.instance.SaveData(data);

        Time.timeScale = 1;

        AdsCtrl.instance.HideBanner();
    }


    public void SetStarAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;
    }

    /// <summary>
    /// unlocks the specified level.
    /// </summary>
    /// <param name="levelNumber"></param>
    public void UnlockNextLevel(int levelNumber)
    {
        data.levelData[levelNumber].isUnlocked = true;
    }
    /// <summary>
    /// get the current score for the level complete menu
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return data.score;
    }
    /// <summary>
    /// restarts the level when players dies
    /// </summary>
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // push the player back
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        //rotates the player
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // disable the colliders so the player can fall through the ground
        foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }
        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        Camera.main.GetComponent<CameraCtrl>().enabled = false;

        rb.velocity = Vector2.zero;

        StartCoroutine("PauseBeforeReload", player);

    }

    public void PlayerStompsEnemy(GameObject enemy)
    {
        enemy.tag = "Untagged";

        Destroy(enemy);

        UpdateScore(Item.Enemy);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        PlayerDied(player);
    }
    public void PlayerDrowned(GameObject player)
    {
        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }

    public void UpdateCoinCount()
    {
        data.coinCount += 1;
        txtCoinCount.text = " X " + data.coinCount;

        
    }
    public void UpdateScore(Item item)
    {
        int itemValue = 0;
        switch (item)
        {
            case Item.Coin:
                itemValue = coinValue;
                break;
            case Item.RewardCoin:
                itemValue = rewardCoinValue;
                break;
            case Item.Enemy:
                itemValue = enemyValue;
                break;
            default:
                break;
        }
        data.score += itemValue;

        txtScore.text = "Score: " + data.score;   
    }
    /// <summary>
    /// called when bullets hit the enemy
    /// </summary>
    /// <param name="enemy"></param>
    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        Instantiate(rewardCoin, pos, Quaternion.identity);

        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);
    }
    public void UpdateKeyCount(int keyNumber)
    {
        data.keyFound[keyNumber] = true;

        if (keyNumber == 0)
            key0.sprite = key0Full;
        else if (keyNumber == 1)
            key1.sprite = key1Full;
        else if (keyNumber == 2)
            key2.sprite = key2Full;
        if (data.keyFound[0] && data.keyFound[1])
            ShowUnlockablePlatform();

    }

    public void ShowUnlockablePlatform()
    {
        unlockablePlatform.SetActive(true);

        SFXCtrl.instance.ShowPlayerLanding(unlockablePlatform.transform.position);

        timerOn = false;

        bossHealth.gameObject.SetActive(true);
    }

    public void LevelComplete()
    {
        if (timerOn)
            timerOn = false;
        levelCompleteMune.SetActive(true);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        txtTimer.text = "Timer: " + (int)timeLeft;

        if(timeLeft <= 0)
        {
            txtTimer.text = "Timer: 0";

            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            PlayerDied(player);
        }
    }
    private void HandleFirstBoot()
    {
        if(data.isFirstBoot)
        {
            data.lives = 3;

            data.coinCount = 0;


            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;

            data.score = 0;

            data.isFirstBoot = false;
        }
    }
    private void UpdateHearts()
    {
        if(data.lives == 3)
        {
            heart1.sprite = fullHeart;
            heart2.sprite = fullHeart;
            heart3.sprite = fullHeart;
        }
        if(data.lives == 2)
        {
            heart1.sprite = emptyHeart;
        }if(data.lives == 1 )
        {
            heart1.sprite = emptyHeart;
            heart2.sprite = emptyHeart;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives -= 1;
        data.lives = updatedLives;

        if(data.lives == 0)
        {
            data.lives = 3;
            DataCtrl.instance.SaveData(data);
            Invoke("GameOver", restartDelay);
        }
        else
        {
            DataCtrl.instance.SaveData(data); ;
            Invoke("RestartLevel", restartDelay);
        }
    }
    public void StopCameraFollow()
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
        player.GetComponent<PlayerCtrl>().isStuck = true;
        player.transform.Find("Left_check").gameObject.SetActive(false);
        player.transform.Find("Right_check").gameObject.SetActive(false);
    }
     public void ShowLever()
    {
        lever.SetActive(true);
        
        enemySpawner.SetActive(false); // deactives the enemyspawner.

        SFXCtrl.instance.ShowPlayerLanding(lever.gameObject.transform.position);

        AudioCtrl.instance.EnemyExplosion(lever.gameObject.transform.position);
    }

    public void ActivateEnemySpawner()
    {
        enemySpawner.SetActive(true);
    }
    void GameOver()
    {
        //panelGameOver.SetActive(true);
        if (timerOn)
            timerOn = false;
        panelGameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, 0, false);
    }

    public void ShowPauseMenu()
    {
        if (mobileUI.activeInHierarchy)
            mobileUI.SetActive(false);
        pauseMenu.SetActive(true);

        pauseMenu.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, 0, false);

        AdsCtrl.instance.ShowBanner();

        Invoke("SetPause", 1.1f);
        pauseBtn.SetActive(false);
    }

    void SetPause()
    {
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        isPaused = false;

        if (!mobileUI.activeInHierarchy)
            mobileUI.SetActive(true);
        //pauseMenu.SetActive(false);

        pauseMenu.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600f, 0.7f, 0, false);

        AdsCtrl.instance.HideBanner();

        pauseBtn.SetActive(true);
    }

}
