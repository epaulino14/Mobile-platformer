using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Calls mothods from static classes like GameCtrl, AudioCtrl, etc.
/// </summary>
public class Router : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        GameCtrl.instance.ShowPauseMenu();
    }

    public void HidePauseMenu()
    {
        GameCtrl.instance.HidePauseMenu();
    }

    public void ToggleSound()
    {
        AudioCtrl.instance.ToggleSound();
    }

    public void ToggleMusic()
    {
        AudioCtrl.instance.ToggleMusic();
    }
}
