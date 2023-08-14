using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action onEnemyDeathAction;
    public event Action onPlayerDeathAction;

    public void InvokeEnemyDeathAction()
    {
        onEnemyDeathAction?.Invoke();
    }

    internal void InvokePlayerDeathAction()
    {
        onPlayerDeathAction?.Invoke();
    }
}
