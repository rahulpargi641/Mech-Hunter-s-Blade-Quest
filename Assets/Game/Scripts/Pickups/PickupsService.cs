using UnityEngine;

public class PickupsService : MonoSingletonGeneric<PickupsService>
{
    [SerializeField] PickupsView pickupsView;

    private PickupsPool pickupsPool;

    private void Start()
    {
        pickupsPool = new PickupsPool();
    }

    public PickupsController CreatePickup(Vector3 dropPos)
    {
        //Instantiate(itemToDrop, dropPos, Quaternion.identity);
        PickupsModel pickupsModel = new();
        PickupsController pickupsController = pickupsPool.GetPickup(pickupsModel, pickupsView);
        pickupsController.SetTransform(dropPos, Quaternion.identity);
        //pickupsController.gameObject.SetActive(true);
        //pickupsController.transform.position = dropPos;
        //pickupsController.transform.rotation = Quaternion.identity;
        //return pickup;
        pickupsController.EnablePickup();
        return pickupsController;
    }

    public void ReturnPickupToPool(PickupsController pickupsController)
    {
        pickupsController.DisablePickup();
        pickupsPool.ReturnItem(pickupsController);
    }
}
