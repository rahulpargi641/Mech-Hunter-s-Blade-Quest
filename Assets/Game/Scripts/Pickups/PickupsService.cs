using UnityEngine;

public class PickupsService : MonoSingletonGeneric<PickupsService>
{
    private PickupsPool pickupsPool;

    private void Start()
    {
        pickupsPool = GetComponent<PickupsPool>();
    }

    public PickupsView DropItem(Vector3 dropPos)
    {
        //Instantiate(itemToDrop, dropPos, Quaternion.identity);
        PickupsView pickup = pickupsPool.GetPickup();
        pickup.gameObject.SetActive(true);
        pickup.transform.position = dropPos;
        pickup.transform.rotation = Quaternion.identity;
        return pickup;
    }

    public void ReturnPickupToPool(PickupsView pickView)
    {
        pickView.gameObject.SetActive(false);
        pickupsPool.ReturnItem(pickView);
    }
}
