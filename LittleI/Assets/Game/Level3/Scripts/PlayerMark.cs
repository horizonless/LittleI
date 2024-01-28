using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMark : MonoBehaviour
{
    public float moveSpeed = 1f;
    public MazeGenerator mazeGenerator;
    public Transform SpawnPos;

    private Vector2Int gridPosition;
    private float nextMoveTime = 0f;
    private float moveRate = 0.25f; // 每秒移动次数，可以调整以适应游戏的节奏
    public Queue<Vector2Int> playerTrail = new Queue<Vector2Int>();
    public int trailLength = 100; // 玩家轨迹的长度

    void Start()
    {
        // 初始化主角在迷宫左下角
        gridPosition = new Vector2Int(0, 0);
        transform.position = SpawnPos.position;
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            Vector2Int direction = Vector2Int.zero;

            // 检测 WASD 输入并转换为方向
            if (Input.GetKey(KeyCode.W)) direction += Vector2Int.up;
            if (Input.GetKey(KeyCode.A)) direction += Vector2Int.left;
            if (Input.GetKey(KeyCode.S)) direction += Vector2Int.down;
            if (Input.GetKey(KeyCode.D)) direction += Vector2Int.right;

            if (direction != Vector2Int.zero)
            {
                Move(direction);
                nextMoveTime = Time.time + moveRate;
            }
        }
        RecordTrail();
    }
    void RecordTrail()
    {
        Vector2Int currentPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        if (playerTrail.Count >= trailLength && playerTrail.Peek() != currentPos)
        {
            playerTrail.Dequeue();
        }

        if (playerTrail.Count == 0 || playerTrail.Peek() != currentPos)
        {
            playerTrail.Enqueue(currentPos);
        }
    }
    public Vector2Int GetNextTrailPosition()
    {
        if (playerTrail.Count > 0)
        {
            return playerTrail.Dequeue();
        }

        return GetCurrentGridPosition(); // 如果没有轨迹，保持在原地
    }
    public void ResetPlayerPosition()
    {
        gridPosition = new Vector2Int(0, 0);
        transform.position = SpawnPos.position;
    }
    public Vector2Int GetCurrentGridPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
    void Move(Vector2Int direction)
    {
        // 计算新位置
        Vector2Int newPosition = gridPosition + direction;

        // 使用 mazeGenerator.IsWall 来检查新位置是否是墙
        if (mazeGenerator != null && !mazeGenerator.IsWall(newPosition.x, newPosition.y))
        {
            // 更新位置
            gridPosition = newPosition;
            transform.position = new Vector3(SpawnPos.position.x + gridPosition.x / MazeGenerator.shrinkRate, SpawnPos.position.y + gridPosition.y / MazeGenerator.shrinkRate, 0);
            mazeGenerator.SetFootprint(gridPosition); // 留下脚印
        }
        else
        {
            Debug.Log("MazeGenerator reference is null or the position is a wall.");
        }
    }
}
