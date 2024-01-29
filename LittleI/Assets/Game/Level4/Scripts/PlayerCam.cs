using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    public Transform firstLookTrans;

    private float xRotaion;
    private float yRotation;

    private async void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            yRotation -= Time.deltaTime * sensY;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            yRotation += Time.deltaTime * sensY;
        }

        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
