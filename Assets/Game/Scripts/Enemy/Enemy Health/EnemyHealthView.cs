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
        EnemyVFXView enemyVFXView = GetComponent<EnemyVFXView>();
        if (enemyVFXView)
        {
            enemyVFXView.PlayBeingHitVFX(attackerPos);
            enemyVFXView.PlayBeingHitSplashVFX();
        }
        DamageBlinkEffect();

        EnemyView enemyView = GetComponent<EnemyView>();
        Controller.ReduceHealth(enemyView, damage);
    }
}

