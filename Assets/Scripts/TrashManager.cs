using System;
using UnityEngine;

public class TrashManager : MonoBehaviour
{

    private Vector3 _startingPosition;
    private Vector3 _endPosition;

    public void Setup(Vector3 startingPosition)
    {
        transform.position = startingPosition;
        _endPosition = -startingPosition;
        _endPosition.z = -1;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPosition, 3f * Time.deltaTime);
    }
}
