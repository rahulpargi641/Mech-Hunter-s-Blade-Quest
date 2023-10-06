using UnityEngine;
using UnityEngine.VFX;

public class VFXPool : ObjectPoolGeneric<VisualEffect>
{
    private VisualEffect vfxToSpawn;

    public VisualEffect GetHitSplashVFX(VisualEffect beingHitSplashVFX)
    {
        vfxToSpawn = beingHitSplashVFX;

        return GetItemFromPool();
    }

    public VisualEffect GetHealVFX(VisualEffect healVFX)
    {
        vfxToSpawn = healVFX;

        return GetItemFromPool();
    }

    protected override VisualEffect CreateItem()
    {
        return Object.Instantiate(vfxToSpawn);
    }
}
