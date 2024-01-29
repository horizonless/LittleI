using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class LevelFourController : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject BloodGO;
    public GameObject PackManGo;
    public GameObject WinGo;
    public GameObject HolderGO;
    private Transform _initTransform;

    private async void Start()
    {
        _initTransform = PlayerGO.transform;
        await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
        Win();
    }

    public void EnableBlood()
    {
        BloodGO.SetActive(true);
        BloodGO.GetComponent<AIController>().Player = PlayerGO.transform;
    }

    public void ResetLevel()
    {
        Destroy(gameObject);
        LevelController.StartLevel("Level4");
    }

    public void Win()
    {
        PackManGo.transform.DORotate(Vector3.zero, 2f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.5f).OnComplete(() =>
            {
                PackManGo.transform.DOMove(WinGo.transform.position, 1f).OnComplete(() =>
                {
                    Destroy(HolderGO.gameObject);
                });
            }); 
    }

    private void Update()
    {
        return;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 1.5f || Mathf.Abs(mouseY) > 1.5f)
        {
            TrueWin();
        }
    }

    public void TrueWin()
    {
        LevelController.StartLevel("Level5");
        Destroy(gameObject);
    }
    
}
