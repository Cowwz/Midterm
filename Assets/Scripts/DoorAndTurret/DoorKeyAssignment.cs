using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKeyAssignment : MonoBehaviour
{
    public UnityEvent OnKeyPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnKeyPickup?.Invoke();
            Destroy(gameObject);
        }
    }
}
