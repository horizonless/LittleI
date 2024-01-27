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
}
