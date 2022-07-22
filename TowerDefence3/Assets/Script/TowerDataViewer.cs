using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField] Image m_imageTower;
    [SerializeField] TextMeshProUGUI m_textDamage;
    [SerializeField] TextMeshProUGUI m_textRate;
    [SerializeField] TextMeshProUGUI m_textRange;
    [SerializeField] TextMeshProUGUI m_textLevel;
    [SerializeField] TextMeshProUGUI m_textUpgradeCost;
    [SerializeField] TextMeshProUGUI m_textSellCost;
    [SerializeField] TowerAttackRange m_towerAttackRange;
    [SerializeField] Button m_upgradeButon;
    [SerializeField] SystemTextViewer m_systemTextViewer;

    TowerWeapon m_currentTower;
    private void Awake()
    {
        OffPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform p_towerWeapon)
    {
        m_currentTower = p_towerWeapon.GetComponent<TowerWeapon>();
        gameObject.SetActive(true);
        UpdateTowerData();
        m_towerAttackRange.OnAttackRange(m_currentTower.transform.position, m_currentTower.Range);
    }
    public void OffPanel()
    {
        gameObject.SetActive(false);

        m_towerAttackRange.OffAttackRange();
    }
    void UpdateTowerData()
    {
        if(m_currentTower.WeaponType == WeaponType.Cannon || m_currentTower.WeaponType == WeaponType.Laser)
        {
            m_textDamage.text = "Damage : " + m_currentTower.Damage + "+" +"<color=red>" + m_currentTower.AddedDamage.ToString("F1") + "</color>";
        }
        else
        {
            if(m_currentTower.WeaponType == WeaponType.Slow)
            {
                m_textDamage.text = "Slow : " + m_currentTower.Slow * 100 + "%";
            }
            else if(m_currentTower.WeaponType== WeaponType.Buff)
            {
                m_textDamage.text = "Buff : " + m_currentTower.Buff * 100 + "%";
            }
        }

        m_imageTower.sprite = m_currentTower.TowerSprite;
        m_textRate.text = "Rate : " + m_currentTower.Rate;
        m_textRange.text = "Range : " + m_currentTower.Range;
        m_textLevel.text = "Level : " + m_currentTower.Level;
        m_textUpgradeCost.text = m_currentTower.UpgradeCost.ToString();
        m_textSellCost.text = m_currentTower.SellCost.ToString();

        m_upgradeButon.interactable = m_currentTower.Level < m_currentTower.MaxLevel ? true : false;
    }

    public void TowerUpgrade()
    {
        bool isSuccess = m_currentTower.Upgrade();

        if (isSuccess == true)
        {
            UpdateTowerData();

            m_towerAttackRange.OnAttackRange(m_currentTower.transform.position, m_currentTower.Range);
        }
        else
        {
            m_systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void TowerSell()
    {
        m_currentTower.Sell();

        OffPanel();
    }
}
