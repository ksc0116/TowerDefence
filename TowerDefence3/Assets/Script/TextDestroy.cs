using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("DisableObject");
    }

    public IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
