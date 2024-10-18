using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameObject _objectToPool;
    public int _startSize;

    [SerializeField] private List<PooledObjects> _objectPool = new List<PooledObjects>();
    [SerializeField] private List<PooledObjects> _usedPool = new List<PooledObjects>();

    private PooledObjects _tempObj;

    // Start is called before the first frame update
    void Start()
    {
        InitialisePool();
    }

    void InitialisePool()
    {
        for(int i = 0; i < _startSize; i++)
        {
            AddNewObjects();
        }
    }

    void AddNewObjects()
    {
        _tempObj = Instantiate(_objectToPool, transform).GetComponent<PooledObjects>();
        _tempObj.gameObject.SetActive(false);
        _tempObj.SetObjectPool(this);
        _objectPool.Add(_tempObj);
    }

    public PooledObjects GetPooledObject()
    {
        PooledObjects tempObject;
        if(_objectPool.Count > 0)
        {
            tempObject = _objectPool[0];
            _usedPool.Add(tempObject);
            _objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObjects();
            tempObject = GetPooledObject();
        }

        tempObject.gameObject.SetActive(true);
        return tempObject;
    }
    
    public void DestroyPooledObjects(PooledObjects obj, float time = 0)
    {
        if(time == 0)
        {
            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObjects obj)
    {
        Debug.Log("Restored");
        obj.gameObject.SetActive(false);
        _usedPool.Remove(obj);
        _objectPool.Add(obj);
    }
}
