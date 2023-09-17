using UnityEngine;
using UnityEngine.AI;

public class EnemyHurt : EnemyState
{
    public EnemyHurt(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
           : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Hurt");
        enemyAIView.BeingHitAnimationEnded = false;
    }

    protected override void Update()
    {
        base.Update();

        if (enemyAIView.BeingHitAnimationEnded)
        {
            nextState = new EnemyIdle(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Hurt");
        base.Exit();
    }
}
