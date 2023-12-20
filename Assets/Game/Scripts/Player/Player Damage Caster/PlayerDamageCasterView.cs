using UnityEngine;

public class PlayerDamageCasterView : DamageCasterView
{
    [SerializeField] PlayerSO playerSO;

    protected override void Awake()
    {
        base.Awake();
        model = new DamageCasterModel(playerSO.hitForce, playerSO.damage);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        ProcessEnemyCollision(other);
    }

    private void ProcessEnemyCollision(Collider other)
    {
        EnemyView enemyView = other.GetComponent<EnemyView>();
        EnemyVFX enemyVFX = other.GetComponent<EnemyVFX>();

        if (enemyView)
            EventService.Instance.InvokeOnEnemyHit(enemyView, model.Damage);

        if (enemyVFX)
            PlayEnemyDamageVFXs(enemyVFX, transform.parent.position);
    }

    private void PlayEnemyDamageVFXs(EnemyVFX enemyVFX, Vector3 attackerPos = new Vector3())
    {
        enemyVFX.PlayEnemyHitVFX(attackerPos);
        enemyVFX.PlayEnemyOilSplashVFX();

        PlayEnemySlashedVFX(enemyVFX);
    }

    private void PlayEnemySlashedVFX(EnemyVFX enemyVFX)
    {
        RaycastHit hit;
        bool isHit;
        DrawCollisionBox(out hit, out isHit);

        if (isHit)
            enemyVFX.PlaySlashVFX(hit.point + new Vector3(0f, 0.5f, 0f));
    }
}
