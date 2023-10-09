using UnityEngine;

public class PlayerModel
{
    public float MoveSpeed { get; private set; }
    public float FallGravity { get; private set; }
    public float VerticalSpeed { get; set; }

    public Vector3 MovementVelocity;
    public Vector3 CurrentPushVelocity { get; set; }
    public PlayerController Controller { private get; set; }
    public PlayerSO PlayerSO { get; private set; }

    public PlayerModel(PlayerSO playerSO)
    {
        PlayerSO = playerSO;
        MoveSpeed = playerSO.moveSpeed;
        FallGravity = playerSO.fallGravity;
        MovementVelocity = new Vector3();
        CurrentPushVelocity = new Vector3();
        //MinPushForce = 0.2f;
    }
}
