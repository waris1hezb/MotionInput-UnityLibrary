using UnityEngine;

[DisallowMultipleComponent]
public class AudioPlaybackEffect : BaseEffect
{
    [SerializeField] private AudioClip _clip;

    private void Awake()
    {
        if (_clip != null)
            return;

        _clip = Resources.Load("AudioPlaybackEffect_Fallback", typeof(AudioClip)) as AudioClip;
    }

    public override void DisableEffect()
    {
        // This method intentionally does nothing for this particular effect.
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_clip == null)
            return;

        AudioSource.PlayClipAtPoint(_clip, transform.position);
    }
}
