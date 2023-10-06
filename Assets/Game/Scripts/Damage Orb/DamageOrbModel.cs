
public class DamageOrbModel
{
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public DamageOrbController Controller { private get; set; }

    public DamageOrbModel()
    {
        Speed = 9f;
        Damage = 10;
    }
}
