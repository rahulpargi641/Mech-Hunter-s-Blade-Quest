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
    }

    public void Run(float horizontalInput, float verticalInput)
    {
        XZPlaneMovementVelocity(horizontalInput, verticalInput);
        Turn();
        YAxisMovementVelocity();
        InAir();

        view.CharacterController.Move(model.MovementVelocity);
    }

    private void XZPlaneMovementVelocity(float horizontalInput, float verticalInput)
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


    private void YAxisMovementVelocity()
    {
        if (view.CharacterController.isGrounded == false)
            model.VerticalVelocity = model.Gravity;
        else
            model.VerticalVelocity = model.Gravity * 0.3f;

            model.MovementVelocity += model.VerticalVelocity * Vector3.up * Time.deltaTime;
    }

    private void InAir()
    {
        view.Animator.SetBool("AirBorne", !view.CharacterController.isGrounded);
        //view.Animator.SetFloat("Speed", model.MovementVelocity.magnitude);
    }

    internal void AttackSlide()
    {
        model.MovementVelocity = Vector3.zero; // might have value from last frame


        if (Time.time < model.AttackStartTime + model.AttackSlideDuration)
        {
            float timePassed = Time.time - model.AttackStartTime;
            float lerpTime = timePassed / model.AttackSlideDuration;
            model.MovementVelocity = Vector3.Lerp(view.transform.forward * model.AttackSlideSpeed, Vector3.zero, lerpTime);
        }
    }
}
