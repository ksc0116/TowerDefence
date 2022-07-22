using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    Camera m_cam;

    private void Awake()
    {
        m_cam = Camera.main;
    }

    private void Update()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = m_cam.ScreenToWorldPoint(position);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
