using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    private EnemyPool enemyPool;
    private List<EnemyController> enemyControllers;

    private int totalEnemies = 7;
    private int nDeadEnemies;

    protected override void Awake()
    {
        base.Awake();

        enemyPool = GetComponent<EnemyPool>();
        enemyControllers = new List<EnemyController>();
    }

    private void Start()
    {
        EventService.Instance.onEnemyDeathAction += EnemyDead;        
    }

    private void OnDestroy()
    {
        EventService.Instance.onEnemyDeathAction -= EnemyDead;
    }

    private void Update()
    {
        if (enemyControllers.Count == 0)
            return;

        CheckIfCurrentEnemyGroupDead();

        CheckIfAllEnemiesDead();
    }

    private void CheckIfCurrentEnemyGroupDead()
    {
        if (IsCurrentEnemyGroupDead())
        {
            EventService.Instance.InvokeCurrentEnemyGroupDeadAction();
            enemyControllers.Clear();
            Debug.Log("Current Enemy Group dead");
        }
    }

    private void CheckIfAllEnemiesDead()
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

    // Called via Enemy Spawner
    public void CreateEnemy(EnemyView enemyView)
    {
        EnemyModel enemyModel = new();
        //EnemyController enemyController = new EnemyController(enemyModel, enemyView);
        EnemyController enemyController = enemyPool.GetEnemyContoller(enemyModel, enemyView);
        enemyControllers.Add(enemyController);
    }

    // called via event onEnemyDeathAction
    public void EnemyDead(EnemyView enemyView)
    {
        enemyView.Controller.EnemyDead();
        nDeadEnemies++;

        enemyView.gameObject.SetActive(false);
        ReturnEnemyToPool(enemyView.Controller);

    }

    void ReturnEnemyToPool(EnemyController enemyController)
    {
        enemyPool.ReturnItem(enemyController);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);
    //}
}
