using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractor : Interactor
{
    [Header("Interact")]
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionDistance;

    private RaycastHit _rayCastHit;
    private ISelectable _selectable;

    public override void Interact()
    {
        //Cast a ray
        //Draw the ray from a point
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _interactionDistance, _interactionLayer))
        {
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();

            if (_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (_input._activatePressed)
                {
                    _selectable.OnSelect();
                }
            }
        }


        if (_rayCastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }
}
