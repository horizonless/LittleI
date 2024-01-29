using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.DOFade(0, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }

    public void DoShake(float duration)
    {
        transform.DORewind ();
        transform.DOPunchScale (new Vector3 (1, 1, 1), duration);
    }
}
