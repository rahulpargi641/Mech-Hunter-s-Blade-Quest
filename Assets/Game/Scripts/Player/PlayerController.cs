using UnityEngine;

public class PlayerController
{
    readonly PlayerModel model;
    readonly PlayerView view;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
        model.Controller = this;
    }

    public void ProcessMovement(float horizontalInput, float verticalInput)
    {
        XZPlaneMovement(horizontalInput, verticalInput);
        Turn();
        YAxisMovement();
        //InAir();

        view.CharacterController.Move(model.MovementVelocity);
    }

    private void XZPlaneMovement(float horizontalInput, float verticalInput)
    {
        model.MovementVelocity.Set(horizontalInput, 0f, verticalInput);
        model.MovementVelocity.Normalize();
        model.MovementVelocity = Quaternion.Euler(0f, -45f, 0f) * model.MovementVelocity;

        model.MovementVelocity *= model.MoveSpeed * Time.deltaTime;
    }

    private void Turn()
    {
        if (model.MovementVelocity != Vector3.zero)
            view.transform.rotation = Quaternion.LookRotation(model.MovementVelocity);
    }

    private void YAxisMovement()
    {
        if (view.CharacterController.isGrounded == false)
            model.VerticalSpeed = model.FallGravity;
        else
            model.VerticalSpeed = model.FallGravity * 0.3f;

            model.MovementVelocity += model.VerticalSpeed * Vector3.up * Time.deltaTime;
    }

    public void AddHitImpactForce(Vector3 attackerPos, float force)
    {
        Vector3 impactDir = view.transform.position - attackerPos;
        impactDir.Normalize();
        impactDir.y = 0;
        model.CurrentPushVelocity = impactDir * force;
    }

    public void ApplyHitImpactForce()
    {
        if(model.CurrentPushVelocity.magnitude > 0)
        {
            model.MovementVelocity = model.CurrentPushVelocity * Time.deltaTime;
            view.CharacterController.Move(model.MovementVelocity);
        }
        model.CurrentPushVelocity = Vector3.Lerp(model.CurrentPushVelocity, Vector3.zero, Time.deltaTime * 5);
    }

    public PlayerSO GetPlayerSO()
    {
        return model.PlayerSO;
    }

    //private void InAir()
    //{
    //    view.Animator.SetBool("AirBorne", !view.CharacterController.isGrounded);
    //    //view.Animator.SetFloat("Speed", model.MovementVelocity.magnitude);
    //}
}
