using System.Collections;
using System.Collections.Generic;
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
            PlayerVFXService.Instance.PlayHealVFX();
            gameObject.SetActive(false);
        }
    }
}
