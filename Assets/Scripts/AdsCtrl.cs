using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
/// <summary>
/// Handles ads in the Game.
/// </summary>
public class AdsCtrl : MonoBehaviour
{
    public static AdsCtrl instance = null;
    public string Android_Afmob_Banner_ID;

    public bool testMode;
    BannerView bannerView;
    void Start()
    {
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void RequestBanner()
    {
        if (testMode)
        { 
            bannerView = new BannerView(Android_Afmob_Banner_ID, AdSize.Banner, AdPosition.Top); 
        }
        else
        {
            // code for live ad
        }

        AdRequest adRequest = new AdRequest.Builder().Build();

        bannerView.LoadAd(adRequest);

        HideBanner();
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }
    public  void HideBanner()
    {
        bannerView.Hide();
    }
    public void HideBanner(float duration)
    {
        StartCoroutine("HideBannerRoutine", duration);
    }

    IEnumerator HideBannerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        bannerView.Hide();
    }
    private void OnEnable()
    {
        RequestBanner();
    }
    private void OnDisable()
    {
        bannerView.Destroy();
    }
}
