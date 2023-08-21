using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : HealthView
{
    [SerializeField] private Slider healthSlider;
    public PlayerHealthController Controller { get; set; }

    private void Start()
    {
       
    }

    private void Update()
    {
        Debug.Log("Health Percent: " + Controller.CurrentHealthPercentage());
        healthSlider.value = Controller.CurrentHealthPercentage();
    }

    public void DamageVisual(Vector3 attackerPos = new Vector3())
    {
        PlayerVFXView playerVFXView = GetComponent<PlayerVFXView>();
        if (playerVFXView)
        {
            //playerVFXView.PlayBeingHitVFX(attackerPos);
            //playerVFXView.PlayBeingHitSplashVFX();
        }
        DamageBlinkEffect();
    }
}
