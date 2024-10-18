using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject _objToBuild;
    [SerializeField] private Transform _placementPoint;

    public void Build()
    {
        Instantiate(_objToBuild, _placementPoint.position, _placementPoint.rotation);
        Destroy(gameObject);
    }
}
