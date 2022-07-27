using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject m_enemyHPBarPrefab;
    [SerializeField] Transform m_canvasTransform;
    [SerializeField] Transform[] m_wayPoints;
    [SerializeField] PlayerHP m_playerHP;
    [SerializeField] PlayerGold m_playerGold;

    [Header("EnemyPrefab")]
    [SerializeField] GameObject enemy01;
    [SerializeField] GameObject enemy02;
    [SerializeField] GameObject enemy03;
    [SerializeField] GameObject enemy04;
    GameObject tempEnemy;


    Wave m_currentWave;
    int m_currentEnemyCount;
    List<Enemy> enemyList;
    List<Spawn> spawnList;
    int curWaveIndex = 0;

    [SerializeField] EnemyMemoryPool enemyMemoryPool;
    [SerializeField] GameObject clearText;

    public List<Enemy> EnemyList => enemyList;

    public int CurrentEnemyCount => m_currentEnemyCount;
    public int MaxEnemyCount => m_currentWave.enemyCount;

    private void Awake()
    {
        enemyList=new List<Enemy>();
        spawnList= new List<Spawn>();
    }

    void SetSpawnData()
    {
        TextAsset textAsset = Resources.Load("Wave"+ curWaveIndex) as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null) break;

            string[] parts = line.Split(',');

            Spawn spawn = new Spawn();
            spawn.type = parts[0];
            spawn.m_spawnTime = float.Parse( parts[1] );

            spawnList.Add(spawn);
        }
        stringReader.Close();
        
        curWaveIndex++;
    }
    public void StartWave(Wave p_wave)
    {
        m_currentWave=p_wave;

        m_currentEnemyCount = m_currentWave.enemyCount;

        StartCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        SetSpawnData();

        int spawnEnemyCount = 0;

        while (spawnEnemyCount < m_currentWave.enemyCount)
        {
            switch (spawnList[spawnEnemyCount].type)
            {
                case "1":
                    tempEnemy = enemy01;
                    break;
               case "2":
                    tempEnemy = enemy02;
                    break;
                case "3":
                    tempEnemy = enemy03;
                    break;
                case "4":
                    tempEnemy = enemy04;
                    break;
            }
            GameObject enemyClone = enemyMemoryPool.SpawnEnemy(spawnList[spawnEnemyCount].type);
            Enemy enemyLogic = enemyClone.GetComponent<Enemy>();

            enemyLogic.Init(this,m_wayPoints);
            enemyList.Add(enemyLogic);

            SpawnEnemyHPBar(enemyClone);

            yield return new WaitForSeconds(spawnList[spawnEnemyCount].m_spawnTime);

            spawnEnemyCount++;  
        }
        spawnList.Clear();
    }
    void SpawnEnemyHPBar(GameObject p_enemy)
    {
        GameObject sliderClone = enemyMemoryPool.SpawnHPBar(); 
        sliderClone.transform.SetParent(m_canvasTransform);

        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderAutoPosition>().Init(p_enemy.transform,enemyMemoryPool);

        sliderClone.GetComponent<EnemyHPViewer>().Init(p_enemy.GetComponent<EnemyHP>());
    }

    public void DestroyEnemy(EnemyDestroyType p_type,Enemy enemy, int p_gold)
    {
        if (p_type == EnemyDestroyType.Arrive)
        {
            m_playerHP.TakeDamage(1);
        }
        else if(p_type == EnemyDestroyType.Kill)
        {
            m_playerGold.CurrentGold += p_gold;
        }

        m_currentEnemyCount--;

        if (m_currentEnemyCount==0)
        {
            clearText.SetActive(true); 
        }

        enemyList.Remove(enemy);

        enemyMemoryPool.DestroyEnemy(enemy.gameObject);
    }
}
