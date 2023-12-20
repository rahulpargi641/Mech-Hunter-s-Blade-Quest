using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXService : MonoSingletonGeneric<VFXService>
{
    [SerializeField] VisualEffect oilSplashVFX;
    [SerializeField] VisualEffect healVFX;
    [SerializeField] ParticleSystem damageOrbHitVFX;

    private VFXPool healVFXPool = new VFXPool();
    private VFXPool hitOilSplashVFXPool = new VFXPool();

    private ParticlePool particlePool = new ParticlePool();

    public VisualEffect SpawnHitOilSplashVFX(Vector3 spawnPoint)
    {
        VisualEffect oilSplashVFX = hitOilSplashVFXPool.GetVFX(this.oilSplashVFX);

        oilSplashVFX.transform.position = spawnPoint;
        oilSplashVFX.gameObject.SetActive(true);

        return oilSplashVFX;
    }

    public VisualEffect SpawnHealVFX(Vector3 spawnPoint)
    {
        VisualEffect healVFX = healVFXPool.GetVFX(this.healVFX);

        healVFX.transform.position = spawnPoint;
        healVFX.gameObject.SetActive(true);

        return healVFX;
    }

    public ParticleSystem SpawnDamageOrbHitVFX(Vector3 spawnPoint)
    {
        ParticleSystem damageOrbHitVFX = particlePool.GetDamageOrbHitVFX(this.damageOrbHitVFX);

        damageOrbHitVFX.transform.position = spawnPoint;
        damageOrbHitVFX.gameObject.SetActive(true);

        return damageOrbHitVFX;
    }

    public void ReturnVFXToPool(VisualEffect vfx)
    {
        vfx.gameObject.SetActive(false);
        healVFXPool.ReturnItem(vfx);
    }

    public void ReturnVFXToPool(ParticleSystem vfx)
    {
        vfx.gameObject.SetActive(false);
        particlePool.ReturnItem(vfx);
    }
}
