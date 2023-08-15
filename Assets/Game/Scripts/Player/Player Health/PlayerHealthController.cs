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

    internal void ApplyDamage(int damage, Vector3 attackerPos)
    {
        if (model.CurrentHealth <= 0)
        {
            PlayerHealthService.Instance.PlayerDead();
            Debug.Log("Player is Dead!");
        }
        else
        {
            model.CurrentHealth -= damage;
            PlayerHealthService.Instance.PlayerHit();
            PlayerService.Instance.AddHitImpact(attackerPos, 10f);
            Debug.Log("Player Health reduced to : " + model.CurrentHealth);
        }
    }
}
