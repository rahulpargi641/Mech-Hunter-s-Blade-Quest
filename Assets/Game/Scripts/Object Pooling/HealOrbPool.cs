using UnityEngine;

public class HealOrbPool : ObjectPoolGeneric<HealOrb>
{
    private HealOrb healOrb;

    public HealOrb GetHealOrb(HealOrb healOrb)
    {
        this.healOrb = healOrb;

        return GetItemFromPool();
    }

    protected override HealOrb CreateItem()
    {
        HealOrb healOrb = Object.Instantiate(this.healOrb);
        return healOrb;
    }
}
