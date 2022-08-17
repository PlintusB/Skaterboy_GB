using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private InputManager _input;

    [Header("Jump Settings")]
    [SerializeField] private float _gravity;
    [SerializeField] private float _startJumpVelocity;
    [SerializeField] private float _fallingAcceleration;
    private bool _isGrounded;
    private float _yVelocity;
    private float _groundY;
    private float _fixedTimeStep;

    [Header("Drive Settings")]
    [SerializeField] private float _speed;

    [Header("RoadLines Settings")]
    [SerializeField] private List<Transform> _roadLines;
    [SerializeField] private int _startLineNumber;
    private int _currentLineNumber;

    [Header("Other objects")]
    [SerializeField] private Transform _rightScreenSide;
    [SerializeField] private Transform _leftScreenSide;
    private List<float> _roadLineYPositions = new List<float>();

    private void Awake()
    {
        _input = InputManager.Instance;

        _fixedTimeStep = Time.fixedDeltaTime;
        _currentLineNumber = _startLineNumber;
        if (_startLineNumber > _roadLines.Count)
            _startLineNumber = _roadLines.Count;
        if (_startLineNumber < 1)
            _startLineNumber = 1;

        foreach (var line in _roadLines)
        {
            _roadLineYPositions.Add(line.position.y
                   + transform.localScale.y / 4);
        }
        
        _groundY = _roadLineYPositions[_startLineNumber - 1];
    }

    private void FixedUpdate()
    {
        UseGravity();
    }

    private void Update()
    {
        ChangeRoadLine();
        Jump();
        HorizontalMove();
    }

    private void UseGravity()
    {
        Vector2 pos = transform.position;
        if (!_isGrounded)
        {
            pos.y += _yVelocity * _fixedTimeStep;
            _yVelocity += _gravity * _fixedTimeStep;

            if (_yVelocity < 0)
                _yVelocity -= _fallingAcceleration * _fixedTimeStep;

            if (pos.y <= _groundY)
            {
                pos.y = _groundY;
                _isGrounded = true;
            }
        }

        transform.position = new Vector2(transform.position.x, pos.y);
    }

    private void HorizontalMove()
    {
        float currentXInputValue = _input.HorizontalAxis;

        if (currentXInputValue == 0)
            return;

        transform.Translate(transform.right *
                                currentXInputValue *
                                _speed *
                                Time.deltaTime);
        if(currentXInputValue > 0)
            CheckRightScreenSide();
        else
            CheckLeftScreenSide();        
    }

    private void Jump()
    {
        if (_isGrounded && _input.IsJumpButtonDown)
        {
            _isGrounded = false;
            _yVelocity = _startJumpVelocity;
        }
    }

    private void ChangeRoadLine()
    {
        if(_input.IsDownButtonPressed)
        {
            if (_currentLineNumber >= _roadLines.Count)
                return;
            _currentLineNumber++;
            float currentYPosition = _roadLineYPositions[_currentLineNumber - 1];
            transform.position = new Vector2(transform.position.x - 0.25f,
                currentYPosition);
            _groundY = currentYPosition;
            CheckLeftScreenSide();
        }

        if (_input.IsUpButtonPressed)
        {
            if (_currentLineNumber <= 1)
                return;
            _currentLineNumber--;
            float currentYPosition = _roadLineYPositions[_currentLineNumber - 1];
            transform.position = new Vector2(transform.position.x + 0.25f,
                currentYPosition);
            _groundY = currentYPosition;
            CheckRightScreenSide();
        }
    }

    private void CheckRightScreenSide()
    {
        if (transform.position.x > _rightScreenSide.position.x)
            transform.position =
                new Vector2(_rightScreenSide.position.x,
                            transform.position.y);
    }

    private void CheckLeftScreenSide()
    {
        if (transform.position.x < _leftScreenSide.position.x)
            transform.position =
                new Vector2(_leftScreenSide.position.x,
                            transform.position.y);
    }


    // логика столкновения: проверить, на той ли полосе стоит препятствие,
    // по которой едет герой.
}
