using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Properties")]
    public float visibleDist = 10f;
    public float visibleAngle = 80f;
    public float attackDist = 2.2f;
    public float visibleAttackAngle = 60.0f;
    public float shootingDist = 10.0f;
    public float detectDist = 4f;
    public float pathUpdateDelay = 0.2f;
    public int patrolChance = 50;

    [Header("Animation Names")]
    public string idleAnimName = "Idle";
    public string runAnimName = "Run";
    public string hurtAnimName = "Hurt";
    public string attackAnimName = "Attack";
    public string deadAnimName = "Dead";
}
