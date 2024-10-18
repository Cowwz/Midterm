using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMoveBehaviour : MonoBehaviour
{
    private PlayerInput _input;

    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Ground Checker")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance;

    private CharacterController _characterController;

    private float _moveMultiplier = 1;
    private Vector3 _playerVelocity;
    public bool _isGrounded { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }
    void MovePlayer()
    {
        _moveMultiplier = _input._sprintHeld ? _sprintMultiplier : 1;

        _characterController.Move((transform.forward * _input._vertical + transform.right * _input._horizontal) * _moveSpeed * Time.deltaTime *
            _moveMultiplier); // + in this case is or

        //Ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundLayer);
    }

    public void SetYVelocity(float value)
    {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return _input._vertical * _moveSpeed * _moveMultiplier;
    }
}
