using System.Collections;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour, IEffect
{
    public virtual void DisableEffect()
    {
        throw new System.NotImplementedException();
    }

    public virtual void EnableEffect(bool temp = false)
    {
        if (temp)
            StartCoroutine(TempEnable());
    }

    private IEnumerator TempEnable()
    {
        yield return new WaitForSeconds(0.5f);
        DisableEffect();
    }
}
