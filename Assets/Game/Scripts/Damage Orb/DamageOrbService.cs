using UnityEngine;

public class DamageOrbService : MonoSingletonGeneric<DamageOrbService>
{
    [SerializeField] DamageOrbView damageOrbView;
    private DamageOrbPool damageOrbPool;

    void Start()
    {
        damageOrbPool = new DamageOrbPool();
    }

    public DamageOrbController CreateDamageOrb()
    {
        DamageOrbModel damageOrbModel = new();

        DamageOrbController damageOrbController = damageOrbPool.GetDamageOrb(damageOrbModel, damageOrbView);
        damageOrbController.EnableDamageOrb();

        return damageOrbController;
    }

    public void ReturnDamageOrbToPool(DamageOrbController damageOrbController)
    {
        damageOrbController.DisableDamageOrb();
        damageOrbPool.ReturnItem(damageOrbController);
    }
}
