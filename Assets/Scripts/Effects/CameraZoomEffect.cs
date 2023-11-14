using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraZoomEffect : MonoBehaviour
{
    [SerializeField] private Volume _postProcessVolume;
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _intensity = 0.5f;
    [SerializeField] private float _duration = 0.2f;

    private LensDistortion _lensDistortion;

    private void Awake()
    {
        if (_postProcessVolume == null)
        {
            _postProcessVolume = FindObjectOfType<Volume>();

            if (_postProcessVolume == null)
            {
                Debug.LogError("No Post processing Volume found");
            }
            else
            {
                _postProcessVolume.profile.TryGet(out _lensDistortion);
            }
        }

        _postProcessVolume.profile.TryGet(out _lensDistortion);

        DisableEffect();
    }

    public void EnableEffect(int direction)
    {
        if (_lensDistortion == null)
            return;

        StartCoroutine(LerpIntensity(direction * _intensity));
    }

    public void DisableEffect()
    {
        if (_lensDistortion == null)
            return;

        StartCoroutine(LerpIntensity(0));
    }

    private IEnumerator LerpIntensity(float targetIntensity)
    {
        float startIntensity = _lensDistortion.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            _lensDistortion.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _lensDistortion.intensity.value = targetIntensity;
    }
}
