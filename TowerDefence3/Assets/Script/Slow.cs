using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    TowerWeapon m_towerWeapon;
    private void Awake()
    {
        m_towerWeapon = GetComponentInParent<TowerWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;

        Movement moveMent = collision.GetComponent<Movement>();
        moveMent.MoveSpeed -= moveMent.MoveSpeed * m_towerWeapon.Slow;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") return;

        collision.GetComponent<Movement>().ResetMoveSpeed();
    }
}
