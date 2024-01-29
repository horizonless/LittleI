using System;
using System.Collections;
using System.Collections.Generic;
using ClockStone;
using UnityEngine;

public class LevelThreeController : MonoBehaviour
{
    public LevelHoldler LevelHoldler;
    public int maxWinCount = 1;
    private int _winCount = 0;

    private void Start()
    {
        AudioObject AO =AudioController.PlayMusic("Stage3");
        AO.FadeIn(2f);
        AO.transform.SetParent(gameObject.transform);
        AO.GetComponent<AudioSource>().loop = true;
    }

    public void ResetLevel()
    {
        Destroy(LevelHoldler.gameObject);
        GameObject levelGO = Resources.Load<GameObject>($"Prefabs/LevelHolder");
        GameObject instance = Instantiate(levelGO);
        instance.transform.SetParent(gameObject.transform);
        LevelHoldler newLevelHolder = instance.GetComponent<LevelHoldler>();
        LevelHoldler = newLevelHolder;
        newLevelHolder.LevelThreeController = this;
    }

    public void Win()
    {
        _winCount += 1;
        if (_winCount < maxWinCount)
        {
            ResetLevel();
        }
        else
        { 
            LevelController.StartLevel("Level4");
            Destroy(gameObject);
        }
    }
}
