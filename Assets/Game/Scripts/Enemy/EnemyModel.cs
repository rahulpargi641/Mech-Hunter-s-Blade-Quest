
public class EnemyModel
{
    public bool IsDead { get; set; }
    public EnemyController Controller { get; internal set; }

    public EnemyModel()
    {
        IsDead = false;
    }
}
