using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedArea : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;

    private float _delay;
    private float _SoundChangeValue = 0.01f;
    private float _minVolumeValue = 0f;
    private float _maxVolumeValue = 1f;


    private void Start()
    {
        _audioSource.clip = _clip;

        _delay = 1 / 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ThiefMovement>() == false)
            return;

        _audioSource.volume = _minVolumeValue;
        _audioSource.Play();

        StartCoroutine(VolumeIncrease());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ThiefMovement>() == false)
            return;

        StartCoroutine(VolumeDecrease());
    }

    private IEnumerator VolumeIncrease()
    {
        while (_audioSource.volume < _maxVolumeValue)
        {
            yield return new WaitForSeconds(_delay);

            _audioSource.volume += _SoundChangeValue;
        }
    }

    private IEnumerator VolumeDecrease()
    {
        while (_audioSource.volume > _minVolumeValue)
        { 
            yield return new WaitForSeconds(_delay);

            _audioSource.volume -= _SoundChangeValue;
        }

        _audioSource.Stop();
    }
}
