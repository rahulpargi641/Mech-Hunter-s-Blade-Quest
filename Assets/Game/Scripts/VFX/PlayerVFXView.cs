using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXView : MonoBehaviour
{
    [SerializeField] ParticleSystem blade01;
    [SerializeField] ParticleSystem blade02;
    [SerializeField] ParticleSystem blade03;
    [SerializeField] VisualEffect slash;
    [SerializeField] VisualEffect heal;
    [SerializeField] VisualEffect footStep;

    // called via run animation event
    public void UpdateFootStep(bool state)
    {
        if (state)
            footStep.Play();
        else
            footStep.Stop();
    }

    // called via attack01 animation event
    public void PlayBlade01()
    {
        blade01.Play();
    }

    // called via attack02 animation event
    public void PlayBlade02()
    {
        blade02.Play();
    }

    // called via attack03 animation event
    public void PlayBlade03()
    {
        blade03.Play();
    }

    public void StopBlade()
    {
        blade01.Simulate(0);
        blade01.Stop();

        blade02.Simulate(0);
        blade02.Stop();

        blade03.Simulate(0);
        blade03.Stop();
    }

    // called via attack01 animation event
    public void PlaySlashVFX(Vector3 pos)
    {
        slash.transform.position = pos;
        slash.Play();
    }

    // called when player pickups healing orb
    public void PlayHealVFX()
    {
        heal.Play();
    }
}
