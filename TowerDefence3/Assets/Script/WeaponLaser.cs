using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLaser : TowerWeapon
{
    [Header("[Laser]")]
    [SerializeField] LineRenderer m_lineRenderer;
    [SerializeField] Transform m_hitEffect;
    [SerializeField] LayerMask m_targetLayer;

    override protected IEnumerator SearchTarget()
    {
        while (true)
        {
            AttackTarget = FindClosestAttackTarget();

            if (AttackTarget != null)
            {
                ChangeState(WeaponState.TryAttackLaser);
            }
            yield return null;
        }
    }

    override public bool Upgrade()
    {
        if (m_playerGold.CurrentGold < m_towerTemplate.weapon[CurLevel + 1].cost) return false;

        CurLevel++;

        m_spriteRenderer.sprite = m_towerTemplate.weapon[CurLevel].sprite;

        m_playerGold.CurrentGold -= m_towerTemplate.weapon[CurLevel].cost;

        m_lineRenderer.startWidth = 0.05f + CurLevel * 0.05f;
        m_lineRenderer.endWidth = 0.05f;

        m_towerSpawner.OnBuffAllBuffTowers();

        return true;
    }

    IEnumerator TryAttackLaser()
    {
        EnableLaser();

        while (true)
        {
            if (IsPossibleToAttackTarget() == false)
            {
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            SpawnLaser();

            yield return null;
        }
    }
    void EnableLaser()
    {
        m_lineRenderer.gameObject.SetActive(true);
        m_hitEffect.gameObject.SetActive(true);
    }
    void DisableLaser()
    {
        m_lineRenderer.gameObject.SetActive(false);
        m_hitEffect.gameObject.SetActive(false);
    }
    void SpawnLaser()
    {
        Vector3 direction = AttackTarget.transform.position - m_spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(m_spawnPoint.position, direction, m_towerTemplate.weapon[CurLevel].range, m_targetLayer);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform == AttackTarget)
            {
                m_lineRenderer.SetPosition(0, m_spawnPoint.position);
                m_lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);

                m_hitEffect.position = hit[i].point;

                float damage = m_towerTemplate.weapon[CurLevel].damage + AddedDamage;

                AttackTarget.GetComponent<EnemyHP>().TakeDamage(damage * Time.deltaTime);
            }
        }
    }


}
