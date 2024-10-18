using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Destroy(gameObject);
    }
}
