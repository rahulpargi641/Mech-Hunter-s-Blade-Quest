using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthService : MonoSingletonGeneric<PlayerHealthService>
{
    [SerializeField] PlayerHealthView playerHealthView;
    private PlayerHealthController playerHealthController;

    void Start()
    {
        HealthModel healthModel = new HealthModel();
        playerHealthController = new PlayerHealthController(healthModel, playerHealthView);
    }

    internal void PlayerDead()
    {
        EventService.Instance.InvokePlayerDeathAction();
    }

    public void PlayerHit()
    {
        EventService.Instance.InvokePlayerHitAction();
    }
}
