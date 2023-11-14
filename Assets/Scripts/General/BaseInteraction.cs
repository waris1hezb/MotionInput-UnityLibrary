using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour
{
    public bool handpickEffects = false;

    [Tooltip("Effects added to this array will only take effect if handpickEffects is set to True")]
    [SerializeField] private BaseEffect[] _handpickedEffects;

    private IEffect[] _effects;

    protected virtual void Awake()
    {
        if (!handpickEffects)
            _effects = GetComponents<IEffect>();
    }

    protected void EnableAllEffects(bool temp = false)
    {
        if (!handpickEffects)
        {
            foreach (IEffect effect in _effects)
            {
                effect.EnableEffect(temp);
            }
        }
        else
        {
            foreach (BaseEffect effect in _handpickedEffects)
            {
                effect.EnableEffect(temp);
            }
        }
    }

    protected void DisableAllEffects()
    {
        if (!handpickEffects)
        {
            foreach (IEffect effect in _effects)
            {
                effect.DisableEffect();
            }
        }
        else
        {
            foreach (BaseEffect effect in _handpickedEffects)
            {
                effect.DisableEffect();
            }
        }
    }
}
