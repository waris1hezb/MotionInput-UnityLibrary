using UnityEngine;

public class DoubleClickInteraction : BaseInteraction
{
    private float _doubleClickStart = 0;

    void OnMouseUp()
    {
        if ((Time.time - _doubleClickStart) < 0.3f)
        {
            this.OnDoubleClick();
            _doubleClickStart = -1;
        }
        else
        {
            _doubleClickStart = Time.time;
        }
    }

    void OnDoubleClick()
    {
        EnableAllEffects(temp: true);
    }
}
