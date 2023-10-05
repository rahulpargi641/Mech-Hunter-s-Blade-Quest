using UnityEngine;

public class PickupsService : MonoSingletonGeneric<PickupsService>
{
    [SerializeField] GameObject itemToDrop;

    public void DropItem(Vector3 dropPos)
    {
        Instantiate(itemToDrop, dropPos, Quaternion.identity);
    }
}
