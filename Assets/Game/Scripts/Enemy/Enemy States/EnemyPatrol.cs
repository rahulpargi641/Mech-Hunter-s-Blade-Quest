using UnityEngine;

public class EnemyPatrol : EnemyState
{
    private readonly float pathUpdateDelay;
    private float pathUpdateDeadline;
    private int currentIndex = -1; // Current Patrol Point index

    public EnemyPatrol(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Patrol;

        pathUpdateDelay = controller.PathUpdateDelay;

        navMeshAgent.speed = 1;
        navMeshAgent.isStopped = false;
    }

    protected override void Enter()
    {
        base.Enter();
        animator.SetTrigger(controller.RunAnimName);

        FindClosestPatrolPoint();
    }

    protected override void Update()
    {
        base.Update();

        SetNextPatrolPointIf(); // If has reached destination

        SwitchStateToPursueIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.RunAnimName);
        base.Exit();
    }

    private void FindClosestPatrolPoint()
    {
        float lastPatrolPointDist = Mathf.Infinity;

        for (int i = 0; i < controller.PatrolPoints.Count; i++)
        {
            Transform currentPatrolPoint = controller.PatrolPoints[i];
            float distance = CalculateDistanceToPatrolPoint(currentPatrolPoint);

            UpdateClosestPatrolPointIndex(distance, ref lastPatrolPointDist, i);
        }
    }

    private float CalculateDistanceToPatrolPoint(Transform patrolPoint)
    {
        return Vector3.Distance(controller.EnemyTransform.position, patrolPoint.position);
    }

    private void UpdateClosestPatrolPointIndex(float distance, ref float lastPatrolPointDist, int index)
    {
        if (distance < lastPatrolPointDist)
        {
            currentIndex = index;
            lastPatrolPointDist = distance;
        }
    }

    private void SetNextPatrolPointIf()
    {
        if (HasReachedDestination())
        {
            SetNextPatrolPoint();
        }
    }

    private bool HasReachedDestination()
    {
        return !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

    private void SetNextPatrolPoint()
    {
        if (currentIndex >= controller.PatrolPoints.Count - 1) 
            currentIndex = 0;

        Vector3 patrolPoint = controller.PatrolPoints[currentIndex].position;
        SetDestination(patrolPoint);
        currentIndex++;
    }

    private void SetDestination(Vector3 targetPoint)
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + pathUpdateDelay;  // Update the deadline for the next path update
            navMeshAgent.SetDestination(targetPoint);
        }
    }

    private void SwitchStateToPursueIf()
    {
        if (CanSeePlayer())
        {
            nextState = new EnemyPursue(controller);
            stage = EStage.Exit;
        }
    }
}
