using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    #region View Related
    public NavMeshAgent NavMeshAgent => view.NavMeshAgent;
    public Animator Animator => view.Animator;
    public DamageCasterView DamageCaster => view.DamageCaster;
    public CharacterMaterial CharacterMaterial => view.CharacterMaterial;
    public Transform PlayerTransform => view.PlayerTransform;
    public Transform EnemyTransform => view.transform;
    public List<Transform> PatrolPoints => view.PatrolPoints;

    public bool AttackAnimationEnded { get { return view.AttackAnimationEnded; } set { view.AttackAnimationEnded = value; } }
    public bool HurtAnimationEnded { get { return view.HurtAnimationEnded; } set { view.HurtAnimationEnded = value; } }
    #endregion

    #region Model Related
    //public EnemyType EnemyType => model.EnemyType;
    public EnemyType EnemyType => view.EnemyType;
    public float SpawningDuration => model.SpawningDuration;

    public bool IsHit { get { return model.IsHit; } set { model.IsHit = value; } }
    public bool IsDead => model.IsDead;
    public bool IsPlayerDead { get; private set; } // For stopping enemy from attacking the player if player is dead

    public float VisibleDist => model.VisibleDist; // distance at which enemy can see the player
    public float VisibleAngle => model.VisibleAngle; // beyond this angle enemy can't see the player
    public float AttackDist => model.AttackDist;
    public float VisibleAttackAngle => model.VisibleAttackAngle;
    public float ShootingDist => model.ShootingDist;
    public float DetectDist => model.DetectDist; // min distance at which enemy senses the player regardless of Visible angle
    public float PathUpdateDelay => model.PathUpdateDelay; // delay for updating path enemy path, for avoiding calculating path every frame to improve the performance 
    public int PatrolChance => model.PatrolChance; // probability of enemy going into the patrol state from idle state.

    // Enemy Attack
    public string IdleAnimName => model.IdleAnimName;
    public string RunAnimName => model.RunAnimName;
    public string HurtAnimName => model.HurtAnimName;
    public string AttackAnimName => model.AttackAnimName;
    public string DeadAnimName => model.DeadAnimName;
    #endregion
    public bool CanEnterDeadState { get; set; } = true;

    readonly EnemyModel model;
    readonly EnemyView view;

    private EnemyState currentState;

    public EnemyController(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;

        EventService.Instance.onEnemyHit += ProcessEnemyHit;
        EventService.Instance.onPlayerDeath += PlayerDead;

        currentState = new EnemySpawning(this);
    }

    public void ProcessCurrentState()
    {
         currentState = currentState.ProcessState();
    }

    public void SetTransform(Vector3 spawnPoint)
    {
        view.transform.position = spawnPoint;
    }

    public void EnableEnemy()
    {
        view.EnableEnemy();
    }

    public void DisableEnemy()
    {
        view.DisableEnemy();
    }

    public void ProcessEnemyHit(EnemyView hitEnemy, int damage)
    {
        if(hitEnemy == view)
        {
            model.IsHit = true;
            ApplyDamage(damage);
        }
    }

    private void ApplyDamage(int damage)
    {
        if (model.CurrentHealth > 0)
        {
            model.CurrentHealth -= damage;

            if (model.CurrentHealth <= 0)
            {
                model.IsDead = true;
                EventService.Instance.InvokeOnEnemyDeath(view);
            }
        }
    }

    public void PlayerDead()
    {
        IsPlayerDead = true;
    }

    public void OnDestroy() // called in view
    {
        EventService.Instance.onEnemyHit -= ProcessEnemyHit;
        EventService.Instance.onPlayerDeath -= PlayerDead;
    }
}
