using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelTwoController : MonoBehaviour
{
    [SerializeField] LevelTwoConfig _levelTwoConfig;
    public Transform _linesTransform;
    public int PlayerStartIndex = 102;
    public int PanaltyNum = 2;
    public List<int> cellsGapNum;
    public int coinJumpTriggerNum = 2;
    public PlayerCursor PlayerCursor;
    
    private int _currentPlayerIndex;
    private int _currentCoinIndex;
    private int _currentGap;
    private LineCell[] _allLineCells;
    private Action _onLevelWin;
    private LineCell _currentCoinCell;
    private int _jumpCount = 0;
    private bool _shouldBlockInput = false;

    public async void Start()
    {
        _shouldBlockInput = true;
        AudioController.PlayMusic("Stage2");
        _currentPlayerIndex = PlayerStartIndex;
        _allLineCells = GetComponentsInChildren<LineCell>();
        Debug.Log("Count:" + _allLineCells.Length);
        _currentCoinIndex = _currentPlayerIndex + cellsGapNum[_jumpCount];
        _currentCoinCell = _allLineCells[_currentCoinIndex];
        _currentCoinCell.SetCoin(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        gameObject.transform.DOScale(new Vector3(0.4f, 0.4f, 1), 2f).OnComplete(() =>
        {
            _shouldBlockInput = false;
        });
    }

    void Update()
    {
        if(_shouldBlockInput) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerCursor.DoShake(1f);
            _shouldBlockInput = true;
            _linesTransform.DOMoveX(_linesTransform.position.x + 1 * 0.4f,1f).OnComplete(() =>
            {
                _shouldBlockInput = false;
            });
            // _linesTransform.Translate(Vector3.right* 1);
            _currentPlayerIndex -= 1;
            CheckPlayerPos(Vector3.right);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerCursor.DoShake(0.3f);
            _shouldBlockInput = true;
            // _linesTransform.Translate(Vector3.left * 1);
            _linesTransform.DOMoveX(_linesTransform.position.x - 1 * 0.4f,0.3f).OnComplete(() =>
            {
                
                _shouldBlockInput = false;
            });
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
            Destroy(gameObject);
            LevelController.StartLevel("Level3");
        }
        if (_allLineCells[_currentPlayerIndex].IsDanger)
        {
            Destroy(gameObject);
            LevelController.StartLevel("Level2");
        }
        
        if ((_currentCoinIndex - _currentPlayerIndex) < coinJumpTriggerNum)
        {
            _jumpCount += 1;
            Debug.Log("jumpCount" + _jumpCount + " gapCount:" + cellsGapNum.Count);
            if(_jumpCount >=  cellsGapNum.Count) return;
            AudioController.Play("Stage2_CoinLeap_sfx");
            _currentCoinCell.SetCoin(false);
            _currentCoinIndex = _currentPlayerIndex + cellsGapNum[_jumpCount];
            _currentCoinCell = _allLineCells[_currentCoinIndex];
            _currentCoinCell.SetCoin(true);
        }
        
    }
}
