using UnityEngine;

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
public abstract class PlayerState
{
    protected EPlayerState state;
    protected EStage stage;
    protected PlayerState nextState;

    protected PlayerController controller;
    protected Animator animator;
    protected bool RunButtonDown => controller.RunButtonDown;
    protected bool RollButtonDown => controller.RollButtonDown;
    protected bool AttackButtonDown => controller.AttackButtonDown;
    protected bool DashAttackButtonDown => controller.DashAttackButtonDown;

    public PlayerState(PlayerController controller)
    {
        this.controller = controller;
        animator = controller.Animator;

        stage = EStage.Enter;
    }

    protected virtual void Enter()
    {
        stage = EStage.Update;
    }

    protected virtual void Update() // Update is the stage on which the current state is being processed, is called every frame in the view.
    {
        stage = EStage.Update;

        if (SwitchStateToDeadIf()) return;
        if (SwitchStateToHurtIf()) return;
    }

    protected virtual void Exit()
    {
        stage = EStage.Exit;
    }

    public PlayerState ProcessState()  // Gets run in the view every update and progresses the current state through each of the three stages.
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

    private bool SwitchStateToDeadIf()
    {
        if (controller.IsDead && controller.CanEnterDeadState)
        {
            nextState = new PlayerDead(controller);
            stage = EStage.Exit;
            return true;
        }

        return false;
    }

    private bool SwitchStateToHurtIf()
    {
        if (controller.IsHit && !controller.IsDead)
        {
            nextState = new PlayerHurt(controller);
            stage = EStage.Exit;
            return true;
        }

        return false;
    }
}
