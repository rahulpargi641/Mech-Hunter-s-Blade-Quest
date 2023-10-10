using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : EnemyState
{
    private float spawnDuration = 3f;
    private float currentSpawnTime;

    public EnemySpawning(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Spawning;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger(enemy.idleAnimName);

        currentSpawnTime = spawnDuration;
        MaterialBlock materialBlockView = enemyAIView.GetComponent<MaterialBlock>();
        materialBlockView.CharacterAppearEffect();
    }

    protected override void Update()
    {
        base.Update();

        currentSpawnTime -= Time.deltaTime;
        if(currentSpawnTime <= 0)
        {
            nextState = new EnemyIdle(enemyAIView, enemy);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger(enemy.idleAnimName);
        base.Exit();
    }
}
