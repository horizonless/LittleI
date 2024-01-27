using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private List<LineCell> _lineCells = new List<LineCell>();
    private int _currentPlayerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        LineCell[] lineCells = GetComponentsInChildren<LineCell>();
        float x = 0.5f;
        for (int i = 0; i < lineCells.Length; i++)
        {
            lineCells[i].gameObject.transform.position = new Vector3(x, 0, 0);
            x += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.right* 1);
            CheckPlayerPos();
            _currentPlayerIndex -= 1;
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.left * 1);
            CheckPlayerPos();
            _currentPlayerIndex += 1;
        }
    }

    //check if the line cell pass through is red
    public void CheckPlayerPos()
    {
        if (_lineCells[_currentPlayerIndex].IsDanger)
        {
            Debug.Log("IsDanger!");
        }
    }
}
