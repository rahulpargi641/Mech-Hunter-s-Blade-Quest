using UnityEngine;

public class PlayerDamageCaster : DamageCasterView
{
    protected override void OnTriggerEnter(Collider other)
    {
        EnemyHealthView enemyhealthView = other.GetComponent<EnemyHealthView>();

        if (enemyhealthView /*&& !model.damagedTargets.Contains(other)*/)
        {
            enemyhealthView.ApplyDamage(model.Damage, transform.parent.position);
            PlayerVFXView playerVFXView = transform.parent.GetComponent<PlayerVFXView>();
            if (playerVFXView)
            {
                RaycastHit hit;
                bool isHit;
                DrawBox(out hit, out isHit);

                if (isHit)
                    playerVFXView.PlaySlashVFX(hit.point + new Vector3(0f, 0.5f, 0f));
            }

        }
        base.OnTriggerEnter(other);
    }
}
