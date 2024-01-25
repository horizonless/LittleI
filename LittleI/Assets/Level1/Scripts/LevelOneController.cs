using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LevelOneController : MonoBehaviour
{
    [SerializeField] LevelOneConfig _levelOneConfig;
    public DiscController discController;

    private async void Start()
    {
        while (true)
        {
            foreach (var levelOneConfigEntity in _levelOneConfig.LevelOneConfigEntities)
            {
                discController.ChangeColor(levelOneConfigEntity.value);
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            }
        }
    }
}
