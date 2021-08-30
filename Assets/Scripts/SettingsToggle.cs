using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Toggles the Social Buttons
/// </summary>
public class SettingsToggle : MonoBehaviour
{
    public RectTransform btnFB, btnT, btnG, btnR;
    public float moveFB, moveT, moveG, moveR;
    public float defaulPosY, defaultPosX;
    public float speed;

    bool expanded;

    void Start()
    {
        expanded = false;
    }

    public void Toggle()
    {
        if(!expanded)
        {
            btnFB.DOAnchorPosY(moveFB, speed,0, false);
            btnT.DOAnchorPosY(moveT, speed,0, false);
            btnG.DOAnchorPosY(moveG, speed,0, false);
            btnR.DOAnchorPosY(moveR, speed,0, false);
            expanded = true;
        }
        else
        {
            btnFB.DOAnchorPosY(defaulPosY, speed, 0, false);
            btnT.DOAnchorPosY(defaulPosY, speed, 0, false);
            btnG.DOAnchorPosY(defaulPosY, speed, 0, false);
            btnR.DOAnchorPosY(defaulPosY, speed, 0, false);
            expanded = false;
        }
    }
}
