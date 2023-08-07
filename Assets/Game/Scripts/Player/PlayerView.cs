using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; set; }

    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        ProcessPlayerMovement();
    }
    private void ProcessPlayerMovement()
    {
        //if (horizontalInput != 0 || verticalInput != 0)
            Controller.PlayerMovement(horizontalInput, verticalInput);
    }
    private void OnDisable()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }
}
