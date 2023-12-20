using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMaterial))]
[RequireComponent(typeof(EnemyVFX))]
public class EnemyView : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private EnemyType enemyType; // set it in Scriptable Object
    public EnemyType EnemyType => enemyType;
    public Transform PlayerTransform => playerTransform;

    public List<Transform> PatrolPoints; // make serializefield
    public bool AttackAnimationEnded { get; set; }
    public bool HurtAnimationEnded { get; set; }
    public CharacterController CharacterController { get; set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public DamageCasterView DamageCaster { get; private set; }
    public CharacterMaterial CharacterMaterial { get; private set; }
    public EnemyController Controller { get; set; }

    virtual protected void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        DamageCaster = GetComponentInChildren<DamageCasterView>();
        CharacterMaterial = GetComponent<CharacterMaterial>();
    }

    virtual protected void FixedUpdate()
    {
        Controller?.ProcessCurrentState();
    }

    // called via animation event
    public void AttackAnimationEnd()
    {
        AttackAnimationEnded = true;
    }

    // called via animation event
    public void HurtAnimationEnd()
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

    public void EnableEnemy()
    {
        gameObject.SetActive(true);
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    //private void SetPatrolPoints()
    //{
    //    Transform parentTransform;
    //    if (EnvironmentService.Instance.enemyPatrolPoints.TryGetValue(EnemyID, out parentTransform))
    //    {
    //        for (int i = 0; i < parentTransform.childCount; i++)
    //            PatrolPoints2.Add(parentTransform.GetChild(i));
    //    }
    //}
}
