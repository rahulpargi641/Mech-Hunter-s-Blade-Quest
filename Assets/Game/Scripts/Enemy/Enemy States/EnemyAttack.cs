using UnityEngine;

public class EnemyAttack : EnemyState
{
    private float detectDist; // distance at which enemy detects the player even if enemy can't see the player

    public EnemyAttack(EnemyView enemyAIView, EnemySO enemy) : base(enemyAIView, enemy)
    {
        state = EState.Attack;
        stage = EStage.Enter;
    }

    protected override void Enter()
    {
        base.Enter();

        detectDist = enemy.detectDist;
        Attack();
        //AudioService.Instance.PlayAttackSound
    }

    protected override void Update()
    {
        if (isPlayerDead) return;

        base.Update();

        if (enemyAIView.AttackAnimationEnded)
        {
            Attack();

            if(CanDetectPlayer())
            {
                nextState = new EnemyPursue(enemyAIView, enemy);
                stage = EStage.Exit;
            }
            else if (!CanAttackPlayer())
            {
                nextState = new EnemyIdle(enemyAIView, enemy);
                stage = EStage.Exit;
            }
        }
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

    private void Attack()
    {
        FaceTowardsPlayer();

        enemyAIView.AttackAnimationEnded = false;
        navMeshAgent.isStopped = true;
        animator.SetTrigger(enemy.attackAnimName);
    }

    private void FaceTowardsPlayer()
    {
        Vector3 playerDirection = playerTransform.position - enemyAIView.transform.position;
        playerDirection.y = 0; // To do: Update this with extension function

        float facingAngle = Vector3.Angle(playerDirection, enemyAIView.transform.forward);
        Quaternion lookRotation = Quaternion.LookRotation(playerDirection);
        enemyAIView.transform.rotation = lookRotation;
        //enemyAIView.transform.rotation = Quaternion.Slerp(enemyAIView.transform.rotation, lookRotation, rotationSpeed);
    }

    protected override void Exit()
    {
        animator.ResetTrigger(enemy.attackAnimName);
        base.Exit();
    }
}
