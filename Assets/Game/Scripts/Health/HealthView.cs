using System;
using UnityEngine;

public class HealthView: MonoBehaviour
{
    protected void DamageBlinkEffect()
    {
        MaterialBlockView materialBlockView = GetComponent<MaterialBlockView>();
        materialBlockView.Blink();
    }
}
