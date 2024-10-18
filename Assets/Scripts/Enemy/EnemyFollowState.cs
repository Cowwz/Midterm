using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyFollowState : EnemyState
{
    float _distanceToPlayer;
    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy will start following the Player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will stop following the Player");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > 10)
            {
                //Going back to idle
                _enemy.ChangeState(new EnemyIdleState(_enemy));

            }

            //Set the attack
            if (_distanceToPlayer < 2)
            {
                //Go to the attack state
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            //Going back to Idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}
