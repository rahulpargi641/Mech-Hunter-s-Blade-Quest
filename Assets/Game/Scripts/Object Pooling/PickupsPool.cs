using UnityEngine;

public class PickupsPool : ObjectPoolGeneric<PickupsView>
{
    [SerializeField] PickupsView pickupPrefab;

    public PickupsView GetPickup()
    {
        return GetItemFromPool();
    }

    protected override PickupsView CreateItem()
    {
        return Instantiate(pickupPrefab);
    }
}
