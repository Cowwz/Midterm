using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] //Makes this script run before say -80. We want this script to exicute first
public class PlayerInput : MonoBehaviour
{
    public float _horizontal { get; private set; } //Ensures that outside of this class you can't modify this. (Encapsulation)
    public float _vertical { get; private set; } 
    public float _mouseX { get; private set; } 
    public float _mouseY { get; private set; } 

    public bool _sprintHeld { get; private set; }
    public bool _jumpPressed { get; private set; }
    public bool _activatePressed { get; private set; }
    public bool _primaryShootPressed { get; private set; }
    public bool _secondaryShootPressed { get; private set; }
    public bool _weapon1Pressed { get; private set; }
    public bool _weapon2Pressed { get; private set; }
    public bool _commandPressed { get; private set; }

    private bool _clear;

    //Singleton
    private static PlayerInput _instance; //if you change this to public, you can refrence this class anywhere with PlayerInput.variable or method

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;
    }

    public static PlayerInput GetInstance() //not needed if public _instance.
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClearInputs();
        ProcessInputs();
    }

    void ProcessInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _sprintHeld = _sprintHeld || Input.GetButton("Sprint");
        _jumpPressed = _jumpPressed || Input.GetButtonDown("Jump");
        _activatePressed = _activatePressed || Input.GetKeyDown(KeyCode.E);

        _primaryShootPressed = _primaryShootPressed || Input.GetButtonDown("Fire1");
        _secondaryShootPressed = _secondaryShootPressed || Input.GetButtonDown("Fire2");

        _weapon1Pressed = _weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        _weapon2Pressed = _weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);

        _commandPressed = _commandPressed || Input.GetKeyDown(KeyCode.Q);
    }

    private void FixedUpdate()
    {
        _clear = true;
    }

    void ClearInputs()
    {
        if (!_clear)
            return;

        _horizontal = 0;
        _vertical = 0;
        _mouseX = 0;
        _mouseY = 0;

        _sprintHeld = false;
        _jumpPressed = false;
        _activatePressed = false;

        _primaryShootPressed = false;
        _secondaryShootPressed = false;

        _weapon1Pressed = false;
        _weapon2Pressed = false;

        _commandPressed = false;
    }
}
