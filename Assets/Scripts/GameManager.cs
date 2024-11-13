using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int _points = 0;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject addedPointsPrefab;
    [SerializeField] private GameObject trashPrefab;
    private GameState _gameState = GameState.Paused;
    private List<Vector3> _trashSpawnPoints;

    private TMP_Text _pointsTMP;
    
    void Start()
    {
        _pointsTMP = canvas.transform.Find("PointsText").GetComponent<TMP_Text>();

        _trashSpawnPoints = new List<Vector3>();
        GameObject trashSPs = GameObject.Find("SpawnPoints_Trash");
        foreach (Transform trans in trashSPs.transform)
        {
            _trashSpawnPoints.Add(trans.position);
        }
        Destroy(trashSPs);
        
        UpdatePointsText();
        
        //InvokeRepeating(nameof(AddP), 3.0f, 3.0f);
        InvokeRepeating(nameof(SpawnTrash), 3.0f, 3.0f);
    }

    public void SpawnTrash()
    {
        GameObject trash = Instantiate(trashPrefab);
        Vector3 startPos = _trashSpawnPoints[Random.Range(0, _trashSpawnPoints.Count)];
        trash.GetComponent<TrashManager>().Setup(startPos);
    }
    
    public void AddP() => AddPoints(1);

    public void AddPoints(int points)
    {
        _points += points;
        UpdatePointsText();
        Instantiate(addedPointsPrefab, _pointsTMP.transform).GetComponent<TMP_Text>().text = $"+ {points}";
    }

    public void CatchTrash()
    {
        AddPoints(50);
    }
    
    private void UpdatePointsText()
    {
        _pointsTMP.text = $"Points: <color=#5de381>{_points}";
    }

    public enum GameState
    {
        Playing, Paused
    }
    public GameState GetGameState() => _gameState;
    public void SetGameState(GameState gameState) => _gameState = gameState;

    public void Collided()
    {
        SetGameState(GameState.Paused);
        // add death screen or check for lives
    }
}
