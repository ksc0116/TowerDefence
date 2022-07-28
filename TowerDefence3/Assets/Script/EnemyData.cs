using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class EnemyData : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private void Awake()
    {
        DataLoad();
       
    }
    // Resources폴더에 파일이 있을 때
    /*    void DataLoad()
        {
            TextAsset textAsset = Resources.Load("EnemyData") as TextAsset;
            StringReader stringReader = new StringReader(textAsset.text);

            int index = 0;

            while (stringReader != null)
            {
                string line = stringReader.ReadLine();
                if (line == null) break;

                string[] data = line.Split(',');

                enemies[index].GetComponent<Enemy>().Gold = int.Parse(data[0]);
                enemies[index].GetComponent<Movement>().MoveSpeed = float.Parse(data[1]);
                enemies[index].GetComponent<EnemyHP>().MaxHP = float.Parse(data[2]);
                index++;
            }

            stringReader.Close();
        }*/
    // StreamingAssets폴더에 파일이 있을 때
    void DataLoad()
    {
        //string streamingAssetsDirectory = "jar:file://" + Application.dataPath + "!/assets/";
        //string filePath = streamingAssetsDirectory + "EnemyData";

        string filePath = $"{Application.streamingAssetsPath}/EnemyData.csv";

        StreamReader sr = new StreamReader(filePath);
        int index = 0;
        while (sr != null)
        {
            string line = sr.ReadLine();
            if (line == null) break;

            string[] data = line.Split(',');

            enemies[index].GetComponent<Enemy>().Gold = int.Parse(data[0]);
            enemies[index].GetComponent<Movement>().MoveSpeed = float.Parse(data[1]);
            enemies[index].GetComponent<EnemyHP>().MaxHP = float.Parse(data[2]);
            index++;
        }
        sr.Close();
    }
}
