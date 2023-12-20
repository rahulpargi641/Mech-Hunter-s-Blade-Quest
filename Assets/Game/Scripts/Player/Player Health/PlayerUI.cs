using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        EventService.Instance.OnPlayerHealthChange += UpdateHealthSlider;
    }

    private void OnDestroy()
    {
        EventService.Instance.OnPlayerHealthChange -= UpdateHealthSlider;
    }

    private void UpdateHealthSlider(float currentHealthPercent)
    {
        healthSlider.value = currentHealthPercent;
    }
}
