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

    //instantiate line cell with id to set sprites
    public void GenerateLine(LevelTwoConfig config)
    {
        float x = 0f;
        foreach (var levelTwoConfigEntity in config.LevelTwoConfigEntities)
        {
            x += 1;
            GameObject prefab = Resources.Load("Prefabs/LineCell") as GameObject; 
            GameObject instance = Instantiate(prefab, new Vector3(x, 0, 0), Quaternion.identity);
            instance.transform.SetParent(gameObject.transform);
            LineCell lineCell = instance.GetComponent<LineCell>();
            for (int i = 1; i < 4; i++)
            {
                // Sprite sprite = Resources.Load<Sprite>($"Sprites/alphabet/{levelTwoConfigEntity.id}{i}");
                Sprite sprite = Resources.Load<Sprite>($"Sprites/alphabet/a{i}");
                lineCell.AddSprites(sprite);
            }
            _lineCells.Add(lineCell);
        }
    }

    public void RefreshLineCells(List<int> snapshot)
    {
        int i = 0;
        foreach (var lineCell in _lineCells)
        {
            lineCell.SetColor(snapshot[i]);
            i += 1;
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
