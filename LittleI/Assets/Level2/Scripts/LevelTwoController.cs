using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelTwoController : MonoBehaviour
{
    [SerializeField] LevelTwoConfig _levelTwoConfig;
    public LineController lineController;
    private int _snapShotsCount;
    private List<List<int>> _snapShotsList = new List<List<int>>();
    private async void Start()
    {
        lineController.GenerateLine(_levelTwoConfig);
        List<int> snapShots1 = _levelTwoConfig.LevelTwoConfigEntities.Select(x => x.snapshot1).ToList();
        List<int> snapShots2 = _levelTwoConfig.LevelTwoConfigEntities.Select(x => x.snapshot2).ToList();
        List<int> snapShots3 = _levelTwoConfig.LevelTwoConfigEntities.Select(x => x.snapshot3).ToList();
        _snapShotsList.Add(snapShots1);
        _snapShotsList.Add(snapShots2);
        _snapShotsList.Add(snapShots3);
        
        foreach (var snapshot in _snapShotsList)
        {
            lineController.RefreshLineCells(snapshot);
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
        }
    }
}
