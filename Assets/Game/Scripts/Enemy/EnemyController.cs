using System;
using UnityEngine;

public class EnemyController
{
    readonly EnemyModel model;
    readonly EnemyView view;

    public EnemyController(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
        model.Controller = this;
    }

    public void SetTransform(Vector3 spawnPoint)
    {
        view.transform.position = spawnPoint;
    }

    public void EnableEnemy(int enemyID)
    {
        view.EnemyID = enemyID;
        view.gameObject.SetActive(true);
    }

    public void DisableEnemy()
    {
        view.gameObject.SetActive(false);
    }

    public void EnemyDead()
    {
        model.IsDead = true;
    }

    public bool IsDead()
    {
        if (model.IsDead)
            return true;
        else
            return false;
    }
}
