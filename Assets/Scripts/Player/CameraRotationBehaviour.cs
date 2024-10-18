using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationBehaviour : MonoBehaviour
{
    private PlayerInput _input;

    [Header("Camera Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private bool _invertMouse;

    private float _camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        _camXRotation += Time.deltaTime * _input._mouseY * _turnSpeed * (_invertMouse ? 1 : -1); //another if else statement
        _camXRotation = Mathf.Clamp(_camXRotation, -40f, 40f);

        transform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }
}
