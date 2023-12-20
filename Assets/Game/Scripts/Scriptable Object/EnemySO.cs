using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Type")]
    public EnemyType enemyType;
    public float spawningDuration = 3f;

    [Header("Health")]
    public int MaxHealth = 100;

    [Header("Chracteristics")]
    public float visibleDist = 10f; // distance at which enemy can see the player
    public float visibleAngle = 80f; // beyond this angle enemy can't see the player
    public float attackDist = 2.2f;
    public float visibleAttackAngle = 60.0f;
    public float shootingDist = 10.0f;
    public float detectDist = 4f; // min distance at which enemy senses the player regardless of Visible angle
    public float pathUpdateDelay = 0.2f; // delay for updating path enemy path, for avoiding calculating path every frame to improve the performance 
    public int patrolChance = 50; // probability of enemy going into the patrol state from idle state.

    [Header("Attack")]
    public int damage = 30;
    public int hitForce = 10;

    [Header("Animation Names")]
    public string idleAnimName = "Idle";
    public string runAnimName = "Run";
    public string hurtAnimName = "Hurt";
    public string attackAnimName = "Attack";
    public string deadAnimName = "Dead";
}
