using System;
using UnityEngine;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action<float> OnPlayerHealthChange;
    public event Action onPlayerDeath;
    public event Action<Vector3, float, int> onPlayerHit;

    public event Action<int> onHealPickup;

    public event Action<EnemyView, int> onEnemyHit;
    public event Action<EnemyView> onEnemyDeath;
    public event Action<EnemyView> onEnemyDissolved;

    public event Action onAllEnemiesDead;
    public event Action onCurrentEnemyGroupDead;

    public void InvokeOnPlayerHealthChange(float currentHealthPercent)
    {
        OnPlayerHealthChange?.Invoke(currentHealthPercent);
    }

    public void InvokeOnPlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }

    public void InvokeOnPlayerHit(Vector3 hitPoint, float hitForce, int damage)
    {
        onPlayerHit?.Invoke(hitPoint, hitForce, damage);
    }

    public void InvokeOnHealPickup(int healthGain)
    {
        onHealPickup?.Invoke(healthGain);
    }

    public void InvokeOnEnemyHit(EnemyView hitEnemy, int damage)
    {
        onEnemyHit?.Invoke(hitEnemy, damage);
    }

    public void InvokeOnEnemyDeath(EnemyView enemyView)
    {
        onEnemyDeath?.Invoke(enemyView);
    }

    public void InvokeOnEnemyDissolved(EnemyView dissolvedEnemy)
    {
        onEnemyDissolved?.Invoke(dissolvedEnemy);
    }

    public void InvokeOnAllEnemiesDead()
    {
        onAllEnemiesDead?.Invoke();
    }

    public void InvokeOnCurrentEnemyGroupDead()
    {
        onCurrentEnemyGroupDead?.Invoke();
    }
}
