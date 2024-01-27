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
    // Start is called before the first frame update
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
            await UniTask.Delay(TimeSpan.FromSeconds(changeRate), ignoreTimeScale: false);
            _spriteRenderer.sprite = spritesLooper[UnityEngine.Random.Range(0, spritesLooper.Count)];
        }
    }
}
