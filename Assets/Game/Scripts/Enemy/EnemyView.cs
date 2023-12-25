using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMaterial))]
public class EnemyView : MonoBehaviour
{
    public EnemyType EnemyType => enemyType;
    public Transform PlayerTransform => playerTransform;
    public DamageCasterView DamageCaster => damageCasterView;

    public List<Transform> PatrolPoints; // make serializefield
    public bool AttackAnimationEnded { get; set; }
    public bool HurtAnimationEnded { get; set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public CharacterMaterial CharacterMaterial { get; private set; }
    public EnemyController Controller { get; set; }

    [SerializeField] private Transform playerTransform;
    [SerializeField] private EnemyType enemyType; // set it in Scriptable Object
    [SerializeField] private DamageCasterView damageCasterView;

    virtual protected void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        CharacterMaterial = GetComponent<CharacterMaterial>();
        //DamageCaster = GetComponentInChildren<DamageCasterView>();
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
}
