using UnityEngine;

public class BossEnemyShooting : MonoBehaviour // Only Boss enemy has shooting ability, attached to boss enemy
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform player;
    
    public void ShootDamageOrb()
    {
        DamageOrbService.Instance.SpawnDamageOrb(shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
    }

    private void Update()
    {
        transform.LookAt(player, Vector3.up); // To do: Rotate boss slowly using slerp
    }
}
