using UnityEngine;

public class DamageOrbController
{
    private DamageOrbModel model;
    private DamageOrbView view;

    public DamageOrbController(DamageOrbModel model, DamageOrbView view)
    {
        this.model = model;
        this.view = view;

        this.model.Controller = this;
        this.view.Controller = this;
    }

    public void MoveOrbForward()
    {
        view.RigidBody.MovePosition(view.transform.position + view.transform.forward * model.Speed * Time.deltaTime);
    }

    public void ProcessHit(PlayerHealthPresenter playerHealth)
    {
        EventService.Instance.InvokeOnPlayerHit();
        PlayerService.Instance.AddHitImpactForce(view.transform.position, model.HitForce);
        playerHealth.ApplyDamage(model.Damage);

        DamageOrbService.Instance.ReturnDamageOrbToPool(this);
    }

    public void SetDamageOrbTransform(Vector3 shootingPointPosition, Quaternion shootingPointRotation)
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
