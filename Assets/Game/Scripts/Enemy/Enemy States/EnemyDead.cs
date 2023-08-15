using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDead : EnemyState
{
    public EnemyDead(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
          : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Dead;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        animator.SetTrigger("Dead");
    }

    protected override void Exit()
    {
        base.Exit();
    }
}