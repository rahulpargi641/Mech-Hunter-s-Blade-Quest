using UnityEngine;

public class State
{
    public enum EStage
    {
        Enter, Update, Exit
    };
    public enum EPlayerState
    {
        Idle, Run, Attack
    };

    protected EStage stage;
    public EPlayerState state;
    protected PlayerView playerView;
    protected Animator animator;
    protected State nextState;

    public State(PlayerView playerView, Animator animator)
    {
        this.playerView = playerView;
        this.animator = animator;

        stage = EStage.Enter;
    }

    public virtual void Enter() { stage = EStage.Update; }
    public virtual void Update() { stage = EStage.Update; }
    public virtual void Exit() { stage = EStage.Exit; }

    // Get run from outside and progress state through each of the different stages
    public State Process()
    {
        if (stage == EStage.Enter) Enter();
        if (stage == EStage.Update) Update();
        if (stage == EStage.Exit)
        {
            Exit();
            return nextState;
        }
        return this; // we keep returning the same state
    }
}
