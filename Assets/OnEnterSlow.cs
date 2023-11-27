using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterSlow : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Material _material;
    [SerializeField] private AudioSource[] _audioSource;
    [SerializeField] private float _timePlayBoost;

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
                hit.ChangeSpeed(-value);
                StartCoroutine(PlaySlow());
            }
        }
    }

    IEnumerator PlaySlow()
    {
        _audioSource[0].Play();
        yield return new WaitForSeconds(_timePlayBoost);
        _audioSource[0].Stop();
        _audioSource[1].Play();
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
