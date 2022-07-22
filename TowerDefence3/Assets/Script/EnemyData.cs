using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyData : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private void Awake()
    {
        DataLoad();
       
    }
    void DataLoad()
    {
        TextAsset textAsset = Resources.Load("EnemyData") as TextAsset; 
        StringReader stringReader = new StringReader(textAsset.text);

        int index=0;

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null) break;

            string[] data = line.Split(',');

            enemies[index].GetComponent<Enemy>().Gold = int.Parse( data[0] );
            enemies[index].GetComponent<Movement>().MoveSpeed = float.Parse( data[1] );
            enemies[index].GetComponent<EnemyHP>().MaxHP = float.Parse(data[2]);
            index++;
        }
        
        stringReader.Close();
    }
}
