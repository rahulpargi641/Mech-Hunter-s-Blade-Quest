using UnityEngine;

public class CollectibleService : MonoSingletonGeneric<CollectibleService>
{
    [SerializeField] GameObject itemToDrop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DropItem(Vector3 dropPos)
    {
        Instantiate(itemToDrop, dropPos, Quaternion.identity);
    }
}
