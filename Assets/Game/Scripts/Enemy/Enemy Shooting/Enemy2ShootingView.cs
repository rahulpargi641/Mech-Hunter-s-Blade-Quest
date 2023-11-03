using UnityEngine;

public class Enemy2ShootingView : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject damageOrb;
    [SerializeField] Transform player;

    public void ShootDamageOrb()
    {
        //Instantiate(damageOrb, shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
        // DamageOrbView damageOrb = DamageOrbService.Instance.CreateDamageOrb();
        //damageOrb.transform.position = shootingPoint.position;
        //damageOrb.transform.rotation = Quaternion.LookRotation(shootingPoint.forward);
        

        DamageOrbService.Instance.CreateDamageOrb(shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));

            //damageOrbController.SetDamageOrbTransform(shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
    }

    private void Update()
    {
        transform.LookAt(player, Vector3.up); // Rotate it slowly
    }
}
