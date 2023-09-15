using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    //[SerializeField] BoxCollider boxCollider;
    private List<EnemySpawner> spawnPointList;
    private List<EnemyController> enemyControllers;

    private bool areEnemiesSpawned = false;
    private int TotalEnemies = 9;
    private int deadEnemies;

    protected override void Awake()
    {
        base.Awake();

        //var spawnPointArray = transform.parent.GetComponentsInChildren<SpawnPoint>();
        //spawnPointList = new List<SpawnPoint>(spawnPointArray);

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<PlayerView>())
    //    {
    //        SpawnEnemies();
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);
    //}

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



    //public void SpawnEnemies()
    //{
    //    if (areEnemiesSpawned) return;

    //    areEnemiesSpawned = true;

    //    foreach (SpawnPoint spawnPoint in spawnPointList)
    //    {
    //        if(spawnPoint.EnemyPrefab)
    //        {
    //            EnemyModel enemyModel = new();
    //            EnemyView enemyView = Instantiate(spawnPoint.EnemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    //            Debug.Log("Enemy Spawned." + enemyView.gameObject.name);
    //            enemyControllers.Add(new EnemyController(enemyModel, enemyView));
    //            EventService.Instance.InvokeEnemySpawnedAction();
    //        }
    //    }
    //}

    public void AddEnemyController(EnemyModel model, EnemyView view)
    {
        enemyControllers.Add(new EnemyController(model, view));
    }

    public void EnemyDead(EnemyView enemyView)
    {
        enemyView.EnemyDead();
        deadEnemies++;
    }
}
