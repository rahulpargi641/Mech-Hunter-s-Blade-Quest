
using UnityEngine;

public class HealthModel
{
    public int MaxHealth { get; private set; }

    public int CurrentHealth { get; set; }

    public HealthModel()
    {
        MaxHealth = 1000;
        CurrentHealth = MaxHealth;
    }
}
