using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    public CharacterController CharacterController { get; private set; }
    public Animator Animator { get; private set; }
    public DamageCasterView DamageCaster { get; private set; }

    public bool RollAnimationEnded { get; set; }
    public bool AttackAnimationEnded { get; set; }
    public bool HurtAnimationEnded { get; set; }

    public bool LeftMouseButtonDown { get; private set; }
    public bool RightMouseButtonDown { get; private set; }
    public bool SpaceButtonDown { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public PlayerController Controller { private get; set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        DamageCaster = GetComponentInChildren<DamageCasterView>();
    }

    private void Update()
    {
        ReadPlayerInput();

        Controller?.ProcessCurrentState();
    }

    //private void FixedUpdate()
    //{
    //    //currentState = currentState.Process();
    //}

    private void ReadPlayerInput()
    {
        if (Time.timeScale != 0) LeftMouseButtonDown = Input.GetMouseButton(0);
        if (Time.timeScale != 0) RightMouseButtonDown = Input.GetMouseButton(1);

        SpaceButtonDown = Input.GetKeyDown(KeyCode.Space);

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    // called via animation event
    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
    }

    public void RollAnimationEnd()
    {
        RollAnimationEnded = true; 
    }

    // called via animation event
    public void BeingHitAnimationEnd()
    {
        HurtAnimationEnded = true;
    }

    // called via animation event
    public void EnableDamageCaster()
    {
        DamageCaster.EnableDamageCaster();
    }

    // called via animation event
    public void DisableDamageCaster()
    {
        DamageCaster.DisableDamageCaster();
    }

    private void OnDestroy()
    {
        Controller.OnDestroy();
    }
}
