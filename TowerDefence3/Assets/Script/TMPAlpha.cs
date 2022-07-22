using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPAlpha : MonoBehaviour
{
    [SerializeField] float m_lerpTime = 0.5f;
    TextMeshProUGUI m_text;

    private void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void FadeOut()
    {
        StartCoroutine(AlphaLerp(1, 0));
    }

    IEnumerator AlphaLerp(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime+=Time.deltaTime;
            percent = currentTime / m_lerpTime;

            Color color = m_text.color;
            color.a = Mathf.Lerp(start, end, percent);
            m_text.color = color;

            yield return null;
        }
    }
}
