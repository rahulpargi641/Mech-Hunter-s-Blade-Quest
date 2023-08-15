using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action onEnemyDeathAction;
    public event Action onPlayerDeathAction;
    public event Action onPlayerHitAction;

    public void InvokeEnemyDeathAction()
    {
        onEnemyDeathAction?.Invoke();
    }

    public void InvokePlayerDeathAction()
    {
        onPlayerDeathAction?.Invoke();
    }

    public void InvokePlayerHitAction()
    {
        onPlayerHitAction?.Invoke();
    }
}
