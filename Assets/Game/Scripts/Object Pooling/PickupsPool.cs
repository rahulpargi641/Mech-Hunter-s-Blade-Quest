using UnityEngine;

public class PickupsPool : ObjectPoolGeneric<PickupsController>
{
    private PickupsModel model;
    private PickupsView view;

    public PickupsController GetPickup(PickupsModel model, PickupsView view)
    {
        this.model = model;
        this.view = view;

        return GetItemFromPool();
    }

    protected override PickupsController CreateItem()
    {
        PickupsView view = Object.Instantiate(this.view);

        return new PickupsController(model, view);
    }
}
