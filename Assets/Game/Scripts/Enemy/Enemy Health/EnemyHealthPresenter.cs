using UnityEngine;

public class EnemyHealthPresenter: MonoBehaviour
{
    private HealthModel model;
    private void Start()
    {
        model = new HealthModel();
    }

    public void ApplyDamage(EnemyView enemyView, int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;

            if (model.CurrentHealth <= 0)
                EventService.Instance.InvokeOnEnemyDeath(enemyView);
        }
    } 
}

