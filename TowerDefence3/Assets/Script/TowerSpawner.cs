using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] TowerTemplate[] m_towerTemplate;
    [SerializeField] EnemySpawner m_enemySpwaner;
    [SerializeField] PlayerGold m_playerGold;
    [SerializeField] SystemTextViewer m_systemTextViewer;

    bool m_isOnTowerButton = false;
    GameObject m_followTowerClone = null;
    int m_towerType;
    public void ReadyToSpawnTower(int p_type)
    {
        m_towerType = p_type;
        if (m_isOnTowerButton == true) return;

        if (m_towerTemplate[m_towerType].weapon[0].cost > m_playerGold.CurrentGold)
        {
            m_systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        m_isOnTowerButton = true;

        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        m_followTowerClone = Instantiate(m_towerTemplate[m_towerType].followTowerPrefab, Camera.main.ScreenToWorldPoint(position),Quaternion.identity);

        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform p_tileTransform)
    {
        if (m_isOnTowerButton == false) return;

        Tile tile = p_tileTransform.GetComponent<Tile>();

        if (tile.isBuild == true)
        {
            m_systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        m_isOnTowerButton = false;

        tile.isBuild = true;

        m_playerGold.CurrentGold -= m_towerTemplate[m_towerType].weapon[0].cost;

        Vector3 position = p_tileTransform.position + Vector3.back;

        GameObject clone = Instantiate(m_towerTemplate[m_towerType].towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Init(this,m_enemySpwaner,m_playerGold,tile);

        OnBuffAllBuffTowers();

        Destroy(m_followTowerClone);

        StopCoroutine("OnTowerCancelSystem");
    }
    IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                m_isOnTowerButton = false;

                Destroy(m_followTowerClone);
                break;
            }

            yield return null;
        }
    }

    public void OnBuffAllBuffTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for(int i=0;i<towers.Length;i++)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();

            if(weapon.WeaponType == WeaponType.Buff)
            {
                weapon.GetComponent<WeaponBuff>().OnBuffAroundTower();
            }
        }
    }
}
