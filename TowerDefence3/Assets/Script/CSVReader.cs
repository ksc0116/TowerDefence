using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset textAsset;
    StringReader stringReader;
    void Start()
    {
        textAsset= Resources.Load("EnemyData") as TextAsset;    
        stringReader = new StringReader(textAsset.text);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            FileRead();
        }
    }

    void FileRead()
    {
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;
        }
        stringReader.Close();
    }

}
