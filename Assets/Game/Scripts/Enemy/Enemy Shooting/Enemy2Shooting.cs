using UnityEngine;

public class Enemy2Shooting : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform player;

    public void ShootDamageOrb()
    {
        DamageOrbService.Instance.SpawnDamageOrb(shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
    }

    private void Update()
    {
        transform.LookAt(player, Vector3.up); // To do: Rotate it slowly using slerp
    }
}
