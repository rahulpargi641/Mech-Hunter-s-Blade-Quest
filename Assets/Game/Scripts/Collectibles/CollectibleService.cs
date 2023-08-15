using UnityEngine;

public class CollectibleService : MonoSingletonGeneric<CollectibleService>
{
    [SerializeField] GameObject itemToDrop;

    public void DropItem(Vector3 dropPos)
    {
        Instantiate(itemToDrop, dropPos, Quaternion.identity);
    }
}
