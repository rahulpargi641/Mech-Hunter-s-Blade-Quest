using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action<EnemyView> onEnemyDeathAction;
    public event Action onAllEnemiesDeadAction;
    public event Action onCurrentEnemyGroupDeadAction;
    public event Action onPlayerDeathAction;
    public event Action onPlayerHitAction;
    public event Action<EnemyView> onEnemyHitAction;

    private void Awake()
    {
        base.Awake();
    }

    public void InvokeOnEnemyDeath(EnemyView enemyView)
    {
        onEnemyDeathAction?.Invoke(enemyView);
    }

    public void InvokeOnPlayerDeath()
    {
        onPlayerDeathAction?.Invoke();
    }

    public void InvokeOnPlayerHit()
    {
        onPlayerHitAction?.Invoke();
    }

    public void InvokeOnEnemyHit(EnemyView enemyView)
    {
        onEnemyHitAction?.Invoke(enemyView);
    }

    public void InvokeOnAllEnemiesDead()
    {
        onAllEnemiesDeadAction?.Invoke();
    }

    public void InvokeOnCurrentEnemyGroupDead()
    {
        onCurrentEnemyGroupDeadAction?.Invoke();
    }
}
