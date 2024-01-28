using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public float shrinkRate = 2.8f;
    public int width = 30;
    public int height = 16;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Transform CenterPos;
    public bool IsMazeReady { get; private set; }

    private bool[,] maze;
    private bool[,] playerFootprints; // 用于追踪玩家脚印

    // 添加一个公共方法来检查某个位置是否是墙
    public bool IsWall(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return !maze[x, y];
        }
        return true; // 如果位置超出迷宫范围，默认为墙
    }
    public void GenerateNewMaze()
    {
        maze = new bool[width, height];
        playerFootprints = new bool[width, height];
        GenerateMaze(0, 0);
        DrawMaze();
        IsMazeReady = true;
    }
    void Start()
    {
        maze = new bool[width, height];
        GenerateMaze(0, 0);
        DrawMaze();
        IsMazeReady = true; // 设置迷宫已准备好的标志
        transform.position = CenterPos.position;
    }

    void GenerateMaze(int x, int y)
    {
        maze[x, y] = true;

        while (true)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if (x > 1 && !maze[x - 2, y]) neighbors.Add(Vector2Int.left);
            if (x < width - 2 && !maze[x + 2, y]) neighbors.Add(Vector2Int.right);
            if (y > 1 && !maze[x, y - 2]) neighbors.Add(Vector2Int.down);
            if (y < height - 2 && !maze[x, y + 2]) neighbors.Add(Vector2Int.up);

            if (neighbors.Count == 0) return;

            Vector2Int direction = neighbors[Random.Range(0, neighbors.Count)];
            int nx = x + direction.x * 2;
            int ny = y + direction.y * 2;

            maze[nx, ny] = true;
            maze[x + direction.x, y + direction.y] = true;

            GenerateMaze(nx, ny);
        }
    }
    public void SetFootprint(Vector2Int position)
    {
        if (position.x >= 0 && position.x < width && position.y >= 0 && position.y < height)
        {
            playerFootprints[position.x, position.y] = true;
        }
    }

    public bool HasFootprint(Vector2Int position)
    {
        if (position.x >= 0 && position.x < width && position.y >= 0 && position.y < height)
        {
            return playerFootprints[position.x, position.y];
        }
        return false;
    }

    public void ClearFootprints()
    {
        playerFootprints = new bool[width, height];
    }
    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(x / shrinkRate, y / shrinkRate);
                var instance = Instantiate(maze[x, y] ? floorPrefab : wallPrefab, position, Quaternion.identity);
                instance.transform.SetParent(this.gameObject.transform);
            }
        }
    }
}
