using System;
using UnityEngine;

public class PlayerState
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
    protected PlayerState nextState;

    bool isDead = false;

    public PlayerState(PlayerView playerView, Animator animator)
    {
        this.playerView = playerView;
        this.animator = animator;

        stage = EStage.Enter;
    }

    protected virtual void Enter() 
    { 
        stage = EStage.Update;

        EventService.Instance.onPlayerDeathAction += PlayerDead;
    }

    protected virtual void Update() 
    {
        stage = EStage.Update;

        if (isDead)
        {
            nextState = new Dead(playerView, animator);
            stage = EStage.Exit;
            return;
        }
    }
    protected virtual void Exit() { stage = EStage.Exit; }

    // Get run from outside and progress state through each of the different stages
    public PlayerState Process()
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

    private void PlayerDead()
    {
        isDead = true;
    }
}
