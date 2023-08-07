using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXView : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;

    public void UpdateFootStep(bool state)
    {
        if (state)
            footStep.Play();
        else
            footStep.Stop();
    }
}
