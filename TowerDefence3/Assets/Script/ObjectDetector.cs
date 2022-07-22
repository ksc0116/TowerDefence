using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] TowerSpawner m_towerSpawner;
    [SerializeField] TowerDataViewer m_towerDataViewer;

    Camera m_cam;
    Ray m_ray;
    RaycastHit m_hit;
    Transform m_hitTransform = null;

    private void Awake()
    {
        m_cam = Camera.main;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true) return;

        if (Input.GetMouseButtonDown(0))
        {
            m_ray = m_cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(m_ray,out m_hit, Mathf.Infinity))
            {
                m_hitTransform = m_hit.transform;

                if(m_hit.transform.tag == "Tile")
                {
                    m_towerSpawner.SpawnTower(m_hit.transform);
                }
                else if (m_hit.transform.tag == "Tower")
                {
                    m_towerDataViewer.OnPanel(m_hit.transform);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(m_hitTransform == null || m_hitTransform.tag != "Tower")
            {
                m_towerDataViewer.OffPanel();
            }
            m_hitTransform = null;
        }
    }
}
