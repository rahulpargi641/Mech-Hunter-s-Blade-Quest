using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    //[SerializeField] BoxCollider boxCollider;
    private List<EnemySpawner> spawnPointList;
    private List<EnemyController> enemyControllers;

    private bool areEnemiesSpawned = false;

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

        if(AllEnemiesDead())
        {
            EventService.Instance.InvokeAllEnemiesDeadAction();
            enemyControllers.Clear();
            Debug.Log("All Enemies dead");
        }
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

    private bool AllEnemiesDead()
    {
        bool allEnemiesDead = true;
        foreach (EnemyController enemyController in enemyControllers)
        {
            if (!enemyController.IsDead())
            {
                allEnemiesDead = false;
            }
        }
        return allEnemiesDead;
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
    }
}
