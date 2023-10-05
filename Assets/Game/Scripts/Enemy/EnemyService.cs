using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    private List<EnemyController> enemyControllers;

    private int totalEnemies = 7;
    private int nDeadEnemies;

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

        if(IsCurrentEnemyGroupDead())
        {
            EventService.Instance.InvokeCurrentEnemyGroupDeadAction();
            enemyControllers.Clear();
            Debug.Log("Current Enemy Group dead");
        }

        AllEnemiesDead();
    }

    private void AllEnemiesDead()
    {
        if(nDeadEnemies == totalEnemies)
            EventService.Instance.InvokeAllEnemiesDeadAction();
    }

    private bool IsCurrentEnemyGroupDead()
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
        nDeadEnemies++;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);
    //}
}
