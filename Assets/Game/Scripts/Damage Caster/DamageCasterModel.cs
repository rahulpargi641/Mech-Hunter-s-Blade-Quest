using System.Collections.Generic;
using UnityEngine;

public class DamageCasterModel
{
    public int Damage { get; private set; }
    public int HitForce { get; private set; }

    public List<Collider> damagedTargets;

    public DamageCasterModel()
    {
        Damage = 30;
        HitForce = 10;
        damagedTargets = new List<Collider>();
    }
}
