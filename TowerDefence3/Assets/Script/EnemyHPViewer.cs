using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    EnemyHP m_enemyHP;
    Slider m_hpSlider;

    public void Init(EnemyHP p_enemyHP)
    {
        m_enemyHP = p_enemyHP;
        m_hpSlider = GetComponent<Slider>();    
    }

    private void Update()
    {
        m_hpSlider.value = m_enemyHP.CurrentHP / m_enemyHP.MaxHP;
    }
}
