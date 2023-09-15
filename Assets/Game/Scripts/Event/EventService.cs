using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoSingletonGeneric<EventService>
{
    //public event Action<EnemyView> onEnemySpawned;
    public event Action<EnemyView> onEnemyDeathAction;
    public event Action onAllEnemiesDeadAction;
    public event Action onCurrentEnemyGroupDeadAction;
    public event Action onPlayerDeathAction;
    public event Action onPlayerHitAction;

    private void Awake()
    {
        base.Awake();
    }
    //public void InvokeEnemySpawnedAction(EnemyView enemyView)
    //{
    //    onEnemySpawned?.Invoke(enemyView);
    //}

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

    public void InvokeCurrentEnemyGroupDeadAction()
    {
        onCurrentEnemyGroupDeadAction?.Invoke();
    }
}
