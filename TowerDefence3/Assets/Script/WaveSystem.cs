using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] Wave[] m_waves;
    [SerializeField] EnemySpawner m_enemySpawner;
    [SerializeField] GameObject waveText;
    int m_currentWaveIndex = -1;

    public int CurrentWave => m_currentWaveIndex+1;
    public int MaxWave => m_waves.Length;

    private void Awake()
    {
        SetWaveInfo();
    }
    // Resources폴더에 파일이 있을 때
    /*void SetWaveInfo()
    {
        List<string> waveInfo = new List<string>();
        int waveCount = 0;
        TextAsset textAsset = Resources.Load("WaveInfo") as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            waveInfo.Add(line);

            if (line == null) break;
            waveCount++;
        }
        m_waves = new Wave[waveCount];
        for (int i = 0; i < waveCount; i++)
        {
            m_waves[i].enemyCount = int.Parse(waveInfo[i]);
        }
        stringReader.Close();
    }*/

    // StreamingAssets폴더에 파일이 있을 때
    void SetWaveInfo()
    {
        List<string> waveInfo = new List<string>();
        int waveCount = 0;

/*        string streamingAssetsDirectory = "jar:file://" + Application.dataPath + "!/assets/";
        string filePath = streamingAssetsDirectory + "WaveInfo";*/
        string filePath = $"{Application.streamingAssetsPath}/WaveInfo.csv";

        StreamReader sr = new StreamReader(filePath);
        while (sr != null)
        {
            string line = sr.ReadLine();

            waveInfo.Add(line);

            if (line == null) break;
            waveCount++;
        }
        m_waves = new Wave[waveCount];
        for (int i = 0; i < waveCount; i++)
        {
            m_waves[i].enemyCount = int.Parse(waveInfo[i]);
        }
        sr.Close();
    }
    public void StartWave()
    {
        if(m_enemySpawner.EnemyList.Count==0 && m_currentWaveIndex < m_waves.Length - 1)
        {
            m_currentWaveIndex++;

            waveText.SetActive(true);
            waveText.GetComponent<TextMeshProUGUI>().text = "Wave" + (m_currentWaveIndex + 1);

            m_enemySpawner.StartWave(m_waves[m_currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public int enemyCount;
}

[System.Serializable]
public struct Spawn
{
    public string type;
    public float m_spawnTime;
}