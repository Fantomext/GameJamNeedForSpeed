using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterBoost : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Material _material;

    private void Start()
    {
        Blink();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.TryGetComponent<OnEnterHit>(out var hit))
            {
                hit.ChangeSpeed(value);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.TryGetComponent<OnEnterHit>(out var hit))
            {
                hit.ChangeSpeed(-value);
            }
        }
    }

    public void Blink()
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

    [ContextMenu("Wo")]
    public void EmissionOn()
    {
        _material.EnableKeyword("_EMISSION");
    }
    [ContextMenu("Wop")]
    public void EmissionOff()
    {
        _material.DisableKeyword("_EMISSION");
    }
}
