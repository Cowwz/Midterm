using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInterator : Interactor
{
    //If we didn't get rid of the update method, it would override the update method in the abstract class.
    

    [Header("Gun")]
    public MeshRenderer _gunRenderer;
    public Color _bulletColour;
    public Color _rocketColour;

    [Header("Shoot")]
    
    public ObjectPool _bulletPool;
    public ObjectPool _rocketPool;

    [SerializeField] private float _shootVelocity;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayerMoveBehaviour _moveBehaviour;

    private float _finalShootVelocity;
    private IShootStrategy _currentStrategy;

    public override void Interact()
    {
        if(_currentStrategy == null)
        {
            _currentStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon1Pressed)
        {
            _currentStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon2Pressed)
        {
            _currentStrategy = new RocketShootStrategy(this);
        }


        //Shoot Strategy
        if (_input._primaryShootPressed && _currentStrategy != null)
        {
            _currentStrategy.Shoot();
        }
    }
    
    //void Shoot()
    //{
    //    PooledObjects pooledObj = _objPool.GetPooledObject();
    //    pooledObj.gameObject.SetActive(true);

    //    //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
    //    Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
    //    bullet.transform.position = _shootPoint.position;
    //    bullet.transform.rotation = _shootPoint.rotation;

    //    bullet.velocity = _shootPoint.forward * _finalShootVelocity;

    //    //Destroy(bullet.gameObject, 5.0f);
    //    _objPool.DestroyPooledObjects(pooledObj, 5.0f);
    //}
    


    //Exposing the variables without changing the variable to public
    public Transform GetShootPoint()
    {
        return _shootPoint;
    }

    public float GetShootVelocity()
    {
        _finalShootVelocity = _moveBehaviour.GetForwardSpeed() + _shootVelocity;
        return _finalShootVelocity;
    }
}
