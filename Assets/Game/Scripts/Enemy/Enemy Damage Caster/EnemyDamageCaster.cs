using UnityEngine;

public class EnemyDamageCaster : DamageCasterPresenter
{
    protected override void OnTriggerEnter(Collider other)
    {
        PlayerHealthPresenter playerHealth = other.GetComponent<PlayerHealthPresenter>();

        if (playerHealth)
        {
            playerHealth.ApplyDamage(model.Damage);

            EventService.Instance.InvokeOnPlayerHit();
            PlayerService.Instance.AddHitImpactForce(transform.parent.position, model.HitForce);
        }

        base.OnTriggerEnter(other);
    }
}
