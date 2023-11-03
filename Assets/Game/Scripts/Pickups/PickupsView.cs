using UnityEngine;

public class PickupsView : MonoBehaviour
{
   public enum EPickUp
    {
        HealOrb, Coin
    }

    EPickUp pickup;

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            PlayerHealthService.Instance.AddHealth(20);
            PlayerVFXService.Instance.PlayHealVFX();
            AudioService.Instance.PlaySound(SoundType.HealOrbPickup);
            gameObject.SetActive(false);
        }
    }
}
