using System.Collections.Generic;
using UnityEngine;

public class PooledBullets : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject storedBullets;
    
    private List<GameObject> poolPrefabs;

    private void Awake()
    {
        poolPrefabs = new List<GameObject>();
    }

    public GameObject GetBullet(Vector3 position)
    {
        return SpawnItemFromPool(bulletPrefab, poolPrefabs, position);
    }

    private GameObject SpawnItemFromPool(GameObject prefab, List<GameObject> pool, Vector3 position)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].transform.position = position;
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject instancedObj = Instantiate(prefab, position, Quaternion.identity, storedBullets.transform);
        pool.Add(instancedObj);
        return instancedObj;
    }
}
