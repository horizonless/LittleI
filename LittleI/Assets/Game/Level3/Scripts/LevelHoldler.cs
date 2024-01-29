using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHoldler : MonoBehaviour
{
    public MazeGenerator mazeGenerator;
    public PlayerMark playerController;
    public WinMark winMarkController;
    public EnemyController enemyController;

    public LevelThreeController LevelThreeController;
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }
    
    public void ResetGame()
    {
        LevelThreeController.ResetLevel();
    }

    public void Win()
    {
        LevelThreeController.Win();
    }

    public void StartLevel()
    {
        mazeGenerator.GenerateNewMaze();
        mazeGenerator.ClearFootprints();
        winMarkController.PlaceWinCondition();
        playerController.ResetPlayerPosition();
        enemyController.ResetAndDeactivateEnemy();
    }
}
