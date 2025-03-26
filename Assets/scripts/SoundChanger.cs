using UnityEngine;
using System.Collections;

public class SoundChanger : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ClosedArea _closedArea;

    private float _SoundChangeValue = 0.5f;
    private float _minVolumeValue = 0f;
    private float _maxVolumeValue = 1f;

    private float _targetVolume;

    private Coroutine _volumeCoroutine;
    private WaitForSeconds _delay = new WaitForSeconds(1 / 10);

    private void Awake()
    {
        _audioSource.clip = _clip;
    }

    private void OnEnable()
    {
        _closedArea.Entered += Increase;
        _closedArea.Exited += Decrease;
    }

    private void OnDisable()
    {
        _closedArea.Entered -= Increase;
        _closedArea.Exited -= Decrease;
    }

    private void Increase(ClosedArea area)
    {
        _audioSource.Play();

        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(ChangeVolume(_maxVolumeValue));
    }

    private void Decrease(ClosedArea area)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(ChangeVolume(_minVolumeValue));
    }

    private IEnumerator ChangeVolume(float _targetVolume)
    {
        while (_audioSource.volume != _targetVolume)
        {
            yield return _delay;

            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _SoundChangeValue * Time.deltaTime);
        }

        if ((_audioSource.volume == _minVolumeValue) && (_targetVolume == _minVolumeValue))
            _audioSource.Stop();

        _volumeCoroutine = null; 
    }
}