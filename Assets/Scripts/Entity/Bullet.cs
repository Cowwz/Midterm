using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) //the parameter is the collider of the other object. you can change the name of the parameter
    {
        Debug.Log($"Collided with {collision.gameObject.name}");

        //Checking the existence of the compenents and assigning it
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();

        if(destroyable != null)
        {
            destroyable.OnCollided();
        }

        Destroy(gameObject);
        
    }
}
