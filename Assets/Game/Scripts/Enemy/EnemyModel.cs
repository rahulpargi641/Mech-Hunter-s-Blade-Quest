
public class EnemyModel
{
    public bool IsDead { get; set; }
    public EnemySO EnemySO { get; private set; }
    public EnemyController Controller { get; internal set; }

    public EnemyModel(EnemySO enemySO)
    {
        EnemySO = enemySO;
        IsDead = false;
    }
}
