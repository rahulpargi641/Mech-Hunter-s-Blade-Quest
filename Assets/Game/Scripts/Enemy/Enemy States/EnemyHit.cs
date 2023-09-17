using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHit : EnemyState
{
    public EnemyHit(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
           : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Idle;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("BeingHit");
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
        animator.ResetTrigger("BeingHit");
        base.Exit();
    }
}
