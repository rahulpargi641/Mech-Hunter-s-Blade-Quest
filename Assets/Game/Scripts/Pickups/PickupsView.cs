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
        PlayerHealthPresenter playerHealth = other.GetComponent<PlayerHealthPresenter>();
        if(playerHealth)
        {
            Controller.ProcessHeal(playerHealth);

            //PlayerVFX playerVFX = other.GetComponent<PlayerVFX>();

            //if (playerVFX)
            //    playerVFX.PlayHealVFX();
            //gameObject.SetActive(false);
        }
    }
}
