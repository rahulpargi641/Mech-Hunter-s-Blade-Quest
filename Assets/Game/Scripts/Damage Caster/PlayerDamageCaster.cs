using UnityEngine;

public class PlayerDamageCaster : DamageCasterView
{
    protected override void OnTriggerEnter(Collider other)
    {
        EnemyHealthView enemyhealthView = other.GetComponent<EnemyHealthView>();
        EnemyView enemyView = other.GetComponent<EnemyView>();

        if (enemyhealthView && enemyView /*&& !model.damagedTargets.Contains(other)*/)
        {
            enemyhealthView.ApplyDamage(model.Damage, transform.parent.position);
            EventService.Instance.InvokeEnemyHitAction(enemyView);
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
