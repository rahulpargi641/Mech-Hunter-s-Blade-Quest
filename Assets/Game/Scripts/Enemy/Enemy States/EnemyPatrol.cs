using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyState
{
    int currentIndex = -1;

    public EnemyPatrol(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
        : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Patrol;
        stage = EStage.Enter;

        navMeshAgent.speed = 1;
        navMeshAgent.isStopped = false;
    }

    protected override void Enter()
    {
        base.Enter();

        float lastPatrolPointDist = Mathf.Infinity;
        for (int i = 0; i < enemyAIView.PatrolPoints.Count; i++)
        {
            Transform currPatrolPoint = enemyAIView.PatrolPoints[i];

            float distance = Vector3.Distance(enemyAIView.transform.position, currPatrolPoint.position);
            if (distance < lastPatrolPointDist)
            {
                currentIndex = i;
                lastPatrolPointDist = distance;
            }
        }

        animator.SetTrigger("Run");
    }
    protected override void Update()
    {
        base.Update();

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (currentIndex >= enemyAIView.PatrolPoints.Count - 1)
                currentIndex = 0;

            Vector3 patrolPoint = enemyAIView.PatrolPoints[currentIndex].position;
            UpdatePath(patrolPoint);
            currentIndex++;
        }

        if (CanSeePlayer())
        {
            nextState = new EnemyPursue(enemyAIView, navMeshAgent, animator, playerTransform);
            stage = EStage.Exit;
        }
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Run");
        base.Exit();
    }
}
