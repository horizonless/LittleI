using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpritesLooper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> spritesLooper;

    public float changeRate = 0.1f;
    private bool _isDestory = false;
    
    async void Start()
    {
        if (spritesLooper.Count == 0)
        {
            for (int i = 1; i < 3; i++)
            {
                Sprite sprite = Resources.Load<Sprite>($"Sprites/Alphabet/{gameObject.name}{i}");
                spritesLooper.Add(sprite);
            }
        }
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            if (_isDestory) return;
            _spriteRenderer.sprite = spritesLooper[UnityEngine.Random.Range(0, spritesLooper.Count)];
            await UniTask.Delay(TimeSpan.FromSeconds(changeRate), ignoreTimeScale: false);
        }
    }

    private void OnDestroy()
    {
        _isDestory = true;
    }
}
