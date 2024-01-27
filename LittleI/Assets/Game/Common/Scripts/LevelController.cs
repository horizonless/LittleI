using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameObject _currentLevelGO;
    private void Start()
    {
        StartLevel("Level2");
    }

    public void StartLevel(string levelName)
    {
        GameObject levelGO = Resources.Load<GameObject>($"Prefabs/{levelName}");
        GameObject instance = Instantiate(levelGO);
        LevelTwoController levelTwoController = instance.GetComponent<LevelTwoController>();
        levelTwoController.Init(Win);
    }

    public void Win()
    {
        Debug.Log("win");
    }
}
