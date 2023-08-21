using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{   
    public enum EStage
    {
        Enter, Update, Exit
    };
    public enum EState
    {
        Spawning, Idle, Patrol, Pursue, Attack, BeingHit, Dead
    };

    protected EState state;
    protected EStage stage;
    protected EnemyView enemyAIView;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    protected Transform playerTransform;
    protected EnemyState nextState;

    private float visibleDist = 8.0f; // 10f
    private float visibleAngle = 80.0f; // 30f
                                        // private float attackDist = 2.2f; // 6f
    private float attackDist = 6f;

    private float pathUpdateDelay = 0.2f;
    private float pathUpdateDeadline;

    private bool isEnemyDead = false;
    public bool isPlayerDead = false;

    public EnemyState(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
    {
        this.enemyAIView = enemyAIView;
        this.navMeshAgent = navMeshAgent;
        this.animator = animator;
        this.playerTransform = playerTransform;
    }

    protected virtual void Enter() 
    { 
        stage = EStage.Update;

        EventService.Instance.onEnemyDeathAction += EnemyDead;
        EventService.Instance.onPlayerDeathAction += PlayerDead;
    }

    protected virtual void Update() 
    {
        stage = EStage.Update;

        if (isEnemyDead)
        {
            nextState = new EnemyDead(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
            return;
        }
    }
    protected virtual void Exit() { stage = EStage.Exit; }
    
    public EnemyState Process()
    {
        if (stage == EStage.Enter) Enter();
        if (stage == EStage.Update) Update();
        if (stage == EStage.Exit)
        {
            Exit();
            return nextState;
        }
        return this; // we keep returning the same state
    }

    protected void UpdatePath(Vector3 targetPoint)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + pathUpdateDelay;
            navMeshAgent.SetDestination(targetPoint);
        }
    }
    protected bool CanSeePlayer()
    {
        Vector3 playerDirection = playerTransform.position - enemyAIView.transform.position;
        float facingAngle = Vector3.Angle(playerDirection, enemyAIView.transform.forward);

        if (playerDirection.magnitude < visibleDist && facingAngle < visibleAngle)
            return true;
        else
            return false;
    }

    protected bool CanAttackPlayer()
    {
        Vector3 playerDirection = playerTransform.position - enemyAIView.transform.position;

        float facingAngle = Vector3.Angle(playerDirection, enemyAIView.transform.forward);

        if (playerDirection.magnitude < attackDist && facingAngle < 60)
            return true;
        else
            return false;
    }

    void EnemyDead(EnemyView enemyView)
    {
        isEnemyDead = true;
    }

    private void PlayerDead()
    {
        isPlayerDead = true;
    }
}
