using UnityEngine;

public class PlayerController
{
    #region View Related
    public Animator Animator => view.Animator;
    public DamageCasterView PlayerDamageCaster => view.DamageCaster;

    public float HorizontalInput => view.HorizontalInput;
    public float VerticalInput => view.VerticalInput;

    public Vector3 PlayerForwardVector => view.transform.forward;
    public bool IsGrounded => view.CharacterController.isGrounded;

    public bool RunButtonDown => view.HorizontalInput != 0 || view.VerticalInput != 0;
    public bool RollButtonDown => view.SpaceButtonDown;
    public bool AttackButtonDown => view.LeftMouseButtonDown;
    public bool DashAttackButtonDown => view.RightMouseButtonDown;

    public bool HurtAnimationEnded { get { return view.HurtAnimationEnded; } set { view.HurtAnimationEnded = value; } }
    public bool RollAnimationEnded { get { return view.RollAnimationEnded; } set { view.RollAnimationEnded = value; } }
    public bool AttackAnimationEnded { get { return view.AttackAnimationEnded; } set { view.AttackAnimationEnded = value; } }
    #endregion

    #region Model Related
    #region Movement Related

    public bool IsHit { get { return model.IsHit; } set { model.IsHit = value; } }
    public bool IsDead => model.IsDead;
    public float MoveSpeed => model.MoveSpeed;
    public float RollSlideSpeed => model.RollSlideSpeed;
    public float FallGravity => model.FallGravity;
    public Vector3 CurrentPushVelocity => model.CurrentPushVelocity;
    #endregion

    #region Attack related
    public float MinComboWindow => model.MinComboWindow; // Min window to perform attack combo
    public float MaxAnimWindow => model.MaxComboWindow; // Max window to perform attack combo
    public float DashAttackSlideDuration => model.DashAttackSlideDuration;
    public float DashAttackSlideSpeed => model.DashAttackSlideSpeed;
    #endregion

    #region Animations Names
    public string IdleAnimName => model.IdleAnimName;
    public string RunAnimName => model.RunAnimName;
    public string RollAnimName => model.RollAnimName;
    public string HurtAnimName => model.HurtAnimName;
    public string AttackAnimName => model.AttackAnimName;
    public string LastAttackInComboAnimName => model.LastAttackInComboAnimName;
    public string DeadAnimName => model.DeadAnimName;
    #endregion
    #endregion

    public bool CanEnterDeadState { get; set; } = true;

    readonly PlayerModel model;
    readonly PlayerView view;

    private PlayerState currentState;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
        model.Controller = this;

        EventService.Instance.onPlayerHit += ProcessPlayerHit;
        EventService.Instance.onHealPickup += IncreaseHealth;

        currentState = new PlayerIdle(this);
    }

    public void ProcessCurrentState()
    {
        currentState = currentState.ProcessState();
    }

    public void MovePlayer(Vector3 moveVelocity)
    {
        view.CharacterController.Move(moveVelocity);
    }

    public void RotatePlayer(Vector3 moveVelocity) // Rotate player in the direction of velocity i.e current player moveement input
    {
        view.transform.rotation = Quaternion.LookRotation(moveVelocity);
    }

    private void ProcessPlayerHit(Vector3 hitPoint, float hitForce, int damage)
    {
        model.IsHit = true;
        AddHitImpactForce(hitPoint, hitForce); 
        ApplyDamage(damage);
    }

    public void AddHitImpactForce(Vector3 attackerPos, float hitForce)
    {
        Vector3 impactDir = view.transform.position - attackerPos;
        impactDir.Normalize();
        impactDir.y = 0;

        model.CurrentPushVelocity = impactDir * hitForce;
    }

    private void ApplyDamage(int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;
            EventService.Instance.InvokeOnPlayerHealthChange(model.CurrentHealthPercent);

            if (model.CurrentHealth <= 0)
            {
                model.IsDead = true;
                EventService.Instance.InvokeOnPlayerDeath();
            }
        }
    }

    private void IncreaseHealth(int healthGain)
    {
        if (model.CurrentHealth < model.MaxHealth)
            model.CurrentHealth += healthGain;

        EventService.Instance.InvokeOnPlayerHealthChange(model.CurrentHealthPercent);
    }

    public void OnDestroy()
    {
        EventService.Instance.onPlayerHit -= ProcessPlayerHit;
        EventService.Instance.onHealPickup -= IncreaseHealth;
    }

    //private void InAir()
    //{
    //    view.Animator.SetBool("AirBorne", !view.CharacterController.isGrounded);
    //    //view.Animator.SetFloat("Speed", model.MovementVelocity.magnitude);
    //}
}
