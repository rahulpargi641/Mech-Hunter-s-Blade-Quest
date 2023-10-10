using UnityEngine;

public class PickupsService : MonoSingletonGeneric<PickupsService>
{
    [SerializeField] PickupsView pickupsView;

    private PickupsPool pickupsPool;

    private void Start()
    {
        pickupsPool = new PickupsPool();
    }

    public void SpawnPickup(Vector3 dropPos)
    {
        PickupsModel pickupsModel = new();
        PickupsController pickupsController = pickupsPool.GetPickup(pickupsModel, pickupsView);
        pickupsController.SetTransform(dropPos, Quaternion.identity);
        pickupsController.EnablePickup();
    }

    public void ReturnPickupToPool(PickupsController pickupsController)
    {
        pickupsController.DisablePickup();
        pickupsPool.ReturnItem(pickupsController);
    }
}
