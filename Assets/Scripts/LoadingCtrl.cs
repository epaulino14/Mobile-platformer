using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shows the loading screen.
/// </summary>
public class LoadingCtrl : MonoBehaviour
{
    public GameObject loadingScreen;
    public static LoadingCtrl instance;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowLoading()
    {
        loadingScreen.SetActive(true);
    }
}
