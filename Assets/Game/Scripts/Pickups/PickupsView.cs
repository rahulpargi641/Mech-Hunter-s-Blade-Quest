using UnityEngine;

public class PickupsView : MonoBehaviour
{
   public enum EPickUp
    {
        HealOrb, Coin
    }

    EPickUp pickup;

    private int healOrbGain = 20;

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            PlayerHealthService.Instance.AddHealth(healOrbGain);
            PlayerVFX playerVFX = other.GetComponent<PlayerVFX>();

            if (playerVFX)
                playerVFX.PlayHealVFX();

            PickupsService.Instance.ReturnPickupToPool(this);
            //gameObject.SetActive(false);
        }
    }
}
