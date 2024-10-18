using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveBehaviour))] //forces a dependent component on a GameObject
public class PlayerJumpBehaviour : Interactor
{

    [Header("Player Jump")]
    [SerializeField] private float _jumpVelocity;

    private PlayerMoveBehaviour _moveBehaviour;

    public override void Interact()
    {
        if(_moveBehaviour == null)
        {
            _moveBehaviour = GetComponent<PlayerMoveBehaviour>();
        }

        if (_input._jumpPressed && _moveBehaviour._isGrounded)
        {
            _moveBehaviour.SetYVelocity(_jumpVelocity);
        }
    }
}
