using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    private void Awake()
    {
        OffAttackRange();
    }

    public void OnAttackRange(Vector3 p_position, float p_range)
    {
        gameObject.SetActive(true);

        float diameter = p_range * 2.0f;
        transform.localScale = Vector3.one * diameter;
        transform.position = p_position;
    }

    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
