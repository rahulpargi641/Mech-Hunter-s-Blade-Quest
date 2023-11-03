using System;

public class EnemyController
{
    readonly EnemyModel model;
    readonly EnemyView view;

    public EnemyController(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
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
