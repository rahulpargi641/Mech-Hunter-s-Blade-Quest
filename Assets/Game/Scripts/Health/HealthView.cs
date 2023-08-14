using UnityEngine;

public class HealthView: MonoBehaviour
{
    protected HealthController controller;
    protected void Start()
    {
        HealthModel healthModel = new HealthModel();
        controller = new HealthController(healthModel);
    }

    public virtual void ApplyDamage(int damage, Vector3 attackerPos = new Vector3())
    {
        controller.ApplyDamage(damage);
    }

    protected void DamageBlink()
    {
        MaterialBlockView materialBlockView = GetComponent<MaterialBlockView>();
        materialBlockView.Blink();
    }
}
