using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrashManager : MonoBehaviour
{

    private Vector3 _startingPosition;
    private Vector3 _endPosition;
    private bool _isPaused = false;
    private float _moveSpeed = 3f;

    private void Start()
    {
        _moveSpeed = Random.Range(3f, 5f);
    }

    public void Setup(Vector3 startingPosition)
    {
        transform.position = startingPosition;
        _endPosition = -startingPosition;
        _endPosition.z = -1;
    }

    public void Pause() => _isPaused = true;

    private void Update()
    {
        if (_isPaused) return;
        transform.position = Vector3.MoveTowards(transform.position, _endPosition, _moveSpeed * Time.deltaTime);
    }
}
