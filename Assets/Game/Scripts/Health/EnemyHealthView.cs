using UnityEngine;

public class EnemyHealthView : HealthView
{
    public override void ApplyDamage(int damage, Vector3 attackerPos = default)
    {
        base.ApplyDamage(damage, attackerPos);
       
        EnemyVFXView enemyVFXView = GetComponent<EnemyVFXView>();
        if(enemyVFXView)
        {
            enemyVFXView.PlayBeingHitVFX(attackerPos);
            enemyVFXView.PlayBeingHitSplashVFX();
        }

        DamageBlink();
    }
}

