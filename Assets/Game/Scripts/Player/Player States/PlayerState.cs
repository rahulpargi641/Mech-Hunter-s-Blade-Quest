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
        Idle, Run, Attack, BeingHit, Roll, Dead
    };

    protected EStage stage;
    public EPlayerState state;
    protected PlayerView playerView;
    protected Animator animator;
    protected PlayerState nextState;

    private bool isHit = false;
    private bool isDead = false;

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
        EventService.Instance.onPlayerHitAction += PlayerHit;
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

        if (isHit)
        {
            nextState = new BeingHit(playerView, animator);
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

    protected bool CanAttack()
    {
        if (playerView.MouseButtonDown)
            return true;
        else
            return false;

    }

    protected bool CanRun()
    {
        if (playerView.HorizontalInput != 0 || playerView.VerticalInput != 0)
            return true;
        else
            return false;
    }

    protected bool CanRoll()
    {
        if (playerView.SpaceKeyDown)
            return true;
        else
            return false;
    }

    private void PlayerHit()
    {
        isHit = true;
    }

    private void PlayerDead()
    {
        isDead = true;
    }
}
