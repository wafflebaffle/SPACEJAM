using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int _points = 0;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject addedPointsPrefab;

    private TMP_Text _pointsTMP;
    
    void Start()
    {
        _pointsTMP = canvas.transform.Find("PointsText").GetComponent<TMP_Text>();

        UpdatePointsText();
        
        InvokeRepeating(nameof(AddP), 3.0f, 3.0f);
    }
    
    public void AddP() => AddPoints(1);

    public void AddPoints(int points)
    {
        _points += points;
        UpdatePointsText();
        Instantiate(addedPointsPrefab, _pointsTMP.transform).GetComponent<TMP_Text>().text = $"+ {points}";
    }

    private void UpdatePointsText()
    {
        _pointsTMP.text = $"Points: <color=#5de381>{_points}";
    }
}
