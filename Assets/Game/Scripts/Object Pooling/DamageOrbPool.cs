using UnityEngine;

public class DamageOrbPool : ObjectPoolGeneric<DamageOrbController>
{
    //[SerializeField]
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
        DamageOrbView damageorbView = Instantiate(view);

        return new DamageOrbController(model, damageorbView);
    }
}
