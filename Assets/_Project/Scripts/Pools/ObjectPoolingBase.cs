using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingBase : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public int initialPool;
    public bool canGrow = true;

    private List<GameObject> pooledMemoryObjects;
    private GameObject tmp;

    void Start()
    {
        pooledMemoryObjects = new List<GameObject>();
        for (int i = 0; i < initialPool; i++)
        {
            CreateNewObject();
        }
    }

    private void CreateNewObject()
    {
        tmp = Instantiate(pooledObjects[Random.Range(0, pooledObjects.Count)], transform);
        tmp.SetActive(false);
        pooledMemoryObjects.Add(tmp);
    }

    public virtual GameObject GetPooledObject(float duration)
    {
        for (int i = 0; i < pooledMemoryObjects.Count; i++)
        {
            if (!pooledMemoryObjects[i].activeInHierarchy)
            {
                StartCoroutine(DisableCurrentPooledObject(pooledMemoryObjects[i], duration));
                return pooledMemoryObjects[i];
            }
        }
        if (canGrow)
        {
            CreateNewObject();
            StartCoroutine(DisableCurrentPooledObject(pooledMemoryObjects[pooledMemoryObjects.Count - 1], duration));
            return pooledMemoryObjects[pooledMemoryObjects.Count - 1];
        }
        return null;
    }

    private IEnumerator DisableCurrentPooledObject(GameObject currentObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentObject.SetActive(false);
    }
}
