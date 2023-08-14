using UnityEngine;

public class EnemyHealthView : HealthView
{
    public EnemyHealthController Controller { get; set; }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        EnemyVFXView enemyVFXView = GetComponent<EnemyVFXView>();
        if (enemyVFXView)
        {
            enemyVFXView.PlayBeingHitVFX(attackerPos);
            enemyVFXView.PlayBeingHitSplashVFX();
        }
        DamageBlinkEffect();

        Controller.ReduceHealth(damage);
    }
}

