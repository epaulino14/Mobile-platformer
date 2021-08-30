using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Provides functionality to the Buttons like faacebook, Twitter follow, google Plus & ratings
/// </summary>
public class SettingsCtrl : MonoBehaviour
{
    public string facebookURL, twitterURL, googlePlusURL, RatingsURL;
    
    public void FacebookLike()
    {
        Application.OpenURL(facebookURL);
    }
    public void TwitterFollow()
    {
        Application.OpenURL(twitterURL);
    }
    public void GooglePlus()
    {
        Application.OpenURL(googlePlusURL);
    }
    public void Rating()
    {
        Application.OpenURL(RatingsURL);
    }
}
