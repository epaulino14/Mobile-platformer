﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Locks/Unlocks a level button and shows number of stars for an unlock button.
/// </summary>
public class BtnCtrl : MonoBehaviour
{
    int levelNumber;
    Button btn;
    Image btnImg;
    Text btnText;
    Transform star1, star2, star3;

    public Sprite lockedBtn;
    public Sprite unlockedBtn;
    public string sceneName;
    void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);

        btn = transform.gameObject.GetComponent<Button>();
        btnImg = btn.GetComponent<Image>();
        btnText = btn.gameObject.transform.GetChild(0).GetComponent<Text>();

        star1 = btn.gameObject.transform.GetChild(1);
        star2 = btn.gameObject.transform.GetChild(2);
        star3 = btn.gameObject.transform.GetChild(3);

        BtnStatus();
    }
    /// <summary>
    /// Locks or Unlocks a certain button. Also shows the number of stars awarded.
    /// </summary>
    void BtnStatus()
    {
        bool unlocked = DataCtrl.instance.isUnlocked(levelNumber);
        int starsAwarded = DataCtrl.instance.getStars(levelNumber);

        if(unlocked)
        {
            if(starsAwarded == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
            }if(starsAwarded == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }
            if(starsAwarded == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            if(starsAwarded == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }

            btn.onClick.AddListener(LoadScene);
           
        }
        else
        {
            btnImg.overrideSprite = lockedBtn;
            btnText.text = "";

            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }

    void LoadScene()
    {
        LoadingCtrl.instance.ShowLoading();

        SceneManager.LoadScene(sceneName);
    }

}
