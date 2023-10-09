using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Enemy01, Enemy02
}
public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    public EnemyType EnemyOfType => enemyType;

    public List<Transform> PatrolPoints;
    public EnemyController Controller { get; set; }
    public CharacterController CharacterController { get; set; }
    public bool AttackAnimationEnded { get; set; }
    public bool BeingHitAnimationEnded { get; set; }
    public int EnemyID { private get; set; }

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected Transform playerTransform;
    protected EnemyState currentState;

    private DamageCasterPresenter damageCaster;

    virtual protected void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        damageCaster = GetComponentInChildren<DamageCasterPresenter>();
    }

    private void OnEnable()
    {
        playerTransform = FindAnyObjectByType<PlayerView>().transform;

        InitializeEnemyState();
    }

    private void InitializeEnemyState()
    {
        currentState = new EnemySpawning(this, navMeshAgent, animator, playerTransform);
    }

    virtual protected void FixedUpdate()
    {
        currentState = currentState.Process();
    }

    // called via animation event
    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
    }

    // called via animation event
    public void HurtAnimationEnd()
    {
        BeingHitAnimationEnded = true;
    }

    // called via animation event
    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }

    // called via animation event
    public void DisableDamageCaster()
    {
        damageCaster.DisableDamageCaster();
    }

    public bool IsDead()
    {
        if (Controller.IsDead())
            return true;
        else
            return false;
    }

    //private void SetPatrolPoints()
    //{
    //    Transform parentTransform;
    //    if (EnvironmentService.Instance.enemyPatrolPoints.TryGetValue(EnemyID, out parentTransform))
    //    {
    //        for (int i = 0; i < parentTransform.childCount; i++)
    //            PatrolPoints2.Add(parentTransform.GetChild(i));
    //    }
    //}
}
