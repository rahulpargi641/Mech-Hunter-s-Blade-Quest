using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : EnemyState
{
    private float spawnDuration = 3f;
    private float currentSpawnTime;

    public EnemySpawning(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
           : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Spawning;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Idle");
        currentSpawnTime = spawnDuration;
        MaterialBlockView materialBlockView = enemyAIView.GetComponent<MaterialBlockView>();
        materialBlockView.CharacterAppear();
    }

    protected override void Update()
    {
        base.Update();

        currentSpawnTime -= Time.deltaTime;
        if(currentSpawnTime <= 0)
        {
            nextState = new EnemyIdle(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
