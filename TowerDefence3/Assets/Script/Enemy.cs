using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType {  Kill=0, Arrive }
public class Enemy : MonoBehaviour
{
    public string type;

    int m_WayPointCount;
    Transform[] m_WayPoints;
    int m_CurrentIndex = 0;
    Movement m_Movement;
    EnemySpawner m_enemySpawner;

    [SerializeField] int m_gold = 10;
    public int Gold
    {
        set => m_gold = Mathf.Max(0, value);    
        get => m_gold;
    }

    private void OnEnable()
    {
        m_CurrentIndex = 0;
    }
    public void Init(EnemySpawner p_enemySpawner, Transform[] p_WayPoints)
    {
        m_Movement = GetComponent<Movement>();

        m_enemySpawner = p_enemySpawner;

        m_WayPointCount = p_WayPoints.Length;
        m_WayPoints = new Transform[m_WayPointCount];
        m_WayPoints = p_WayPoints;

        transform.position = m_WayPoints[m_CurrentIndex].position;

        StartCoroutine("MoveOn");
    }

    IEnumerator MoveOn()
    {
        NextMoveTo();

        while(true)
        {

            transform.Rotate(Vector3.forward * 10f);

            if(Vector3.Distance(transform.position, m_WayPoints[m_CurrentIndex].position) < 0.02f * m_Movement.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    void NextMoveTo()
    {
        if(m_CurrentIndex < m_WayPointCount - 1)
        {
            transform.position = m_WayPoints[m_CurrentIndex].position;

            m_CurrentIndex++;
            Vector3 direction = (m_WayPoints[m_CurrentIndex].position - transform.position).normalized;
            m_Movement.MoveTo(direction);
        }
        else
        {
            m_gold = 0;

            OnDie(EnemyDestroyType.Arrive);
        }
    }
    public void OnDie(EnemyDestroyType p_type)
    {
        m_enemySpawner.DestroyEnemy(p_type,this,m_gold);
    }
}
