using System;
using UnityEngine;

public class MissileManager : MonoBehaviour
{

    private GameObject _playerObject;
    private GameManager _gameManager;

    public void Setup(GameObject player, GameManager gameManager)
    {
        _playerObject = player;
        _gameManager = gameManager;
    }

    private void Update()
    {
        if (!_playerObject) return;
        if (_gameManager.GetGameState() != GameManager.GameState.Playing) return;
        Vector3 pos = _playerObject.transform.position;
        Vector3 direction = pos - transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector3.MoveTowards(transform.position, pos, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            _gameManager.MissileTouchedAsteroid();
            Destroy(gameObject);
        }
    }
}
