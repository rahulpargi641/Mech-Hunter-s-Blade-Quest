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

        model.CurrentHealth = 100;
    }

    internal void ApplyDamage(int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;
          
            view.DamageVisual();

            Debug.Log("Player Health reduced to : " + model.CurrentHealth);

            if (model.CurrentHealth <= 0)
            {
                Debug.Log("Player is Dead!");
                PlayerHealthService.Instance.InvokePlayerDeath();
            }
        }
    }

    internal void AddHealth(int healthPoints)
    {
        model.CurrentHealth += healthPoints;
        Debug.Log("Health added");
    }


    public float CurrentHealthPercentage()
    {
        return model.CurrentHealthPercent;
    }
}
