using System;
using UnityEngine;

public class EnemyHealthController
{
    HealthModel model;
    EnemyHealthView view;

    public EnemyHealthController(HealthModel model, EnemyHealthView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
    }

    internal void ReduceHealth(int damage)
    {
        model.CurrentHealth -= damage;
        Debug.Log("Enemy Health reduced to : " + model.CurrentHealth);
        if(model.CurrentHealth <= 0)
        {
            EnemyHealthService.Instance.EnemyDead();
            Debug.Log("Enemy is Dead!");
        }
    }
}
