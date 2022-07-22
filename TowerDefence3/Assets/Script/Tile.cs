using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isBuild { set;get; }

    private void Awake()
    {
        isBuild = false; 
    }
}
