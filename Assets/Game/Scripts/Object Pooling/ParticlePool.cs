using UnityEngine;

public class ParticlePool : ObjectPoolGeneric<ParticleSystem>
{
    private ParticleSystem particleVFX;

    public ParticleSystem GetDamageOrbHitVFX(ParticleSystem beingHitSplashVFX)
    {
        particleVFX = beingHitSplashVFX;

        return GetItemFromPool();
    }

    protected override ParticleSystem CreateItem()
    {
        return Object.Instantiate(particleVFX);
    }
}
