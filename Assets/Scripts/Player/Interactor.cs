using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput _input; //subclasses can access this variable without creating another var in the subclass with protected.


    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance(); 
    }

    // Update is called once per frame
    void Update()
    {
        Interact(); //Don't have to call the interact method in subclasses because it is already in this update method
    }

    public abstract void Interact();
}
