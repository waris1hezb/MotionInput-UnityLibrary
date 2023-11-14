public class HoverInteraction : BaseInteraction
{
    private void OnMouseEnter()
    {
        EnableAllEffects();
    }

    private void OnMouseExit()
    {
        DisableAllEffects();
    }
}
