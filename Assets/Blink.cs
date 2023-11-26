using EasyRoads3Dv3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private Material[] _material;

    private void Start()
    {
        BlinkStart();
    }

    public void BlinkStart()
    {
        StartCoroutine(BlinkMode());
    }

    IEnumerator BlinkMode()
    {
        while (true)
        {
            Debug.Log("yee");
            EmissionOn();
            yield return new WaitForSeconds(0.5f);
            EmissionOff();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void EmissionOn()
    {
        for (int i = 0; i < _material.Length; i++)
        {
            _material[i].EnableKeyword("_EMISSION");
        }
    }
    [ContextMenu("Wop")]
    public void EmissionOff()
    {
        for (int i = 0; i < _material.Length; i++)
        {
            _material[i].DisableKeyword("_EMISSION");

        }
    }
}
