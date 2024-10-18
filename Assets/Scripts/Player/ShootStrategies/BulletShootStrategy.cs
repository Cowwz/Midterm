using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInterator _interactor;
    Transform _shootPoint;

    public BulletShootStrategy(ShootInterator interactor)
    {
        Debug.Log("Switched to bullet mode");
        this._interactor = interactor;
        _shootPoint = _interactor.GetShootPoint();

        //Change the colour of the gun
        _interactor._gunRenderer.material.color = _interactor._bulletColour;
    }

    public void Shoot()
    {
        PooledObjects pooledObj = _interactor._bulletPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;

        bullet.velocity = _shootPoint.forward * _interactor.GetShootVelocity();

        //Destroy(bullet.gameObject, 5.0f);
        _interactor._bulletPool.DestroyPooledObjects(pooledObj, 5.0f);
    }
}
