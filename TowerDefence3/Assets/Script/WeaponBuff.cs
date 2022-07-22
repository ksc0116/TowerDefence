using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuff : TowerWeapon
{
    public void OnBuffAroundTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < towers.Length; i++)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();

            if (weapon.BuffLevel > Level) continue;

            if (Vector3.Distance(weapon.transform.position, transform.position) <= m_towerTemplate.weapon[CurLevel].range)
            {
                if (weapon.WeaponType == WeaponType.Cannon || weapon.WeaponType == WeaponType.Laser)
                {
                    weapon.AddedDamage = weapon.Damage * (m_towerTemplate.weapon[CurLevel].buff);

                    weapon.BuffLevel = Level;
                }
            }
        }
    }
}
