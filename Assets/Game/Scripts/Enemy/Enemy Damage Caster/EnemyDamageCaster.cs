using UnityEngine;

public class EnemyDamageCaster : DamageCasterView
{
    protected override void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();

        if (playerView)
        {
            PlayerService.Instance.AddHitImpact(transform.parent.position, 10f);
            EventService.Instance.InvokePlayerHitAction();
            PlayerHealthService.Instance.ApplyDamage(model.Damage);
        }

        base.OnTriggerEnter(other);
    }
}
