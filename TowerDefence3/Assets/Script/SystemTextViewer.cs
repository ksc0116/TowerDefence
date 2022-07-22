using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SystemType {  Money =0,Build}

public class SystemTextViewer : MonoBehaviour
{
    TextMeshProUGUI m_textSystem;
    TMPAlpha m_tmpAlpha;

    private void Awake()
    {
        m_textSystem = GetComponent<TextMeshProUGUI>();
        m_tmpAlpha = GetComponent<TMPAlpha>();
    }

    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Money:
                m_textSystem.text = "System : Not enough money...";
                break;
            case SystemType.Build:
                m_textSystem.text = "System : Invalid build tower...";
                break;
        }

        m_tmpAlpha.FadeOut();
    }
}
