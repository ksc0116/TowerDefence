using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileMemoryPool projectileMemory;
    Movement m_movement;
    Transform m_target;
    float m_damage;
    ProjectileMemoryPool projectileMemoryPool;

    public void Init(Transform p_target,float p_damage,ProjectileMemoryPool p_pool,Transform shotPoint)
    {
        transform.position = shotPoint.position;
        projectileMemoryPool = GetComponent<ProjectileMemoryPool>();
        m_movement = GetComponent<Movement>();
        m_target = p_target;
        m_damage = p_damage;
        projectileMemoryPool=p_pool;
    }
    private void Update()
    {
        if(m_target!= null)
        {
            Vector3 direction = (m_target.position - transform.position).normalized;
            m_movement.MoveTo(direction);
        }

        if(m_target.gameObject.activeSelf ==false)
        {
            projectileMemoryPool.DestroyProjectile(gameObject);//Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.tag == "Enemy")) return;
        if (collision.transform != m_target) return;

        collision.GetComponent<EnemyHP>().TakeDamage(m_damage);
        projectileMemoryPool.DestroyProjectile(gameObject);//Destroy(gameObject);
    }
}
