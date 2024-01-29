using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject enemyTrailPrefab; // EnemyTrail 预制体
    public MazeGenerator mazeGenerator;
    public PlayerMark playerController;
    public float moveSpeed = 1f;
    public float startDelay = 10f;
    public LevelHoldler LevelHolder;

    private Vector2Int initialPosition = new Vector2Int(0, 0);
    private float moveRate = 0.25f;
    private float nextMoveTime;
    private bool isChasing;
    private bool startChasingPlayer = false;
    private Vector2Int lastPosition;
    private Vector2Int secondLastPosition;
    private Vector2Int currentPos;

    private void Start()
    {
        currentPos = initialPosition;
        lastPosition = initialPosition;
        secondLastPosition = initialPosition;
        gameObject.SetActive(false);
        Invoke(nameof(ActivateEnemy), startDelay);
    }
    private Vector2Int GetNextFootprintPosition(Vector2Int currentPos)
    {
        Vector2Int bestNextPos = currentPos;
        float bestDistance = float.MaxValue;
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var dir in directions)
        {
            Vector2Int nextPos = currentPos + dir;
            // 检查这个位置是否有脚印，且不是最近访问过的
            if (mazeGenerator.HasFootprint(nextPos) && nextPos != lastPosition && nextPos != secondLastPosition)
            {
                float distanceToPlayer = (playerController.GetCurrentGridPosition() - nextPos).sqrMagnitude;
                // 选择离玩家最近的位置
                if (distanceToPlayer < bestDistance)
                {
                    bestNextPos = nextPos;
                    bestDistance = distanceToPlayer;
                }
            }
        }

        secondLastPosition = lastPosition;
        lastPosition = bestNextPos;
        return bestNextPos;
    }
    private void ActivateEnemy()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(initialPosition.x + SpawnPos.position.x, initialPosition.y + SpawnPos.position.y, 0);
        isChasing = true;
        nextMoveTime = Time.time;
        Invoke(nameof(StartChasingPlayer), 2f);
    }

    private void StartChasingPlayer()
    {
        startChasingPlayer = true;
    }
    private void MoveTowardsPlayer()
    {
        Vector2Int enemyPos = currentPos;

        Vector2Int nextPos = GetNextFootprintPosition(enemyPos);
        if (!mazeGenerator.IsWall(nextPos.x, nextPos.y))
        {
            currentPos = nextPos;
            CreateEnemyTrail(enemyPos); // 在当前位置创建 EnemyTrail
            transform.position = new Vector3(nextPos.x / MazeGenerator.shrinkRate + SpawnPos.position.x, nextPos.y / MazeGenerator.shrinkRate + SpawnPos.position.y, 0);
            //CreateEnemyTrail();
        }
    }
    private void CreateEnemyTrail(Vector2Int position)
    {
        if (enemyTrailPrefab != null)
        {
            var trailInstance = Instantiate(enemyTrailPrefab, new Vector3(position.x / MazeGenerator.shrinkRate + SpawnPos.position.x, position.y / MazeGenerator.shrinkRate + SpawnPos.position.y, -1), Quaternion.identity);
            trailInstance.transform.SetParent(LevelHolder.transform);
        }
        else
        {
            Debug.LogError("EnemyTrail prefab is missing.");
        }
    }

    private void Update()
    {
        if (isChasing && startChasingPlayer && Time.time >= nextMoveTime)
        {
            MoveTowardsPlayer();
            nextMoveTime = Time.time + moveRate / moveSpeed;
        }
    }
    public void ResetAndDeactivateEnemy()
    {
        gameObject.SetActive(false);
        Invoke(nameof(ActivateEnemy), startDelay);
        // 如果需要，这里还可以添加清除脚印的代码
    }
    private Vector2Int GetDirectionTowardsPlayer(Vector2Int enemyPos, Vector2Int playerPos)
    {
        Vector2Int direction = playerPos - enemyPos;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return new Vector2Int((int)Mathf.Sign(direction.x), 0);
        }
        else
        {
            return new Vector2Int(0, (int)Mathf.Sign(direction.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelHolder.ResetGame();
    }
}
