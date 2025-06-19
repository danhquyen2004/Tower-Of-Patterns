using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _instance;
    public static ObjectPooling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPooling>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ObjectPooling");
                    _instance = obj.AddComponent<ObjectPooling>();
                }
            }
            return _instance;
        }
    }
    
    // Dictionary to hold object pools
    Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();
    
    // Method to get an object from the pool
    public GameObject GetObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new List<GameObject>();
        }

        List<GameObject> pool = pools[prefab];
        
        // Check if there are any inactive objects in the pool
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects, instantiate a new one
        GameObject newObj = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        pool.Add(newObj);
        return newObj;
    }
    
    public virtual T Getcomp<T>(T prefab) where T : MonoBehaviour
    {
        return this.GetObject(prefab.gameObject).GetComponent<T>();
    }
}
