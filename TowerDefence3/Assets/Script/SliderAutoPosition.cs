using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAutoPosition : MonoBehaviour
{
    [SerializeField] Vector3 m_distance = Vector3.down * 20.0f;
    Transform m_targetTransform;
    RectTransform m_rectTransform;
    EnemyMemoryPool enemyMemoryPool;

    public void Init(Transform p_target,EnemyMemoryPool pool)
    {
        enemyMemoryPool = pool;
        m_targetTransform = p_target;
        m_rectTransform = GetComponent<RectTransform>();    
    }

    private void LateUpdate()
    {
        if(m_targetTransform.gameObject.activeSelf ==false)
        {
            enemyMemoryPool.DestroyHPBar(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(m_targetTransform.position);
        m_rectTransform.position = screenPosition+m_distance;
    }
}
