using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelTwoController : MonoBehaviour
{
    [SerializeField] LevelTwoConfig _levelTwoConfig;
    public Transform _linesTransform;
    public int PlayerStartIndex = 102;
    public int PanaltyNum = 2;
    private int _currentPlayerIndex;
    private LineCell[] _allLineCells;
    private Action _onLevelWin;

    public void Start()
    {
        AudioController.PlayMusic("Stage2");
        _currentPlayerIndex = PlayerStartIndex;
        _allLineCells = GetComponentsInChildren<LineCell>();
        Debug.Log("Count:" + _allLineCells.Length);
        int coinPos = UnityEngine.Random.Range(0, _allLineCells.Length);
        _allLineCells[coinPos].SetCoin(true);
    }

    public void Init(Action OnLevelWin)
    {
        _onLevelWin = OnLevelWin;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _linesTransform.Translate(Vector3.right* 1);
            _currentPlayerIndex -= 1;
            CheckPlayerPos(Vector3.right);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _linesTransform.Translate(Vector3.left * 1);
            CheckPlayerPos(Vector3.left);
            _currentPlayerIndex += 1;
        }
    }

    //check if the line cell pass through is red
    public void CheckPlayerPos(Vector3 dir)
    {

        if (_allLineCells[_currentPlayerIndex].IsCoin)
        {
            Debug.Log("IsCoin");
            _onLevelWin.Invoke();
        }

        if (_allLineCells[_currentPlayerIndex].IsDanger)
        {
            Debug.Log("IsDanger");
            GameObject levelGO = Resources.Load<GameObject>($"Prefabs/Level2");
            GameObject instance = Instantiate(levelGO);
            Destroy(gameObject);
        }
    }
}
