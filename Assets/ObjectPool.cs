using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    /// <summary>
    /// Sunflower seeds initialisation
    /// </summary>
    public List<GameObject> projectileObjects;
    public GameObject projectileObject;
    public int projectileNumberToPool;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ProjectilesPool();
    }

    private void ProjectilesPool()
    {
        projectileObjects = new List<GameObject>();
        GameObject projectiles;
        for(int i = 0; i < projectileNumberToPool; i++)
        {
            projectiles = Instantiate(projectileObject);
            projectiles.SetActive(false);
            projectileObjects.Add(projectiles);
        }
    }

    public GameObject GetSunflowerSeedObject()
    {
        for(int i = 0; i < projectileNumberToPool; i++)
        {
            if(!projectileObjects[i].activeInHierarchy)
            {
                return projectileObjects[i];
            }
        }
        return null;
    }
}   
