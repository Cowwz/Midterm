using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{
    [Header("Pick And Drop")]
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _attachPoint;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private LayerMask _pickupLayer;

    private bool _isPicked = false;
    private IPickable _pickable;
    private RaycastHit _rayCastHit;

    public override void Interact()
    {
        //cast a ray
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            if (_input._activatePressed && !_isPicked)
            {
                _pickable = _rayCastHit.transform.GetComponent<IPickable>();

                if (_pickable == null)
                    return;

                _pickable.OnPicked(_attachPoint);
                _isPicked = true;
                return;
            }
        }

        if (_input._activatePressed && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}
