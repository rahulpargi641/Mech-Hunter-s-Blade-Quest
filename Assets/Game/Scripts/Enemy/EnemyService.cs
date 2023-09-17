using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    private List<EnemyController> enemyControllers;

    private bool areEnemiesSpawned = false;
    private int TotalEnemies = 15;
    private int deadEnemies;

    protected override void Awake()
    {
        base.Awake();

        enemyControllers = new List<EnemyController>();
    }

    private void Start()
    {
        EventService.Instance.onEnemyDeathAction += EnemyDead;        
    }

    private void Update()
    {
        if (enemyControllers.Count == 0)
            return;

        if(CurrentEnemyGroupDead())
        {
            EventService.Instance.InvokeCurrentEnemyGroupDeadAction();
            enemyControllers.Clear();
            Debug.Log("Current Enemy Group dead");
        }

        AllEnemiesDead();
    }

    private void AllEnemiesDead()
    {
        if(deadEnemies == TotalEnemies)
            EventService.Instance.InvokeAllEnemiesDeadAction();
    }

    private bool CurrentEnemyGroupDead()
    {
        bool currentEnemyGroupDead = true;
        foreach (EnemyController enemyController in enemyControllers)
        {
            if (!enemyController.IsDead())
            {
                currentEnemyGroupDead = false;
            }
        }

        return currentEnemyGroupDead;
    }

    public void AddEnemyController(EnemyModel model, EnemyView view)
    {
        enemyControllers.Add(new EnemyController(model, view));
    }

    public void EnemyDead(EnemyView enemyView)
    {
        enemyView.EnemyDead();
        deadEnemies++;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);
    //}
}
