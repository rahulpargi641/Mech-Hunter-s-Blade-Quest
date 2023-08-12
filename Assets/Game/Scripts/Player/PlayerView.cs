using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; set; }

    private DamageCasterView damageCaster;

    private State currentState;
    public bool AttackAnimationEnded { get; set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool MouseButtonDown { get; private set; }

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
    }

    private void FixedUpdate()
    {
        currentState = currentState.Process();
    }

    private void ReadPlayerInput()
    {
        if (!MouseButtonDown && Time.timeScale != 0)
            MouseButtonDown = Input.GetMouseButton(0);

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    public void Run()
    {
        Controller.Run(HorizontalInput, VerticalInput);
    }

    internal void AttackSlide()
    {
        Controller.AttackSlide();
    }

    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
        MouseButtonDown = false;
    }

    public void EnableDamageCaster()
    {
        damageCaster.EnableDamageCaster();
    }

    public void DisableDamageCaster()
    {
        damageCaster.DisableDamageCaster();
    }

    private void OnDisable()
    {
        MouseButtonDown = false;
        HorizontalInput = 0;
        VerticalInput = 0;
    }
}
