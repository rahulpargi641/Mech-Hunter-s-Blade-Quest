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

    internal void ApplyDamage(int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;
          
            view.DamageVisual();

            Debug.Log("Player Health reduced to : " + model.CurrentHealth);

            if (model.CurrentHealth <= 0)
            {
                PlayerHealthService.Instance.InvokePlayerDeath();
                Debug.Log("Player is Dead!");
            }
        }
    }

    internal void AddHealth(int healthPoints)
    {
        if(model.CurrentHealth < model.MaxHealth)
        {
            model.CurrentHealth += healthPoints;
            Debug.Log("Health added");
        }
    }

    public float CurrentHealthPercentage()
    {
        return model.CurrentHealthPercent;
    }
}
