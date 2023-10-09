using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPresenter : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private HealthModel model;

    private void Start()
    {
        model = new HealthModel();
    }

    private void Update()
    {
        healthSlider.value = model.CurrentHealthPercent;
    }

    public void ApplyDamage(int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;

            if (model.CurrentHealth <= 0)
                EventService.Instance.InvokeOnPlayerDeath();
        }
    }

    public void AddHealth(int healthGain)
    {
        if (model.CurrentHealth < model.MaxHealth)
            model.CurrentHealth += healthGain;
    }
}
