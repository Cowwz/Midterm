using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    float _distanceToPlayer;
    Health _playerHealth;
    float _healthDamagePerSecond = 20f;

    public TurretAttackState(TurretController turret) : base(turret)
    {
        _playerHealth = turret._player.GetComponent<Health>();
    }

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
