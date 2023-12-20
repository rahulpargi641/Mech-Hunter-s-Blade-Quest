using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class HealOrb : MonoBehaviour
{
    [SerializeField] int healOrbHealthGain = 20;

    public void SetTransform(Vector3 spawnPoint, Quaternion rotation)
    {
        transform.position = spawnPoint;
        transform.rotation = rotation;
    }

    public void EnableHealOrb()
    {
        gameObject.SetActive(true);
    }

    public void DisableHealOrb()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerView playerView = other.GetComponent<PlayerView>();
        if (playerView)
        {
            EventService.Instance.InvokeOnHealPickup(healOrbHealthGain);
            VFXService.Instance.SpawnHealVFX(transform.position);

            PickupsService.Instance.ReturnHealOrbToPool(this);
        }
    }
}
