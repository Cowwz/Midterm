using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInterator _interactor;
    Transform _shootPoint;

    public RocketShootStrategy(ShootInterator interator)
    {
        Debug.Log("Switched to rocket mode");
        this._interactor = interator;
        _shootPoint = _interactor.GetShootPoint();

        //change color of gun
        _interactor._gunRenderer.material.color = _interactor._rocketColour;
    }

    public void Shoot()
    {
        PooledObjects pooledObj = _interactor._rocketPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;

        bullet.velocity = _shootPoint.forward * _interactor.GetShootVelocity();

        //Destroy(bullet.gameObject, 5.0f);
        _interactor._rocketPool.DestroyPooledObjects(pooledObj, 5.0f);
    }
}
