using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Camera _camera;
    private GameManager _gameManager;
    
    private float _moveSpeed = 4f;
    private float _dashSpeed = 15f;
    private float _dashDuration = 0.25f;
    private float _dashCooldown = 2f;
    private float _dashTimeRemaining = 0f;
    private float _cooldownTimeRemaining = 0f;
    private Vector3 _movement;
    
    void Start()
    {
        _camera = Camera.main;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.GetGameState() != GameManager.GameState.Playing) return;
        Vector3 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = transform.position.z;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (_cooldownTimeRemaining <= 0f)
            {
                _dashTimeRemaining = _dashDuration;
                _cooldownTimeRemaining = _dashCooldown;
            }
        }

        if (_dashTimeRemaining > 0f)
        {
            _dashTimeRemaining -= Time.deltaTime;
        }
        else if (_cooldownTimeRemaining > 0f)
        {
            _cooldownTimeRemaining -= Time.deltaTime;
        }

        float currentSpeed = (_dashTimeRemaining > 0f) ? _dashSpeed : _moveSpeed;

        //Vector3 finalRot = 

        if (_gameManager)
        {
            //_movement = transform.position - _movement;
            _gameManager.MoveLittle();
        }
        transform.position = Vector3.MoveTowards(transform.position, worldPos, currentSpeed * Time.deltaTime);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Vector3 closestPoint = other.ClosestPoint(transform.position);

        if (other.CompareTag("Missile"))
        {
            _gameManager.Collided();
            return;
        }
        if (other.CompareTag("Asteroid")) _gameManager.Collided();
        if (other.CompareTag("Trash"))
        {
            _gameManager.CatchTrash();
            Destroy(other.gameObject);
        }
        
    }
}
