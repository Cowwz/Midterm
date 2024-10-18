using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Transform _enemyEye;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _checkRadius = 0.8f;

    int _currentTarget = 0;

    public NavMeshAgent _agent;

    public bool _isIdle = true;
    public bool _isPlayerFound;
    public bool _isCloseToPlayer;

    public Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _targetPoints[_currentTarget].position;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isIdle)
        {
            Idle();
        }
        else if (_isPlayerFound)
        {
            if (_isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void Idle()
    {
        if(_agent.remainingDistance < 0.5f)
        {
            _currentTarget++;
            if (_currentTarget >= _targetPoints.Length)
                _currentTarget = 0;
            _agent.destination = _targetPoints[_currentTarget].position;
        }

        //Check for player
        if(Physics.SphereCast(_enemyEye.position, _checkRadius, transform.forward, out RaycastHit hit, _playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found");
                _isIdle = false;
                _isPlayerFound = true;
                _player = hit.transform;
                _agent.destination = _player.position;
            }
        }
    }

    void FollowPlayer()
    {
        if(_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) > 10)
            {
                _isPlayerFound = false;
                _isIdle = true;
            }

            //Set the attack
            if(Vector3.Distance(transform.position, _player.position) < 2)
            {
                _isCloseToPlayer = true;
            }
            else
            {
                _isCloseToPlayer = false;
            }

            _agent.destination = _player.position;
        }
        else
        {
            _isPlayerFound = false;
            _isIdle = true;
            _isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking the Player");
        if(Vector3.Distance(transform.position, _player.position) > 2)
        {
            _isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemyEye.position, _checkRadius);
        Gizmos.DrawWireSphere(_enemyEye.position + _enemyEye.forward * _playerCheckDistance, _checkRadius);

        Gizmos.DrawLine(_enemyEye.position, _enemyEye.position + _enemyEye.forward * _playerCheckDistance);
    }
}
