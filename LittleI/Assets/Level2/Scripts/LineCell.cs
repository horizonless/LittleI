using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LineCell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> spritesLooper;
    public bool IsDanger { get; private set; }

    async void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), ignoreTimeScale: false);
            _spriteRenderer.sprite = spritesLooper[UnityEngine.Random.Range(0, spritesLooper.Count)];
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

    public void AddSprites(Sprite spriteVal)
    {
        spritesLooper.Add(spriteVal);
    }
}
