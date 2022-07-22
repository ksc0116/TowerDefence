using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    MemoryPool m_pool;

    private void Awake()
    {
        m_pool =new MemoryPool(projectilePrefab);
    }

    public GameObject SpawnProjectile()
    {
        return m_pool.ActivePoolItem();
    }

    public void DestroyProjectile(GameObject remove)
    {
        m_pool.DeactivePoolItem(remove);
    }
}
