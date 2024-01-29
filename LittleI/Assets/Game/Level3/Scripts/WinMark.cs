using UnityEngine;

public class WinMark : MonoBehaviour
{
    public Transform initPos;
    public MazeGenerator mazeGenerator;
    public LevelHoldler levelHoldler;

    public void PlaceWinCondition()
    {
        // 随机选择一个地板格子
        Vector2Int position;
        do
        {
            position = new Vector2Int(Random.Range(0, mazeGenerator.width), Random.Range(0, mazeGenerator.height));
        } while (mazeGenerator.IsWall(position.x, position.y)); // 确保选择的是地板格子

        // 将胜利条件 GameObject 放置在随机选定的地板格子上
        transform.position = new Vector3(initPos.position.x + position.x / MazeGenerator.shrinkRate, initPos.position.y + position.y / MazeGenerator.shrinkRate, 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        levelHoldler.Win();
    }
}