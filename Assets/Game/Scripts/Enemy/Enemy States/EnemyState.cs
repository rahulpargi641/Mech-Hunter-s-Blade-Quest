using UnityEngine;
using UnityEngine.AI;

/* Fully Automated Finite State Machine.
 * How this State Machine works :-
 * 1. Any state in the state machine has 3 processes that runs at different time. three processes are Enter, Update and Exit. 
 * 
 * 2. These are also called 3 stages. Similar to Unity's StateMachineBehaviour class, which has different stages which allows 
 *   you to define custom behaviors that are triggered at specific points during the animation state transitions and playback.
 *   
 * What happens in each stage :-
 * 1. Enter - Enter runs as soon as state is transitioned to the state. Responsible for setting up things like animator parameters, animations or calculating waypoints for the patrol when entering into the Partol state.
 * 2. Update - called after enter and continues to run until the transition out of the state is triggerd. Responsible for executing current state. Ex: npc keeps patrolling the area until it detects the player and then goes into the Pursue state.
 * 3. Exit - called after update and before leaving the state. Responsible for resetting animator parameters and animations for preventing animation gliches and ensuring clean Re-entry.

 */
public class EnemyState
{
    protected EEnemyState state;
    protected EStage stage;
    protected EnemyState nextState;

    protected EnemyController controller;
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected Transform playerTransform;

    private float visibleDist;
    private float visibleAngle;
    private float attackDist;
    private float visibleAttackAngle;
    private float shootingDist;

    public EnemyState(EnemyController controller)
    {
        this.controller = controller;

        navMeshAgent = controller.NavMeshAgent;
        animator = controller.Animator;
        playerTransform = controller.PlayerTransform;

        visibleDist = controller.VisibleDist;
        visibleAngle = controller.VisibleAngle;
        attackDist = controller.AttackDist;
        visibleAttackAngle = controller.VisibleAttackAngle;
        shootingDist = controller.ShootingDist;

        stage = EStage.Enter;
    }

    protected virtual void Enter()
    {
        stage = EStage.Update;
    }

    protected virtual void Update() // Update is the stage on which the current state is being processed, is called every frame in the view.
    {
        stage = EStage.Update;

        if (ProcessIfPlayerDead()) return;

        if (SwitchStateToDeadIf()) return;
        if (SwitchStateToHurtIf()) return;
    }

    protected virtual void Exit()
    {
        stage = EStage.Exit;
    }

    public EnemyState ProcessState()  // Gets run in the view every update and progresses the current state through each of the three stages.
    {
        if (stage == EStage.Enter) Enter();
        if (stage == EStage.Update) Update();
        if (stage == EStage.Exit)
        {
            Exit();
            return nextState;
        }
        return this; // keep returning the same state
    }

    protected virtual bool SwitchStateToDeadIf()
    {
        if (controller.IsDead && controller.CanEnterDeadState)
        {
            controller.CanEnterDeadState = false;

            nextState = new EnemyDead(controller);
            stage = EStage.Exit;
            return true;
        }
        return false;
    }

    protected virtual bool ProcessIfPlayerDead()
    {
        if (controller.IsPlayerDead)
        {
            nextState = new EnemyIdle(controller);
            stage = EStage.Exit;
            return true;
        }
        return false;
    }

    protected virtual bool SwitchStateToHurtIf()
    {
        if (controller.IsHit && !controller.IsDead)
        {
            HandleHitActions();
            return true;
        }
        return false;
    }

    protected virtual void HandleHitActions()
    {
        controller.IsHit = false;

        if (controller.EnemyType == EnemyType.GaintMech)
        {
            controller.CharacterMaterial?.PlayHurtBlinkEffect();
            return;
        }

        nextState = new EnemyHurt(controller);
        stage = EStage.Exit;
    }
    
    protected bool CanSeePlayer()
    {
        Vector3 playerDirection = playerTransform.position - controller.EnemyTransform.position;
        float facingAngle = Vector3.Angle(playerDirection, controller.EnemyTransform.forward);

        return (playerDirection.magnitude < visibleDist && facingAngle < visibleAngle);
    }

    protected bool CanAttackPlayer()
    {
        Vector3 playerDirection = playerTransform.position - controller.EnemyTransform.position;
        float facingAngle = Vector3.Angle(playerDirection, controller.EnemyTransform.forward);

        if (controller.EnemyType == EnemyType.GaintMech)
        {
            return CanAttackWithPhysical(playerDirection, facingAngle);
        }
        else if (controller.EnemyType == EnemyType.ShooterMechBoss)
        {
            return CanAttackWithShooting(playerDirection, facingAngle);
        }
        else
            return false;
    }

    private bool CanAttackWithPhysical(Vector3 playerDirection, float facingAngle)
    {
        return playerDirection.magnitude < attackDist && facingAngle < visibleAttackAngle;
    }

    private bool CanAttackWithShooting(Vector3 playerDirection, float facingAngle)
    {
        return playerDirection.magnitude < shootingDist && facingAngle < visibleAttackAngle;
    }
}
