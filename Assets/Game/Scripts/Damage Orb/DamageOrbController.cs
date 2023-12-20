using UnityEngine;

public class DamageOrbController
{
    private DamageOrbModel model;
    private DamageOrbView view;

    public DamageOrbController(DamageOrbModel model, DamageOrbView view)
    {
        this.model = model;
        this.view = view;

        this.view.Controller = this;
    }

    public void MoveOrbForward()
    {
        view.RigidBody.MovePosition(view.transform.position + view.transform.forward * model.Speed * Time.deltaTime);
    }

    public void ProcessHit()
    {
        EventService.Instance.InvokeOnPlayerHit(view.transform.position, model.HitForce, model.Damage);
        DamageOrbService.Instance.ReturnDamageOrbToPool(this);
    }

    public void SetTransform(Vector3 shootingPointPosition, Quaternion shootingPointRotation)
    {
        view.transform.position = shootingPointPosition;
        view.transform.rotation = shootingPointRotation;
    }

    public void EnableDamageOrb()
    {
        view.gameObject.SetActive(true);
    }

    public void DisableDamageOrb()
    {
        view.gameObject.SetActive(false);
    }
}
