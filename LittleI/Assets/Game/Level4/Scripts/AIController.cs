using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class AIController : MonoBehaviour
{
    public LevelFourController LevelFourController;
    public Transform Player;
    public int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;




    async void Start()
    {
        MoveSpeed = 2;
        await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
        MoveSpeed = 3;
    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }
        }
        else
        {
            LevelFourController.ResetLevel();
        }
    }
}