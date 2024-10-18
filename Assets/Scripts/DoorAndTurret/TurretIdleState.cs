using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    Vector3 _endpoint;

    public TurretIdleState(TurretController turret) : base(turret)
    {
        _endpoint = new Vector3(_turret._shootPoint.x, _turret._shootPoint.y, 17f);
    }

    public override void OnStateEnter()
    {
        _turret.ShootLazer(_endpoint);
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateUpdate()
    {
        //if (Physics.Raycast()
        //{

        //}
    }
}
