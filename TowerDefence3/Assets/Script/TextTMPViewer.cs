using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_textPlayerHP;
    [SerializeField] TextMeshProUGUI m_textPlayerGold;
    [SerializeField] TextMeshProUGUI m_textWave;
    [SerializeField] TextMeshProUGUI m_textEnemyCount;
    [SerializeField] PlayerHP m_playerHP;
    [SerializeField] PlayerGold m_playerGold;
    [SerializeField] WaveSystem m_waveSystem;
    [SerializeField] EnemySpawner m_enemySpawner;


    private void Update()
    {
        m_textPlayerHP.text = m_playerHP.CurrentHP + "/" + m_playerHP.MaxHP;
        m_textPlayerGold.text = m_playerGold.CurrentGold.ToString();
        m_textWave.text = m_waveSystem.CurrentWave + "/" + m_waveSystem.MaxWave;
        m_textEnemyCount.text = m_enemySpawner.CurrentEnemyCount + "/" + m_enemySpawner.MaxEnemyCount;
    }
}
