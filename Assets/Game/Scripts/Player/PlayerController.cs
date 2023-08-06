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

    public void PlayerMovement(float horizontalInput, float verticalInput)
    {
        XZPlaneMovement(horizontalInput, verticalInput);
        YAxisMovement();
        Turn();

        view.CharacterController.Move(model.MovementVelocity);
    }

    private void XZPlaneMovement(float horizontalInput, float verticalInput)
    {
        model.MovementVelocity.Set(horizontalInput, 0f, verticalInput);
        model.MovementVelocity.Normalize();
        model.MovementVelocity = Quaternion.Euler(0, -45f, 0) * model.MovementVelocity;
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
            model.VerticalVelocity = model.Gravity;
        else
            model.VerticalVelocity = model.Gravity * 0.3f;

        model.MovementVelocity += model.VerticalVelocity * Vector3.up * Time.deltaTime;
    }
}
