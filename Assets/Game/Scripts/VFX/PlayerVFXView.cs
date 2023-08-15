using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXView : MonoBehaviour
{
    [SerializeField] VisualEffect footStep;
    [SerializeField] ParticleSystem blade01;
    [SerializeField] VisualEffect slash;
    [SerializeField] VisualEffect heal;

    public void UpdateFootStep(bool state)
    {
        if (state)
            footStep.Play();
        else
            footStep.Stop();
    }

    public void PlayBlade01()
    {
        blade01.Play();
    }

    public void PlaySlash(Vector3 pos)
    {
        slash.transform.position = pos;
        slash.Play();
    }

    public void PlayHealVFX()
    {
        heal.Play();
    }
}
