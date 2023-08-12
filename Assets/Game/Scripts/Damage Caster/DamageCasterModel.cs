using System.Collections.Generic;
using UnityEngine;

public class DamageCasterModel
{
    public int Damage { get; private set; }

    public List<Collider> damagedTargets;

    public DamageCasterModel()
    {
        Damage = 30;
        damagedTargets = new List<Collider>();
    }
}
