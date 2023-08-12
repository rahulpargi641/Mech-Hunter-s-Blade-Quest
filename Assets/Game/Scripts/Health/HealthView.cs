using UnityEngine;

public class HealthView : MonoBehaviour
{
    private HealthController controller;

    private void Start()
    {
        HealthModel healthModel = new HealthModel();
        controller = new HealthController(healthModel);
    }

    public void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        controller.ApplyDamage(damage);
    }

}
