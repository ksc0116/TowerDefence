using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed=0.0f;
    [SerializeField] Vector3 moveDir = Vector3.zero;

    float m_baseMoveSpeed;
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value);
        get => moveSpeed;
    }


    private void Awake()
    {
        m_baseMoveSpeed = moveSpeed;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDir = direction;
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = m_baseMoveSpeed;
    }

    private void Update()
    {
        transform.position+=moveDir*moveSpeed*Time.deltaTime;
    }
}
