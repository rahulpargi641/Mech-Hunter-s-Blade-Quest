using UnityEngine;

public class PlayerModel
{
    public float MoveSpeed { get; private set; }
    public float Gravity { get; private set; }
    public float VerticalVelocity { get; set; }

    public Vector3 MovementVelocity;

    public PlayerModel()
    {
        MoveSpeed = 5f;
        Gravity = -9.8f;
    }
}
