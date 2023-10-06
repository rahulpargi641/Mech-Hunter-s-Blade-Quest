using UnityEngine;

public class DamageOrbService : MonoSingletonGeneric<DamageOrbService>
{
    [SerializeField] DamageOrbView damageOrbView;

    private DamageOrbPool damageOrbPool;

    void Start()
    {
        damageOrbPool = new DamageOrbPool();
    }

    public void CreateDamageOrb(Vector3 spawnPoint, Quaternion rotation)
    {
        DamageOrbModel damageOrbModel = new();

        DamageOrbController damageOrbController = damageOrbPool.GetDamageOrb(damageOrbModel, damageOrbView);
        damageOrbController.EnableDamageOrb();

        damageOrbController.SetDamageOrbTransform(spawnPoint, rotation);
    }

    public void ReturnDamageOrbToPool(DamageOrbController damageOrbController)
    {
        damageOrbController.DisableDamageOrb();
        damageOrbPool.ReturnItem(damageOrbController);
    }
}
