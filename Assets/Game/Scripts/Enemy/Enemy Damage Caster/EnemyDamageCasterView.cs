using UnityEngine;

public class EnemyDamageCasterView : DamageCasterView
{
    protected override void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();

        if (playerView)
        {
            PlayerService.Instance.AddHitImpact(transform.parent.position, model.HitForce);
            EventService.Instance.InvokeOnPlayerHit();
            PlayerHealthService.Instance.ApplyDamage(model.Damage);
        }

        base.OnTriggerEnter(other);
    }
}
