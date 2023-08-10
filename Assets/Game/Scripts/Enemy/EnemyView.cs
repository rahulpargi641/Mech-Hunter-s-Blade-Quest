using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EnemyController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    private EnemyState currentState;

    public bool AttackAnimationEnded { get; set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerView>().transform;
        currentState = new EnemyIdle(this, navMeshAgent, animator, playerTransform);
        //navMeshAgent.speed = MoveSpeed = 5;
    }

    private void FixedUpdate()
    {
        currentState = currentState.Process();
    }

    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
        Debug.Log("AttackANimationEnded set to true");
    }
}
