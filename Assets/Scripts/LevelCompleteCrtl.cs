using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Handles the level complete AI
/// </summary>
public class LevelCompleteCrtl : MonoBehaviour
{
    [Header("Button/Images/Text")]
    public Button bntNext;
    public Sprite goldenStar;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    public Text txtScore;

    [Header("Values")]
    public int levelNumber;
    [HideInInspector]
    public int score;

    public int scoreForThreeStars;
    public int scoreForTwoStars;
    public int scoreForOneStar;
    public int scoreForNextLevel;
    public float animStartDelay;
    public float animDelay;

    bool showTwoStars, showThreeStars;

    void Start()
    {
        score = GameCtrl.instance.GetScore();
        txtScore.text = "" + score;

        if(score >= scoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if(score >= scoreForTwoStars && score <scoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
        }
        if(score >= scoreForOneStar && score!= 0 && score < scoreForTwoStars)
        {
            GameCtrl.instance.SetStarAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }
    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }
    IEnumerator HandleFirstStarAnim(Image startImg)
    {
        DoAnim(startImg);

        // cuase a delay before showing star
        yield return new WaitForSeconds(animDelay);

        //call if more than one star is awarded
        if (showTwoStars || showThreeStars)
            StartCoroutine("HandleSecondStarAnim", Star2);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleSecondStarAnim(Image startImg)
    {
        DoAnim(startImg);

        // cuase a delay before showing star
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleThirdStarAnim(Image startImg)
    {
        DoAnim(startImg);

        // cuase a delay before showing star
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;

        Invoke("CheckLevelStatus", 1.2f);
    }
     void DoAnim(Image startImg)
     {
        //Increased the star size
        startImg.rectTransform.sizeDelta = new Vector2(150f, 150f);

        //show the golden star
        startImg.sprite = goldenStar;

        //reduce the star size beack to normal
        RectTransform t = startImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);

        //play audio effect
        AudioCtrl.instance.KeyFound(startImg.gameObject.transform.position);

        //show sparkles effect
        SFXCtrl.instance.ShowPowerupSparkle(startImg.gameObject.transform.position);

     }

    void CheckLevelStatus()
    {
        if(score >= scoreForNextLevel)
        {
            bntNext.interactable = true;

            SFXCtrl.instance.ShowPowerupSparkle(bntNext.gameObject.transform.position);

            AudioCtrl.instance.KeyFound(bntNext.gameObject.transform.position);
            GameCtrl.instance.UnlockNextLevel(levelNumber);
        }
        else
        {
            bntNext.interactable = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
