using UnityEngine;
using UnityEngine.VFX;

public class VFXPool : ObjectPoolGeneric<VisualEffect>
{
    private VisualEffect vfxToSpawn;

    public VisualEffect GetVFX(VisualEffect vfxToSpawn)
    {
        this.vfxToSpawn = vfxToSpawn;

        return GetItemFromPool();
    }

    protected override VisualEffect CreateItem()
    {
        return Object.Instantiate(vfxToSpawn);
    }
}
