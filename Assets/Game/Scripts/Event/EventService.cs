using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action onEnemySpawned;
    public event Action<EnemyView> onEnemyDeathAction;
    public event Action onAllEnemiesDeadAction;
    public event Action onPlayerDeathAction;
    public event Action onPlayerHitAction;

    private void Awake()
    {
        base.Awake();
    }
    public void InvokeEnemySpawnedAction()
    {
        onEnemySpawned?.Invoke();
    }

    public void InvokeEnemyDeathAction(EnemyView enemyView)
    {
        onEnemyDeathAction?.Invoke(enemyView);
    }

    public void InvokePlayerDeathAction()
    {
        onPlayerDeathAction?.Invoke();
    }

    public void InvokePlayerHitAction()
    {
        onPlayerHitAction?.Invoke();
    }

    public void InvokeAllEnemiesDeadAction()
    {
        onAllEnemiesDeadAction?.Invoke();
    }
}
