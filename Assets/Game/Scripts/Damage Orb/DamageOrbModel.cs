
public class DamageOrbModel
{
    public float Speed { get; }
    public int Damage { get; }
    public float HitForce { get; }

    public DamageOrbModel(DamageOrbSO damageOrbSO)
    {
        Speed = damageOrbSO.speed;
        Damage = damageOrbSO.damage;
        HitForce = damageOrbSO.hitForce;
    }
}
