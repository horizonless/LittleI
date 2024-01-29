using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shapes;
using UnityEngine;


enum RoundColorEnum
{
    
    Red,
}

public class RoundPattern
{
    private float duration;
    private RoundColorEnum color;
}

public class DiscController : MonoBehaviour
{
    public LevelOneController LevelOneController;
    public Color rightColor = Color.red; // 可以在Unity编辑器中修改
    public Color wrongColor = Color.blue; // 可以在Unity编辑器中修改
    [Range(0.1f, 1f)]
    public float growRadius = 0.5f; // 圆圈变化速度
    [Range(0.1f, 1f)]
    public float shrinkRadius = 0.5f; // 圆圈变化速度
    [Range(0.1f, 1f)]
    public float shrinkRate = 0.01f; // 圆圈缩小速度
    [Range(0.1f, 1f)]
    public float minRadius = 0.1f;
    public float winRadius = 12f;

    private float _currentSize;
    private Disc _disc;

    private void Start()
    {
        _disc = GetComponent<Disc>();
        _disc.Radius = minRadius;
    }

    void Update()
    {
        _currentSize -= shrinkRate * Time.deltaTime;
        _currentSize = Mathf.Clamp(_currentSize, minRadius, 20f);
        _disc.Radius = _currentSize;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_disc.Color == rightColor)
            {
                AudioController.Play("Stage1_spacebar_sfx");
                Debug.Log("grow");
                _disc.Radius += growRadius;
                _currentSize = _disc.Radius;
            }
            
            if (_disc.Color == wrongColor)
            {
                _disc.Radius -= shrinkRadius;
                _currentSize = _disc.Radius;
            }
            
        }

        CheckWinCondition();
    }

    public void ChangeColor(int val)
    {
        Debug.Log("change color: " + val);
        _disc.Color = val == 1 ? wrongColor : rightColor;
    }

    void CheckWinCondition()
    {
        if (_currentSize >= winRadius) 
        {
            LevelOneController.Win();
        }
    }
}