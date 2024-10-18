using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager _endingLvl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _endingLvl.EndLvl();
            Destroy(gameObject);
        }
    }
}
