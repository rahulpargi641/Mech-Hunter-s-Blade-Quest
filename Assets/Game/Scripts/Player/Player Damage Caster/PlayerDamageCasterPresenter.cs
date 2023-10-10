using UnityEngine;

public class PlayerDamageCasterPresenter : DamageCasterPresenter
{
    protected override void OnTriggerEnter(Collider other)
    {
        EnemyHealthPresenter enemyhealth = other.GetComponent<EnemyHealthPresenter>();
        EnemyView enemyView = other.GetComponent<EnemyView>();
        EnemyVFX enemyVFX = other.GetComponent<EnemyVFX>();

        if (enemyhealth && enemyView)                        /*&& !model.damagedTargets.Contains(other)*/
        {
            enemyhealth.ApplyDamage(enemyView, model.Damage);
            EventService.Instance.InvokeOnEnemyHit(enemyView);
        }

        if (enemyVFX)
            PlayDamageVFXs(enemyVFX, transform.parent.position);

        base.OnTriggerEnter(other);
    }

    private void PlayDamageVFXs(EnemyVFX enemyVFX, Vector3 attackerPos = new Vector3())
    {
        enemyVFX.PlayHitVFX(attackerPos);
        enemyVFX.PlayHitSplashVFX();

        PlaySlashVFX();
    }

    private void PlaySlashVFX()
    {
        RaycastHit hit;
        bool isHit;
        DrawBox(out hit, out isHit);

        if (isHit)
            PlayerVFX.Instance.PlaySlashVFX(hit.point + new Vector3(0f, 0.5f, 0f));
    }
}
