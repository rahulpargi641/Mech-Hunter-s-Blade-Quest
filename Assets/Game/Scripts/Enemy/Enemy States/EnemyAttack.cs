using UnityEngine;

public class EnemyAttack : EnemyState
{
    private readonly float detectDist; // distance at which enemy detects the player even if enemy can't see the player

    public EnemyAttack(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Attack;

        detectDist = controller.DetectDist;
    }

    protected override void Enter()
    {
        base.Enter();
        PerformAttack();
    }

    protected override void Update()
    {
        base.Update();

        ContinueAttackingIf();
    }

    protected override void Exit()
    {
        animator.ResetTrigger(controller.AttackAnimName);
        base.Exit();
    }

    private void PerformAttack()
    {
        FaceTowardsPlayer();

        controller.AttackAnimationEnded = false;
        navMeshAgent.isStopped = true;

        animator.SetTrigger(controller.AttackAnimName);
    }

    private void ContinueAttackingIf()
    {
        if(controller.AttackAnimationEnded) // AttackAnimationEnded will be set to true in the EnemyView when attack animation ends via animation event
        {
            PerformAttack();
            SwitchStateIf();
        }
    }

    private void SwitchStateIf()
    {
        if (CanDetectPlayer())
        {
            nextState = new EnemyPursue(controller);
            stage = EStage.Exit;
        }
        else if (!CanAttackPlayer())
        {
            nextState = new EnemyIdle(controller);
            stage = EStage.Exit;
        }
    }

    private bool CanDetectPlayer()
    {
        Vector3 playerDirection = playerTransform.position - controller.EnemyTransform.position;
        float playerDistance = playerDirection.magnitude;

        return playerDistance < detectDist;
    }

    private void FaceTowardsPlayer()
    {
        Vector3 playerDirection = playerTransform.position - controller.EnemyTransform.position;
        playerDirection.y = 0; // To do: Update this with extension function

        Quaternion lookRotation = Quaternion.LookRotation(playerDirection);
        controller.EnemyTransform.rotation = lookRotation;
        // To do: Rotate slowly using slerp
        //float facingAngle = Vector3.Angle(playerDirection, controller.EnemyTransform.forward);
        //enemyAIView.transform.rotation = Quaternion.Slerp(enemyAIView.transform.rotation, lookRotation, rotationSpeed);
    }
}
