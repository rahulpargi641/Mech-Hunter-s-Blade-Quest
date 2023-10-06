using UnityEngine;

public class PickupsView : MonoBehaviour
{
   public enum EPickUp
   {
        HealOrb, Coin
   }

    EPickUp pickup;

    public PickupsController Controller { private get; set; }

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if(playerView)
        {
            Controller.ProcessHeal();

            //PlayerVFX playerVFX = other.GetComponent<PlayerVFX>();

            //if (playerVFX)
            //    playerVFX.PlayHealVFX();
            //gameObject.SetActive(false);
        }
    }
}
