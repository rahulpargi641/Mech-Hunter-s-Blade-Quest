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
            Debug.Log("Player entered the trigger");

            PlayerHealthService.Instance.AddHealth(20);
            PlayerVFX playerVFX = other.GetComponent<PlayerVFX>();
            //PlayerVFXService.Instance.PlayHealVFX();
            if (playerVFX) playerVFX.PlayHealVFX();
            gameObject.SetActive(false);
        }
    }
}
