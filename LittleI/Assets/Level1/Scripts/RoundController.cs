using System.Collections;
using System.Collections.Generic;
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

public class RoundController : MonoBehaviour
{
    public Color color1 = Color.red; // 可以在Unity编辑器中修改
    public Color color2 = Color.blue; // 可以在Unity编辑器中修改
    public float changeRate = 0.5f; // 圆圈变化速度
    public float shrinkRate = 0.01f; // 圆圈缩小速度

    private float currentSize = 1f;
    private bool isGrowing = false;

    void Start()
    {
    }
    void Update()
    {
        // 处理颜色切换
        HandleColorChange();

        // 处理玩家输入
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GetComponent<SpriteRenderer>().color == color1)
            {
                isGrowing = true;
            }
            else if (GetComponent<SpriteRenderer>().color == color2)
            {
                isGrowing = false;
            }
        }

        // 根据当前状态调整圆圈大小
        AdjustCircleSize();

        // 检查通关条件
        CheckWinCondition();
    }

    void HandleColorChange()
    {
        // 这里添加颜色切换的逻辑，可能需要使用协程来根据时间表改变颜色
    }

    void AdjustCircleSize()
    {
        if (isGrowing)
        {
            currentSize += changeRate * Time.deltaTime;
        }
        else
        {
            currentSize -= shrinkRate * Time.deltaTime;
        }

        currentSize = Mathf.Clamp(currentSize, 1f, 10f); // 假设最大大小是10
        transform.localScale = new Vector3(currentSize, currentSize, 1f);
    }

    void CheckWinCondition()
    {
        // 当圆圈大小足够覆盖屏幕时，通关
        if (currentSize >= 10f) // 假设屏幕大小需要的圆圈大小是10
        {
            Debug.Log("关卡成功通关！");
            // 这里可以添加通关后的逻辑，比如加载下一个关卡
        }
    }
}