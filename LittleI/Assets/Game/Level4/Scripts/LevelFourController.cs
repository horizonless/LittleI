using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LevelFourController : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject BloodGO;
    private Transform _initTransform;

    private async void Start()
    {
        _initTransform = PlayerGO.transform;
        // await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
    }

    public void EnableBlood()
    {
        BloodGO.SetActive(true);
        BloodGO.GetComponent<AIController>().Player = PlayerGO.transform;
    }

    public void ResetLevel()
    {
        Destroy(gameObject);
        LevelController.StartLevel("Level4");
    }
}
