using UnityEngine;
using System.Collections;
using System;

public class SoundChanger : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ClosedArea _closedArea;

    private float _SoundChangeValue = 0.1f;
    private float _minVolumeValue = 0f;
    private float _maxVolumeValue = 1f;

    private WaitForSeconds _delay;

    private float _second = 1f;
    private float _timeDivider = 10f;

    private void Start()
    {
        _delay = new WaitForSeconds(_second / _timeDivider);

        _audioSource.clip = _clip;

        _closedArea.Entered += Increase;
        _closedArea.Exited += Decrease;
    }

    private void Increase(ClosedArea area)
    {
        _audioSource.Play();
        _audioSource.volume = _minVolumeValue;

        StartCoroutine(ChangeVolume(_maxVolumeValue));
    }

    private void Decrease(ClosedArea area)
    {
        StartCoroutine(ChangeVolume(_minVolumeValue));
    }

    private IEnumerator ChangeVolume(float goal)
    {
        while (_audioSource.volume != goal)
        {
            yield return _delay;

            if (goal == _maxVolumeValue)
                _audioSource.volume += _SoundChangeValue;
            else
                _audioSource.volume -= _SoundChangeValue;
        }

        if ((_audioSource.volume == _minVolumeValue) && (goal == _minVolumeValue))
            _audioSource.Stop();
    }
}
