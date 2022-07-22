using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] Image m_imageScreen;
    [SerializeField] float m_maxHP = 20;
    float m_currentHP;

    public float CurrentHP => m_currentHP;
    public float MaxHP => m_maxHP;

    private void Awake()
    {
        m_currentHP = m_maxHP;
    }

    public void TakeDamage(float p_damage)
    {
        m_currentHP -= p_damage;

        StopCoroutine("HitEffect");
        StartCoroutine("HitEffect");

        if (m_currentHP <= 0)
        {

        }
    }

    IEnumerator HitEffect()
    {
        Color color = m_imageScreen.color;
        color.a = 0.4f;
        m_imageScreen.color = color;

        while (color.a >= 0.0f)
        {
            color.a-=Time.deltaTime;
            m_imageScreen.color=color;

            yield return null;
        }
    }
}
