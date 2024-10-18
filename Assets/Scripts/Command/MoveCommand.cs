using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent _agent;
    private Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        this._agent = agent;
        this._destination = destination;
    }

    public override bool isComplete => ReachedDestination();

    public override void Execute()
    {
        _agent.SetDestination(_destination);
    }

    bool ReachedDestination()
    {
        if (_agent.remainingDistance > 0.5f)
            return false;

        return true;
    }
}
