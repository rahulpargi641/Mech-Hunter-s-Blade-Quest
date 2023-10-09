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

    public EPlayerState state;
    protected EStage stage;

    protected PlayerView playerView;
    protected Animator animator;
    protected PlayerSO player;

    protected PlayerState nextState;

    private bool isHit = false;
    private bool isDead = false;
    private bool areEventsSubscribed = false;

    public PlayerState(PlayerView playerView, PlayerSO player)
    {
        this.playerView = playerView;
        this.animator = playerView.Animator;
        this.player = player;

        stage = EStage.Enter;
    }

    protected virtual void Enter() 
    {
        stage = EStage.Update;

        if (!areEventsSubscribed)
        {
            areEventsSubscribed = true;
            EventService.Instance.onPlayerDeathAction += PlayerDead;
            EventService.Instance.onPlayerHitAction += PlayerHit;
        }
    }

    protected virtual void Update() 
    {
        stage = EStage.Update;

        if (isDead)
        {
            nextState = new PlayerDead(playerView, player);
            stage = EStage.Exit;
            return;
        }

        if (isHit)
        {
            nextState = new PlayerHurt(playerView, player);
            stage = EStage.Exit;
            return;
        }
    }

    protected virtual void Exit() 
    {
        stage = EStage.Exit;

        if(isDead)
        {
            EventService.Instance.onPlayerDeathAction -= PlayerDead;
            EventService.Instance.onPlayerHitAction -= PlayerHit;
        }
    }

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
        if (playerView.MouseButton1Down)
            return true;
        else
            return false;
    }

    protected bool CanAttack2()
    {
        if (playerView.MouseButton2Down)
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
