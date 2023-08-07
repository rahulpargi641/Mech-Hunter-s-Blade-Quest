using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EnemyController Controller { private get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; set; }
    public NavMeshAgent NavMeshAgent { get; set; }
    public Transform playerTransform { get; set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerView>().transform;
        //navMeshAgent.speed = MoveSpeed = 5;
    }
   
    private void FixedUpdate()
    {
        ProcessEnemyMovement();
    }

    private void ProcessEnemyMovement()
    {
        Controller.EnemyMovement();
    }
}
