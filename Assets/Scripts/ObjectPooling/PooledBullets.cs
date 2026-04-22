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

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        //returns a bullet prefab from pool list
        return SpawnItemFromPool(bulletPrefab, poolPrefabs, position, rotation);
    }

    private GameObject SpawnItemFromPool(GameObject prefab, List<GameObject> pool, Vector3 position, Quaternion rotation)
    {
        //loops through pool list
        for (int i = 0; i < pool.Count; i++)
        {
            //checks if element in list is deactivated
            if (pool[i].activeInHierarchy == false)
            {
                //if so
                //changes position and rotation to given values
                pool[i].transform.position = position;
                pool[i].transform.rotation = rotation;
                //then reactivates bullet
                pool[i].SetActive(true);
                //returns deactivated bullet
                return pool[i];
            }
        }

        //if no deactivated bullets are present, then instantiates a new bullet
        GameObject instancedObj = Instantiate(prefab, position, Quaternion.identity, storedBullets.transform);
        //adds bullet to the pool list
        pool.Add(instancedObj);
        //returns instantiated bullet
        return instancedObj;
    }
}
