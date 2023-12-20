
public class EnemyModel
{
    public bool IsDead { get; set; } = false;
    public bool IsHit { get; set; } = false;
    public float CurrentHealthPercent { get { return CurrentHealth / (float)MaxHealth; } }

    public EnemyType EnemyType { get; }
    public float SpawningDuration { get; }
    private int MaxHealth { get; }
    public int CurrentHealth { get; set; }
   
    // Enemy AI Chracteristics
    public float VisibleDist { get; }// distance at which enemy can see the player
    public float VisibleAngle { get; } // beyond this angle enemy can't see the player
    public float AttackDist { get; }
    public float VisibleAttackAngle { get; }
    public float ShootingDist { get; }
    public float DetectDist { get; } // min distance at which enemy senses the player regardless of Visible angle
    
    // Enemy Patrolling
    public float PathUpdateDelay { get; } // delay for updating path enemy path, for avoiding calculating path every frame to improve the performance 
    public int PatrolChance { get; } // probability of enemy going into the patrol state from idle state.

    // Animations name
    public string IdleAnimName { get; }
    public string RunAnimName { get; }
    public string HurtAnimName { get; }
    public string AttackAnimName { get; }
    public string DeadAnimName { get; }

    public EnemyModel(EnemySO enemySO)
    {
        EnemyType = enemySO.enemyType;
        SpawningDuration = enemySO.spawningDuration;
        MaxHealth = enemySO.MaxHealth;
        CurrentHealth = MaxHealth;

        VisibleDist = enemySO.visibleDist;
        VisibleAngle = enemySO.visibleAngle;
        AttackDist = enemySO.attackDist;
        VisibleAttackAngle = enemySO.visibleAttackAngle;
        ShootingDist = enemySO.shootingDist;
        DetectDist = enemySO.detectDist;

        PathUpdateDelay = enemySO.pathUpdateDelay;
        PatrolChance = enemySO.patrolChance;

        IdleAnimName = enemySO.idleAnimName;
        RunAnimName = enemySO.runAnimName;
        HurtAnimName = enemySO.hurtAnimName;
        AttackAnimName = enemySO.attackAnimName;
        DeadAnimName = enemySO.deadAnimName;
    }
}
