using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    [SerializeField] LevelOneConfig _levelOneConfig;

    private void Start()
    {
        string t = "";
        _levelOneConfig.LevelOneConfigEntities.ForEach((v) => { t += v.value; });
        Debug.Log(t);
    }
}
