using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private bool _invertMouse;

    [Header("Ground Checker")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Rigidbody _rocketPrefab;
    [SerializeField] private float _shootForce;
    [SerializeField] private Transform _shootPoint;

    [Header("Interact")]
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionDistance;

    [Header("Pick And Drop")]
    [SerializeField] private Transform _attachPoint;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private LayerMask _pickupLayer;

    private CharacterController _characterController;

    private float _horizontal, _vertical, _mouseX, _mouseY, _camXRotations;
    private float _moveMultiplier = 1;
    private Vector3 _playerVelocity;
    private bool _isGrounded;

    //RayCast
    private RaycastHit _rayCastHit;
    private ISelectable _selectable;

    //Pick and Drop
    private bool _isPicked = false;
    private IPickable _pickable;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
        MovePlayer();
        Jump();
        RotatePlayer();

        ShootBullet();
        ShootRocket();

        Interact();
        PickAndDrop();
    }

    void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Sprint") ? _sprintMultiplier : 1;  // if you are sprinting then _moveMultiplier is now _sprintMultiper. else it's 1. this is an if else statement
    }

    void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime *
            _moveMultiplier); // + is this case is or

        //Ground check
        if(_isGrounded && _playerVelocity.y < 0)
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

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerVelocity.y = _jumpForce;
        }
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        _camXRotations += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1); //another if else statement
        _camXRotations = Mathf.Clamp(_camXRotations, -40f, 40f);

        _cameraTransform.localRotation = Quaternion.Euler(_camXRotations, 0, 0);
    }

    void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.AddForce(_shootPoint.forward * _shootForce);
            Destroy(bullet.gameObject, 5.0f);
        }
    }

    void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody rocket = Instantiate(_rocketPrefab, _shootPoint.position, _shootPoint.rotation);
            rocket.AddForce(_shootPoint.forward * _shootForce);
            Destroy(rocket.gameObject, 5.0f);
        }
    }

    void Interact()
    {
        //Cast a ray
        //Draw the ray from a point
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out _rayCastHit, _interactionDistance, _interactionLayer))
        {
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();

            if(_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }
            }
        }


        if(_rayCastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }

    void PickAndDrop()
    {
        //cast a ray
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            if(Input.GetKeyDown(KeyCode.E) && !_isPicked)
            {
                _pickable = _rayCastHit.transform.GetComponent<IPickable>();

                if (_pickable == null)
                    return;

                _pickable.OnPicked(_attachPoint);
                _isPicked = true;
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}
