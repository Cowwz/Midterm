using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private TurretState _currentState;

    public Vector3 _shootPoint;
    public float _playerCheckDistance;
    public LineRenderer _lazer;
    

    public Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = new TurretIdleState(this);
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeState(TurretState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    public void ShootLazer(Vector3 endPoint)
    {
        _lazer.SetPosition(0, _shootPoint);
        _lazer.SetPosition(1, endPoint);
    }
}
