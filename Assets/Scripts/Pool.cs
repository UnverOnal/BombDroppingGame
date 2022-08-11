using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool
{
    private List<GameObject> pooledObjects;
    private GameObject objectToPool;

    private GameObject parentObject;
    
    private int amountToPool;

    private Vector3 initialScale;

    public Pool(int amountToPool, GameObject objectToPool, GameObject parentObject)
    {
        this.amountToPool = amountToPool;
        this.objectToPool = objectToPool;
        this.parentObject = parentObject;

        Initialize();
    }

    public void Initialize()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            CreatePooledObject(out _);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject objectToReturn = null;

        foreach(GameObject pooledObject in pooledObjects)
        {
            if(!pooledObject.activeInHierarchy)
            {
                objectToReturn = pooledObject;
                break;
            }
        }

        if(objectToReturn == null)
            CreatePooledObject(out objectToReturn);

        objectToReturn.SetActive(true);
        return objectToReturn;
    }

    private void CreatePooledObject(out GameObject pooledObject)
    {
        pooledObject = Object.Instantiate(objectToPool, parentObject.transform, true);
        pooledObject.SetActive(false);
        pooledObjects.Add(pooledObject);

        if (initialScale == default(Vector3))
            initialScale = pooledObject.transform.localScale;
    }

    public void ResetPool(Action<GameObject> action = null)
    {
        foreach (var pooledObject in pooledObjects)
        {
            //Reset parent
            pooledObject.transform.SetParent(parentObject.transform);
            
            //Reset transform
            pooledObject.transform.localScale = initialScale;
            pooledObject.transform.localPosition = Vector3.zero;
            pooledObject.transform.localRotation = Quaternion.identity;
            
            action?.Invoke(pooledObject);
            
            pooledObject.SetActive(false);
        }
    }
}
