using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class LineCell : MonoBehaviour
{
    public GameObject CoinGO;
    public bool IsDanger { get; private set; }
    public bool IsCoin { get; private set; }
    private bool _isDestroy = false;

    async void Start()
    {
        if(string.IsNullOrWhiteSpace(gameObject.name)) return;
        while (true)
        {
            SetColor(UnityEngine.Random.Range(0,10));
            await UniTask.Delay(TimeSpan.FromSeconds(3f), ignoreTimeScale: false);
        }
    }

    public void SetColor(int val)
    {
        if (_isDestroy) return;
        if (val > 7)
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

    public void SetCoin(bool val)
    {
        IsCoin = val;
        CoinGO.SetActive(val);
        GetComponent<SpriteRenderer>().enabled = !val;
    }

    private void OnDestroy()
    {
        _isDestroy = true;
    }
}
