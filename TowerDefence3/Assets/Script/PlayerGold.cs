using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] int m_currentGold = 100;

    public int CurrentGold
    {
        set => m_currentGold = Mathf.Max(0, value);
        get => m_currentGold;
    }
}
