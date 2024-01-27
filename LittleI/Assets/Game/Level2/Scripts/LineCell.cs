using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class LineCell : MonoBehaviour
{
    public bool IsDanger { get; private set; }

    async void Start()
    {
        while (true)
        {
            SetColor(UnityEngine.Random.Range(0,2));
            await UniTask.Delay(TimeSpan.FromSeconds(5f), ignoreTimeScale: false);
        }
    }

    public void SetColor(int val)
    {
        if (val == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            IsDanger = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            IsDanger = false;
        }
    }
}
