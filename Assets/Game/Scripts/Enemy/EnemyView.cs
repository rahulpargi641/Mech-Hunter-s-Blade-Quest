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
    public EnemyController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    public bool AttackAnimationEnded { get; set; }
    public bool BeingHitAnimationEnded { get; set; }

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected Transform playerTransform;
    private DamageCasterView damageCaster;
    protected EnemyState currentState;

    virtual protected void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        damageCaster = GetComponentInChildren<DamageCasterView>();
    }

    virtual protected void Start()
    {
        gameObject.SetActive(false);

        playerTransform = FindAnyObjectByType<PlayerView>().transform;
    }

    virtual public void SpawnEnemy()
    {
        currentState = new EnemySpawning(this, navMeshAgent, animator, playerTransform);
        //currentState = new EnemyIdle(this, navMeshAgent, animator, playerTransform);
        //navMeshAgent.speed = MoveSpeed = 5;
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
    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }

    public void DisableDamageCaster()
    {
        damageCaster.DisableDamageCaster();
    }

    public void EnemyDead()
    {
        Controller.EnemyDead();
        CharacterController.enabled = false;
    }

    public bool IsDead()
    {
        if (Controller.IsDead())
            return true;
        else
            return false;
    }
}
