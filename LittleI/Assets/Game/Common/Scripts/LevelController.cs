using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string StartLevelName = "Level1";
    private GameObject _currentLevelGO;
    private void Start()
    {
        StartLevel(StartLevelName);
    }

    public static void StartLevel(string levelName)
    {
        GameObject levelGO = Resources.Load<GameObject>($"Prefabs/{levelName}");
        GameObject instance = Instantiate(levelGO);
    }
}
