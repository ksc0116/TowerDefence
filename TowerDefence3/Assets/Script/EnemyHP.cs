using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float m_maxHP;
    float m_currentHP;
    bool m_isDie = false;
    Enemy m_enemy;
    SpriteRenderer m_spriteRenderer;

    public float MaxHP
    {
        set => m_maxHP = Mathf.Max(0,value);
        get { return m_maxHP; }
    }
    public float CurrentHP => m_currentHP;
    private void Awake()
    {
        m_currentHP = m_maxHP;
        m_enemy = GetComponent<Enemy>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        m_isDie = false;
        m_spriteRenderer.color = Color.white;
        m_currentHP = m_maxHP;
    }

    public void TakeDamage(float p_damage)
    {
        if (m_isDie == true) return;

        m_currentHP-= p_damage;

        StopCoroutine("HitEffect");
        StartCoroutine("HitEffect");

        if (m_currentHP <= 0)
        {
            m_isDie = true;
            m_enemy.OnDie(EnemyDestroyType.Kill);
        }
    }
    IEnumerator HitEffect()
    {
        Color color = m_spriteRenderer.color;

        color.a = 0.4f;
        m_spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1f;
        m_spriteRenderer.color = color;
    }
}
