using UnityEngine;

public class PickupsController
{
    private PickupsModel model;
    private PickupsView view;

    public PickupsController(PickupsModel model, PickupsView view)
    {
        this.model = model;
        this.view = view;

        model.Controller = this;
        view.Controller = this;
    }

    public void SetTransform(Vector3 spawnPoint, Quaternion rotation)
    {
        view.transform.position = spawnPoint;
        view.transform.rotation = rotation;
    }

    public void ProcessHeal(PlayerHealthPresenter playerHealth)
    {
        playerHealth.AddHealth(model.HealOrbGain);
        VFXService.Instance.SpawnHealVFX(view.transform.position);

        PickupsService.Instance.ReturnPickupToPool(this);
    }

    public void EnablePickup()
    {
        view.gameObject.SetActive(true);
    }

    public void DisablePickup()
    {
        view.gameObject.SetActive(false);
    }
}
