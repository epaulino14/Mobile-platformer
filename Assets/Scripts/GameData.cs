using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// The Data model for the game data
/// </summary>
/// 
[Serializable]
public class GameData
{
    public int coinCount;
    public int score;
    public bool[] keyFound;
    public LevelData[] levelData;
    public int lives;
    public bool isFirstBoot;

    public bool playSound;
    public bool playMusic;
}
