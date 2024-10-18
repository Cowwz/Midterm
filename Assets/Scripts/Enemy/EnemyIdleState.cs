using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyIdleState : EnemyState
{
    int _currentTarget = 0;
    //Constructor
    public EnemyIdleState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped Idling");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._agent.remainingDistance < 0.5f)
        {
            _currentTarget++;
            if (_currentTarget >= _enemy._targetPoints.Length)
                _currentTarget = 0;
            _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        }

        //Check for player
        if (Physics.SphereCast(_enemy._enemyEye.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found");
                _enemy._player = hit.transform;
                _enemy._agent.destination = _enemy._player.position;

                //Move to a new state
                //Move to follow state
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }
        }
    }
}
