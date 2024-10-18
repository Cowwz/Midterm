using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;
    Health _playerHealth;
    float _healthDamagePerSecond = 20f;

    public EnemyAttackState(EnemyController enemy) : base(enemy) //constructor acts as a start method
    {
        _playerHealth = enemy._player.GetComponent<Health>();
    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy will Attack the Player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will stop attacking the Player");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > 2)
            {
                //Going back to follow state
                _enemy.ChangeState(new EnemyFollowState(_enemy));

            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            //Going back to Idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    void Attack()
    {
        if(_playerHealth != null)
        {
            _playerHealth.DeductHealth(_healthDamagePerSecond * Time.deltaTime);
        }
    }
}
