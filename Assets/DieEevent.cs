using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEevent : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _audioClip;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.TryGetComponent<OnEnterHit>(out var Car))
            {
                _audioClip.Stop();
                _particle.Play();
                _audio.Play();
                Car.DiecheckFull();
            }
        }
    }
}
