using UnityEngine;

public class PickupsService : MonoSingletonGeneric<PickupsService>
{
    [SerializeField] HealOrb healOrbPrefab;
    // [SerializeField] Coin coin;

    private HealOrbPool healOrbPool = new HealOrbPool();

    public void SpawnHealOrb(Vector3 spawnPoint)
    {
        HealOrb healOrb = healOrbPool.GetHealOrb(healOrbPrefab);

        healOrb.SetTransform(spawnPoint, Quaternion.identity);
        healOrb.EnableHealOrb();
    }

    public void ReturnHealOrbToPool(HealOrb healOrb) 
    {
        healOrb.DisableHealOrb();
        healOrbPool.ReturnItem(healOrb);
    }
}
