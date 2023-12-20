using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    [SerializeField] EnemySO enemySO;
    private List<EnemyController> currentEnemyGroup = new List<EnemyController>();

    private EnemyPool enemyPool = new EnemyPool();
    private int nEnemies = 7; // No of enemies in the level

    private void Start()
    {
        EventService.Instance.onEnemyDeath += DecreaseEnemyCount;
        EventService.Instance.onEnemyDissolved += ReturnEnemyToPool;
    }

    private void OnDestroy()
    {
        EventService.Instance.onEnemyDeath -= DecreaseEnemyCount;
        EventService.Instance.onEnemyDissolved -= ReturnEnemyToPool;
    }

    private void Update()
    {
        if (currentEnemyGroup.Count == 0)
            return;
        else
            ProcessIfCurrentEnemyGroupDead();

        ProcessIfAllEnemiesDead();
    }

    private void ProcessIfCurrentEnemyGroupDead()
    {
        if (IsCurrentEnemyGroupDead())
        {
            EventService.Instance.InvokeOnCurrentEnemyGroupDead(); // Opens the gate which gets closed when player enters the area
            currentEnemyGroup.Clear();
            Debug.Log("Current Enemy Group dead"); // remove it
        }
    }

    private bool IsCurrentEnemyGroupDead()
    {
        bool currentEnemyGroupDead = true;
        foreach (EnemyController enemyController in currentEnemyGroup)
        {
            if (!enemyController.IsDead)
                currentEnemyGroupDead = false;
        }

        return currentEnemyGroupDead;
    }

    private void ProcessIfAllEnemiesDead()
    {
        if(nEnemies <= 0)
            EventService.Instance.InvokeOnAllEnemiesDead(); // Activates the GameComplete UI
    }
    
    // Called via Enemy Spawner
    public void SpawnEnemy(EnemyView enemyView, Vector3 spawnPoint)
    {
        EnemyModel enemyModel = new EnemyModel(enemySO);
        EnemyController enemyController = enemyPool.GetEnemyController(enemyModel, enemyView);

        enemyController.EnableEnemy();
        enemyController.SetTransform(spawnPoint);

        currentEnemyGroup.Add(enemyController);
    }

    // called via event onEnemyDeathAction
    public void DecreaseEnemyCount(EnemyView enemyView)
    {
        nEnemies--;
    }

    public void ReturnEnemyToPool(EnemyView dissolvedEnemy)
    {
        dissolvedEnemy.DisableEnemy();
        //enemyPool.ReturnItem(dissolvedEnemy.Controller);
    }
}
