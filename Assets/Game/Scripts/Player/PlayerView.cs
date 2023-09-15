using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; set; }

    private DamageCasterView damageCaster; // service

    private PlayerState currentState;

    public bool RollAnimationEnded { get; set; }
    public bool AttackAnimationEnded { get; set; }
    public bool BeingHitAnimationEnded { get; set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool MouseButton1Down { get; set; }
    public bool MouseButton2Down { get; set; }
    public bool SpaceKeyDown { get; set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        damageCaster = GetComponentInChildren<DamageCasterView>();
    }

    private void Start()
    {
        currentState = new Idle(this, Animator);
    }

    private void Update()
    {
        ReadPlayerInput();
        currentState = currentState.Process();
    }

    private void FixedUpdate()
    {
        // currentState = currentState.Process();
    }

    private void OnDisable()
    {
        //MouseButtonDown = false;
        HorizontalInput = 0;
        VerticalInput = 0;
    }

    private void ReadPlayerInput()
    {
        if (Time.timeScale != 0) MouseButton1Down = Input.GetMouseButton(0);

        if (Time.timeScale != 0) MouseButton2Down = Input.GetMouseButton(1);

        SpaceKeyDown = Input.GetKeyDown(KeyCode.Space);

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    public void Run()
    {
        Controller.Run(HorizontalInput, VerticalInput);
    }

    // called via animation event
    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
        //MouseButtonDown = false;
    }

    public void RollAnimationEnd()
    {
        RollAnimationEnded = true; 
    }

    // called via animation event
    public void BeingHitAnimationEnd()
    {
        BeingHitAnimationEnded = true;
    }

    // called via animation event
    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }

    // called via animation event
    public void DisableDamageCaster()
    {
        damageCaster.DisableDamageCaster();
    }
}
