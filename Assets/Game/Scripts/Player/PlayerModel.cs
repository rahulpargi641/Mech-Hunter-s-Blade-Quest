using UnityEngine;

public class PlayerModel
{
    public float MoveSpeed { get; private set; }
    public float Gravity { get; private set; }
    public float VerticalVelocity { get; set; }

    public Vector3 MovementVelocity;
    public Vector3 ImpactOnCharacter { get; set; }
    public float ImpactMagnitude { get; private set; }


    public PlayerModel()
    {
        MoveSpeed = 5f;
        Gravity = -20f;
        MovementVelocity = new Vector3();
        ImpactOnCharacter = new Vector3();
        ImpactMagnitude = 0.2f;
    }
}
