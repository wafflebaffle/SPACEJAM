using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Camera _camera;
    private float _moveSpeed = 4f;
    private float _dashSpeed = 15f;
    private float _dashDuration = 0.25f;
    private float _dashCooldown = 2f;
    private float _dashTimeRemaining = 0f;
    private float _cooldownTimeRemaining = 0f;
    
    void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
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

        transform.position = Vector3.MoveTowards(transform.position, worldPos, currentSpeed * Time.deltaTime);
    }
}
