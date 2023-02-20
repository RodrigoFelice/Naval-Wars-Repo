using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    //
    public static ObjectPoolingManager instance;

    private void Awake() => instance = this;

    [Header("Game Objects to Pool")]
    [SerializeField] List<GameObject> objectPrefabs; 
    [SerializeField] int poolSize; 
    Dictionary<string, List<GameObject>> pools;

    [Header("Pool Childs")]
    [SerializeField] Transform[] poolParents; 

    void Start()
    {
        pools = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject newObj = Instantiate(objectPrefabs[i], poolParents[i]);
                newObj.SetActive(false);
                objectPool.Add(newObj);
            }

            pools.Add(objectPrefabs[i].name, objectPool);
        }
    }

    
    public GameObject GetObject(string poolName)
    {
        if (pools.ContainsKey(poolName))
        {
            for (int i = 0; i < pools[poolName].Count; i++)
            {
                if (!pools[poolName][i].activeInHierarchy)
                {
                    pools[poolName][i].SetActive(true);
                    return pools[poolName][i];
                }
            }

            GameObject newObj = Instantiate(objectPrefabs.Find(x => x.name == poolName), poolParents[System.Array.IndexOf(objectPrefabs.ToArray(), objectPrefabs.Find(x => x.name == poolName))]);
            newObj.SetActive(true);
            pools[poolName].Add(newObj);
            return newObj;
        }

        return null;
    }

    public void ReturnObject(string poolName, GameObject objectToReturn)
    {
        if (pools.ContainsKey(poolName)) objectToReturn.SetActive(false);
    }


    public void ClearAllChildrenObjects()
    {
        foreach(Transform childs in transform) childs.gameObject.SetActive(false);
    }
}