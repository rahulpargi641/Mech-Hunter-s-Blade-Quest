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

    internal void ReduceHealth(EnemyView enemyView, int damage)
    {
        model.CurrentHealth -= damage;

        if(model.CurrentHealth <= 0)
        {
            EventService.Instance.InvokeOnEnemyDeath(enemyView);
            
            Debug.Log( enemyView.gameObject.name +": is Dead!");
        }
    }
}
