
public class DamageCasterModel
{
    public int Damage { get; }
    public int HitForce { get; }

    public DamageCasterModel(int hitForce, int damage) // initialized via child classes (Player and Enemies)
    {
        HitForce = hitForce;
        Damage = damage;
    }
}
