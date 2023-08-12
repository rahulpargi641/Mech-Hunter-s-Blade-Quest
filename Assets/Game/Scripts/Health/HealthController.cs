using UnityEngine;
public class HealthController
{
    private HealthModel model;

    public HealthController(HealthModel model)
    {
        this.model = model;
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        model.CurrentHealth -= damage;
        Debug.Log("Health Decresed, now current health is: " + model.CurrentHealth);
    }
}
