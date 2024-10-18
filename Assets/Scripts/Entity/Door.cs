using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] private Material _defaultMaterial, _detectedMaterial;
    //[SerializeField] private MeshRenderer _doorRenderer;
    [SerializeField] private Animator _doorAnimator;

    private bool _isLocked = true;

    private float _timer = 0;

    private const float _waitTime = 1.0f;


    private void OnTriggerEnter(Collider other)  //Runs like the Start Method, Once on Enter
    {
        if(!_isLocked && other.CompareTag("Player"))
        {
            //Reset the timer and also change the colour of the door
            _timer = 0;
           // _doorRenderer.material = _detectedMaterial;
        }
    }

    private void OnTriggerStay(Collider other)  //Runs while object in the trigger detection
    {
        if (_isLocked)
            return;

        if (!other.CompareTag("Player"))
            return;

        _timer += Time.deltaTime;

        if(_timer >= _waitTime)
        {
            _timer = _waitTime;
            OpenDoor(true);
            //_doorAnimator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)  //Runs on Exit
    {
        OpenDoor(false);
        //_doorAnimator.SetBool("isOpen", false);
        //_doorRenderer.material = _defaultMaterial;
    }

    public void LockDoor()
    {
        _isLocked = true;
    }

    public void UnlockDoor()
    {
        _isLocked = false;
    }

    public void OpenDoor(bool doorState)
    {
        if(!_isLocked)
            _doorAnimator.SetBool("isOpen", doorState);
    }
}
