using UnityEngine;

public class EnemyDamageCaster : DamageCasterView
{
    protected override void OnTriggerEnter(Collider other)
    {
        PlayerHealthView playerhealthView = other.GetComponent<PlayerHealthView>();

        if (playerhealthView && !model.damagedTargets.Contains(other))
        {
            playerhealthView.ApplyDamage(model.Damage, transform.parent.position);
        }

        base.OnTriggerEnter(other);
    }
}
