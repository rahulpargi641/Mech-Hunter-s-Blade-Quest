using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    //public event Action<EnemyView> onEnemySpawned;
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

    //public void InvokeEnemySpawnedAction(EnemyView enemyView)
    //{
    //    onEnemySpawned?.Invoke(enemyView);
    //}

    public void InvokeOnEnemyDeath(EnemyView enemyView)
    {
        onEnemyDeathAction?.Invoke(enemyView);
    }

    public void InvokePlayerDeathAction()
    {
        onPlayerDeathAction?.Invoke();
    }

    public void InvokeOnPlayerHit()
    {
        onPlayerHitAction?.Invoke();
    }

    public void InvokeEnemyHitAction(EnemyView enemyView)
    {
        onEnemyHitAction?.Invoke(enemyView);
    }

    public void InvokeAllEnemiesDeadAction()
    {
        onAllEnemiesDeadAction?.Invoke();
    }

    public void InvokeCurrentEnemyGroupDeadAction()
    {
        onCurrentEnemyGroupDeadAction?.Invoke();
    }
}
