using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCannon : TowerWeapon
{
    [Header("[Cannon]")]
    [SerializeField] GameObject m_projectilePrefab;
    ProjectileMemoryPool m_projectileMemoryPool;
    private void Awake()
    {
        m_projectileMemoryPool=GetComponent<ProjectileMemoryPool>();
    }
    override protected IEnumerator SearchTarget()
    {
        while (true)
        {
            AttackTarget = FindClosestAttackTarget();

            if (AttackTarget != null)
            {
                ChangeState(WeaponState.TryAttackCannon);
            }
            yield return null;
        }
    }
    IEnumerator TryAttackCannon()
    {
        while (true)
        {
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
            }

            yield return new WaitForSeconds(m_towerTemplate.weapon[CurLevel].rate);

            SpawnProjectile();
        }
    }
    void SpawnProjectile()
    {
        GameObject clone = m_projectileMemoryPool.SpawnProjectile();//Instantiate(m_projectilePrefab, m_spawnPoint.position, Quaternion.identity);

        float damage = m_towerTemplate.weapon[CurLevel].damage + AddedDamage;
        clone.GetComponent<Projectile>().Init(AttackTarget, damage,m_projectileMemoryPool,m_spawnPoint);
    }
}
