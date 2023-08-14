using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
    private float detectDist = 4f;
    private float rotationSpeed = 2f;
    private float rotationDuration = 0.5f;
    public EnemyAttack(EnemyView enemyAIView, NavMeshAgent navMeshAgent, Animator animator, Transform playerTransform)
          : base(enemyAIView, navMeshAgent, animator, playerTransform)
    {
        state = EState.Attack;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        Attack();
        //AudioService.Instance.PlayAttackSound
    }

    protected override void Update()
    {
        base.Update();

        if (enemyAIView.AttackAnimationEnded)
        {
            Attack();

            if(CanDetectPlayer())
            {
                nextState = new EnemyPursue(enemyAIView, navMeshAgent, animator, playerTransform);
                stage = EStage.Exit;
            }
            else if (! CanAttackPlayer())
            {
                nextState = new EnemyIdle(enemyAIView, navMeshAgent, animator, playerTransform);
                stage = EStage.Exit;
            }
        }
    }

    private void FaceTowardsPlayer()
    {
        Vector3 playerDirection = playerTransform.position - enemyAIView.transform.position;
        playerDirection.y = 0; // Update this 

        float facingAngle = Vector3.Angle(playerDirection, enemyAIView.transform.forward);
        Quaternion lookRotation = Quaternion.LookRotation(playerDirection);
        enemyAIView.transform.rotation = lookRotation;
        //enemyAIView.transform.rotation = Quaternion.Slerp(enemyAIView.transform.rotation, lookRotation, rotationSpeed);
    }

    protected override void Exit()
    {
        animator.ResetTrigger("Attack");
        base.Exit();
    }

    private void Attack()
    {
        FaceTowardsPlayer();
        enemyAIView.AttackAnimationEnded = false;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");
    }

    private bool CanDetectPlayer()
    {
        Vector3 playerDirection = playerTransform.position - enemyAIView.transform.position;
        float facingAngle = Vector3.Angle(playerDirection, enemyAIView.transform.forward);

        if (playerDirection.magnitude < detectDist)
            return true;
        else
            return false;
    }
}
