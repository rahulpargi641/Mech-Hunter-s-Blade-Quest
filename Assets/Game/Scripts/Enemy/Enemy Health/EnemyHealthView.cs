using UnityEngine;

public class EnemyHealthView : HealthView
{
    public EnemyHealthController Controller { get; set; }

    private void Start()
    {
        HealthModel healthModel = new HealthModel();
        Controller = new EnemyHealthController(healthModel, this);
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        EnemyVFX enemyVFXView = GetComponent<EnemyVFX>();
        if (enemyVFXView)
        {
            enemyVFXView.PlayBeingHitVFX(attackerPos);
            enemyVFXView.PlayBeingHitSplashVFX();
        }

        PlayDamageBlinkEffect();

        EnemyView enemyView = GetComponent<EnemyView>();
        Controller.ReduceHealth(enemyView, damage);
    }
}

