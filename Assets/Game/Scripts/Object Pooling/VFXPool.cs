using UnityEngine;
using UnityEngine.VFX;

public class VFXPool : ObjectPoolGeneric<VisualEffect>
{
    [SerializeField] VisualEffect beingHitSplashVFX;

    public VisualEffect GetHitSplashVFX()
    {
        return GetItemFromPool();
    }

    protected override VisualEffect CreateItem()
    {
        return Instantiate(beingHitSplashVFX);
    }
}
