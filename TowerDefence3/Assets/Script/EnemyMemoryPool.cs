using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject enemy01;
    [SerializeField] GameObject enemy02;
    [SerializeField] GameObject enemy03;
    [SerializeField] GameObject enemy04;
    [SerializeField] GameObject hpSlider;

    MemoryPool enemy01Pool;
    MemoryPool enemy02Pool;
    MemoryPool enemy03Pool;
    MemoryPool enemy04Pool;
    MemoryPool hpSliderPool;

    private void Awake()
    {
        enemy01Pool = new MemoryPool(enemy01);
        enemy02Pool = new MemoryPool(enemy02);
        enemy03Pool = new MemoryPool(enemy03);
        enemy04Pool = new MemoryPool(enemy04);
        hpSliderPool = new MemoryPool(hpSlider);
    }

    public GameObject SpawnHPBar()
    {
        return hpSliderPool.ActivePoolItem();
    }
    public void DestroyHPBar(GameObject hpBar)
    {
        hpSliderPool.DeactivePoolItem(hpBar);
    }
    public GameObject SpawnEnemy(string type)
    {
        GameObject tempEnemy = null;
        switch (type)
        {
            case "1":
                tempEnemy = enemy01Pool.ActivePoolItem();
                break;
            case "2":
                tempEnemy = enemy02Pool.ActivePoolItem();
                break;
            case "3":
                tempEnemy = enemy03Pool.ActivePoolItem();
                break;
            case "4":
                tempEnemy = enemy04Pool.ActivePoolItem();
                break;
        }
        return tempEnemy;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        string type = enemy.GetComponent<Enemy>().type;
        switch (type)
        {
            case "1":
                enemy01Pool.DeactivePoolItem(enemy);
                break;
            case "2":
                enemy02Pool.DeactivePoolItem(enemy);
                break;
            case "3":
                enemy03Pool.DeactivePoolItem(enemy);
                break;
            case "4":
                enemy04Pool.DeactivePoolItem(enemy);
                break;
        }
    }
}
