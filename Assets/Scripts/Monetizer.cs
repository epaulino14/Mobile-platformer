using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shows and Hides the ads in the game.
/// </summary>
public class Monetizer : MonoBehaviour
{
    public bool timedBanner;
    public float bannerDuration;

    void Start()
    {
        AdsCtrl.instance.ShowBanner();
    }

    private void OnDisable()
    {
        if (!timedBanner)
            AdsCtrl.instance.HideBanner();
        else
            AdsCtrl.instance.HideBanner(bannerDuration);
    }
}
