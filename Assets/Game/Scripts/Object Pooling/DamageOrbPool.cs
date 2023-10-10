using UnityEngine;

public class DamageOrbPool : ObjectPoolGeneric<DamageOrbController>
{
    private DamageOrbModel model;
    private DamageOrbView view;
    
    public DamageOrbController GetDamageOrb(DamageOrbModel model, DamageOrbView view)
    {
        this.model = model;
        this.view = view;

        return GetItemFromPool();
    }

    protected override DamageOrbController CreateItem()
    {
        DamageOrbView view = Object.Instantiate(this.view);

        return new DamageOrbController(model, view);
    }
}
