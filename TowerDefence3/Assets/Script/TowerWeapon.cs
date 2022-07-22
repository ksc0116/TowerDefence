using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Cannon=0, Laser, Slow, Buff,}
public enum WeaponState {  SearchTarget = 0, TryAttackCannon, TryAttackLaser}

public class TowerWeapon : MonoBehaviour
{
    [Header("[Commons]")]
    [SerializeField] protected TowerTemplate m_towerTemplate;
    [SerializeField] protected Transform m_spawnPoint;
    [SerializeField] protected WeaponType m_weaponType;
    int m_level = 0;


    WeaponState m_weaponState = WeaponState.SearchTarget;
    Transform m_attackTarget = null;
    protected SpriteRenderer m_spriteRenderer;
    protected TowerSpawner m_towerSpawner;
    protected PlayerGold m_playerGold;
    EnemySpawner m_enemySpawner;
    Tile m_ownerTile;

    float m_addedDamage;
    int m_buffLevel;

    public Sprite TowerSprite => m_towerTemplate.weapon[m_level].sprite;
    public float Damage => m_towerTemplate.weapon[m_level].damage;
    public float Rate => m_towerTemplate.weapon[m_level].rate;
    public float Range => m_towerTemplate.weapon[m_level].range;
    public int UpgradeCost => Level < MaxLevel ? m_towerTemplate.weapon[m_level + 1].cost : 0;
    public int SellCost => m_towerTemplate.weapon[m_level].sell;
    public int Level => m_level+1;
    public int CurLevel
    {
        set { m_level = value; }
        get { return m_level; }
    }
    public int MaxLevel => m_towerTemplate.weapon.Length;
    public float Slow => m_towerTemplate.weapon[m_level].slow;
    public float Buff => m_towerTemplate.weapon[m_level].buff;
    public WeaponType WeaponType => m_weaponType;
    public Transform AttackTarget
    {
        set => m_attackTarget = value;
        get { return m_attackTarget; }
    }
    
    public float AddedDamage
    {
        set => m_addedDamage = Mathf.Max(0,value);
        get => m_addedDamage;   
    }
    public int BuffLevel
    {
        set => m_buffLevel = Mathf.Max(0,value);
        get => m_buffLevel;
    }

    public void Init(TowerSpawner p_towerSpawner,EnemySpawner p_enemySpawner,PlayerGold p_playerGold, Tile p_ownerTile)
    {
        m_spriteRenderer=GetComponent<SpriteRenderer>();
        m_enemySpawner = p_enemySpawner;
        m_playerGold = p_playerGold;
        m_ownerTile = p_ownerTile;
        m_towerSpawner = p_towerSpawner;

        if (m_weaponType == WeaponType.Cannon || m_weaponType == WeaponType.Laser)
        {
            ChangeState(WeaponState.SearchTarget);
        }
    }

    

    protected Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;

        for (int i = 0; i < m_enemySpawner.EnemyList.Count; i++)
        {
            float distance = Vector3.Distance(m_enemySpawner.EnemyList[i].transform.position, transform.position);

            if (distance <= m_towerTemplate.weapon[m_level].range && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                m_attackTarget = m_enemySpawner.EnemyList[i].transform;
            }
        }

        return m_attackTarget;
    }

    protected bool IsPossibleToAttackTarget()
    {
        if (m_attackTarget == null) return false;

        float distance = Vector3.Distance(m_attackTarget.position,transform.position);
        if(distance > m_towerTemplate.weapon[m_level].range)
        {
            m_attackTarget = null;
            return false;
        }
        else if (m_attackTarget.gameObject.activeSelf == false)
        {
            m_attackTarget = null;
            return false;
        }

        return true;
    }

    public void ChangeState(WeaponState p_weaponState)
    {
        StopCoroutine(m_weaponState.ToString());
        m_weaponState = p_weaponState;
        StartCoroutine(m_weaponState.ToString());
    }
    private void Update()
    {
        if (m_attackTarget != null)
        {
            RotateToTarget();
        }
    }

    void RotateToTarget()
    {
        float dx = m_attackTarget.position.x - transform.position.x;
        float dy = m_attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Euler(0,0, degree);    
    }
    virtual protected IEnumerator SearchTarget()
    {
        yield return null;
    }


    virtual public bool Upgrade()
    {
        if (m_playerGold.CurrentGold < m_towerTemplate.weapon[m_level + 1].cost) return false;

        m_level++;

        m_spriteRenderer.sprite = m_towerTemplate.weapon[m_level].sprite;

        m_playerGold.CurrentGold-=m_towerTemplate.weapon[m_level].cost;

        m_towerSpawner.OnBuffAllBuffTowers();

        return true;
    }
    public void Sell()
    {
        m_playerGold.CurrentGold += m_towerTemplate.weapon[m_level].sell;

        m_ownerTile.isBuild = false;

        Destroy(gameObject);
    }
}
