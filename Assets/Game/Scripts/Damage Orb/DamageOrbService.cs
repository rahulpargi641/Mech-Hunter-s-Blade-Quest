using UnityEngine;

public class DamageOrbService : MonoSingletonGeneric<DamageOrbService>
{
    [SerializeField] DamageOrbSO damageOrbSO;
    [SerializeField] DamageOrbView damageOrbView;

    private DamageOrbPool damageOrbPool = new DamageOrbPool();

    public void SpawnDamageOrb(Vector3 spawnPoint, Quaternion rotation)
    {
        DamageOrbModel damageOrbModel = new(damageOrbSO);
        DamageOrbController damageOrbController = damageOrbPool.GetDamageOrb(damageOrbModel, damageOrbView);

        damageOrbController.SetTransform(spawnPoint, rotation);
        damageOrbController.EnableDamageOrb();
    }

    public void ReturnDamageOrbToPool(DamageOrbController damageOrbController)
    {
        damageOrbController.DisableDamageOrb();
        damageOrbPool.ReturnItem(damageOrbController);
    }
}
