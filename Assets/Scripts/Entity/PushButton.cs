using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    //[SerializeField] MeshRenderer _buttonMesh;
    //[SerializeField] private Material _defaultColour, _hoverColour;

    public UnityEvent _onPush;

    public UnityEvent onHoverEnter, onHoverExit;

    public void OnHoverEnter()
    {
        //_buttonMesh.material = _hoverColour;
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        //_buttonMesh.material = _defaultColour;
        onHoverExit?.Invoke();
    }

    public void OnSelect()
    {
        Debug.Log("Button Pressed");
        _onPush?.Invoke();
    }
}
