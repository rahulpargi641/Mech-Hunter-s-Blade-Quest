using UnityEngine;

public class HealthView: MonoBehaviour
{
    protected void PlayDamageBlinkEffect()
    {
        MaterialBlockView materialBlockView = GetComponent<MaterialBlockView>();
        materialBlockView.CharacterBlink();
    }
}
