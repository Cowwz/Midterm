using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private bool _isUp;
    [SerializeField] private float _speed;

    Vector3 _destination;
    bool _isMoving;

    public void ToggleLift()
    {
        if (_isMoving)
            return;

        if (_isUp)
        {
            _destination = transform.localPosition - new Vector3(0, _distance, 0);
            _isUp = false;
        }
        else
        {
            _destination = transform.localPosition + new Vector3(0, _distance, 0);
            _isUp = true;
        }

        _isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _destination, _speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.localPosition, _destination) < 0.05f)
        {
            _isMoving = false;
        }
    }
}
