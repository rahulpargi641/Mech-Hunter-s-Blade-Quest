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

    private float visibleDist = 10.0f;
    private float visibleAngle = 80.0f; // 30f
    private float attackDist = 2.2f; // 6f
    private float visibleAttackAngle = 60.0f;
    private float shootingDist = 10.0f;

    private float pathUpdateDelay = 0.2f;
    private float pathUpdateDeadline;

    protected bool isPlayerDead = false;
    private bool isEnemyHit = false;
    private bool isEnemyDead = false;

    private bool areEventsSubscribed = false;

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

        if(!areEventsSubscribed)
        {
            areEventsSubscribed = true;
            EventService.Instance.onPlayerDeathAction += PlayerDead;
            EventService.Instance.onEnemyDeathAction += EnemyDead;
            EventService.Instance.onEnemyHitAction += EnemyHit;
        }
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

        if(isEnemyHit)
        {
            nextState = new EnemyHurt(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }
    }
    protected virtual void Exit()
    { 
        stage = EStage.Exit; 

        if(isEnemyDead)
        {
            EventService.Instance.onPlayerDeathAction -= PlayerDead;
            EventService.Instance.onEnemyDeathAction -= EnemyDead;
            EventService.Instance.onEnemyHitAction -= EnemyHit;
        }
    }
    
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

        if (enemyAIView.EnemyOfType == EnemyType.Enemy01)
        {
            if (playerDirection.magnitude < attackDist && facingAngle < visibleAttackAngle)
                return true;
            else
                return false;
        }
        else if (enemyAIView.EnemyOfType == EnemyType.Enemy02)
        {
            if (playerDirection.magnitude < shootingDist && facingAngle < visibleAttackAngle)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void PlayerDead()
    {
        isPlayerDead = true;
    }

    private void EnemyDead(EnemyView enemyView)
    {
        if(enemyAIView == enemyView)
        {
            isEnemyDead = true;
        }
    }

    private void EnemyHit(EnemyView enemyView)
    {
        if (enemyView.EnemyOfType == EnemyType.Enemy02)
        {
            isEnemyHit = true;
        }
    }
}
