using UnityEngine;

public class PlayerModel
{
    public float MoveSpeed { get; private set; }
    public float Gravity { get; private set; }
    public float VerticalVelocity { get; set; }

    public Vector3 MovementVelocity;

    // Player Sliding
    public float AttackStartTime;
    public float AttackSlideDuration = 0.5f;
    public float AttackSlideSpeed = 3f;

    public PlayerModel()
    {
        MoveSpeed = 5f;
        Gravity = -9.8f;
    }
}
