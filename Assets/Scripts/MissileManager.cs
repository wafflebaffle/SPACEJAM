using System;
using UnityEngine;

public class MissileManager : MonoBehaviour
{

    private GameObject _playerObject;

    private void Start()
    {
        _playerObject = GameObject.Find("Player");
    }

    public void Setup(GameObject player)
    {
        _playerObject = player;
    }

    private void Update()
    {
        Vector3 pos = _playerObject.transform.position;
        Vector3 direction = pos - transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector3.MoveTowards(transform.position, pos, 5f * Time.deltaTime);
    }
}
