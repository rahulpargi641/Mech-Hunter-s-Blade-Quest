using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2ShootingPresenter : MonoBehaviour
{
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject damageOrb;
    [SerializeField] Transform player;

    public void ShootDamageOrb()
    {
        Instantiate(damageOrb, shootingPoint.position, Quaternion.LookRotation(shootingPoint.forward));
    }

    private void Update()
    {
        transform.LookAt(player, Vector3.up);
    }
}
