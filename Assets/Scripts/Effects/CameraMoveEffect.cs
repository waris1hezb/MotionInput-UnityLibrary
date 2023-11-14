using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraMoveEffect : MonoBehaviour
{
    [SerializeField] private Volume _postProcessVolume;
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _intensity = 0.5f;
    [SerializeField] private float _duration = 0.2f;

    private Vignette _vignette;

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
                _postProcessVolume.profile.TryGet(out _vignette);
            }
        }

        _postProcessVolume.profile.TryGet(out _vignette);

        DisableEffect();
    }

    public void DisableEffect()
    {
        if (_vignette == null)
            return;

        StartCoroutine(LerpIntensity(0));
    }

    public void EnableEffect()
    {
        if (_vignette == null)
            return;

        StartCoroutine(LerpIntensity(_intensity));
    }

    private IEnumerator LerpIntensity(float targetIntensity)
    {
        float startIntensity = _vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            _vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _vignette.intensity.value = targetIntensity;
    }
}
