using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _pickupLayer;

    public UnityEvent _OnCubePlaced;
    public UnityEvent _OnCubeRemoved;

    private void OnCollisionEnter(Collision collision)
    {
        //check is the collider of the cube is close enought to the center of the pressure pad.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _checkRadius, _pickupLayer);

        foreach(var collider in hitColliders)
        {
            Debug.Log("Collider in contact = " + collider.gameObject.name);

            if (collider.CompareTag("PickCube"))
            {
                _OnCubePlaced?.Invoke();
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PickCube"))
        {
            _OnCubeRemoved?.Invoke();
        }
    }
}
