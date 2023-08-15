using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController
{
    private HealthModel model;
    private PlayerHealthView view;

    public PlayerHealthController(HealthModel model, PlayerHealthView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
    }

    internal void ReduceHealth(int damage)
    {
        model.CurrentHealth -= damage;
        Debug.Log("Player Health reduced to : " + model.CurrentHealth);
        if (model.CurrentHealth <= 0)
        {
            PlayerHealthService.Instance.PlayerDead();
            Debug.Log("Player is Dead!");
        }
        else
            PlayerHealthService.Instance.PlayerHit();
    }
}
