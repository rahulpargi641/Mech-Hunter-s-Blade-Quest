using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] EnemyView enemyView;
    private EnemyPool enemyPool;
    private List<EnemyController> enemyControllers;

    private int nEnemies = 7;
    private int enemyNumber;

    protected override void Awake()
    {
        base.Awake();

        enemyPool = new EnemyPool();
        enemyControllers = new List<EnemyController>();
    }

    private void Start()
    {
        EventService.Instance.onEnemyDeathAction += DecreaseEnemyCount;        
    }

    private void OnDestroy()
    {
        EventService.Instance.onEnemyDeathAction -= DecreaseEnemyCount;
    }

    private void Update()
    {
        if (enemyControllers.Count == 0)
            return;
        else
            CheckIfCurrentEnemyGroupDead();

        CheckIfAllEnemiesDead();
    }

    private void CheckIfCurrentEnemyGroupDead()
    {
        if (IsCurrentEnemyGroupDead())
        {
            EventService.Instance.InvokeOnCurrentEnemyGroupDead();
            enemyControllers.Clear();
            Debug.Log("Current Enemy Group dead");
        }
    }

    private void CheckIfAllEnemiesDead()
    {
        if(nEnemies <= 0)
            EventService.Instance.InvokeOnAllEnemiesDead();
    }

    private bool IsCurrentEnemyGroupDead()
    {
        bool currentEnemyGroupDead = true;
        foreach (EnemyController enemyController in enemyControllers)
        {
            if (!enemyController.IsDead())
                currentEnemyGroupDead = false;
        }

        return currentEnemyGroupDead;
    }

    // Called via Enemy Spawner
    public void SpawnEnemy(EnemyView enemyView, Vector3 spawnPoint)
    {
        EnemyModel enemyModel = new EnemyModel(enemySO);
        EnemyController enemyController = enemyPool.GetEnemyContoller(enemyModel, enemyView);

        enemyNumber++;
        enemyController.EnableEnemy(enemyNumber);
        enemyController.SetTransform(spawnPoint);

        enemyControllers.Add(enemyController);
    }

    // called via event onEnemyDeathAction
    public void DecreaseEnemyCount(EnemyView enemyView)
    {
        nEnemies--;
    }

    public void EnemyDissolved(EnemyView enemyView)
    {
        enemyView.Controller.EnemyDead();
        enemyView.Controller.DisableEnemy();
        //ReturnEnemyToPool(enemyView.Controller);
    }

    public void ReturnEnemyToPool(EnemyController enemyController)
    {
        enemyPool.ReturnItem(enemyController);
    }
}
