
using UnityEngine;

public class HealthModel
{
    public int MaxHealth { get; private set; }

    public int CurrentHealth { get; set; }

    public HealthModel()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }
}
