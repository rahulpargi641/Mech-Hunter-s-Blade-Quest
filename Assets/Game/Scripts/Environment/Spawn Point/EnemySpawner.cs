using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] EnemyView enemyPrefab;
    [SerializeField] BoxCollider boxCollider;
    //EnemyView EnemyPrefab => enemyPrefab;

    private bool areEnemiesSpawned = false;

    private List<EnemySpawner> spawnPointList;

    //private void Awake()
    //{
    //    var spawnPointArray = transform.parent.GetComponentsInChildren<EnemySpawner>();
    //    spawnPointList = new List<EnemySpawner>(spawnPointArray);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        if (areEnemiesSpawned) return;

        areEnemiesSpawned = true;

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (enemyPrefab)
            {
                EnemyModel enemyModel = new();
                EnemyView enemyView = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                Debug.Log("Enemy Spawned." + enemyView.gameObject.name);
                //enemyControllers.Add(new EnemyController(enemyModel, enemyView));
                EnemyService.Instance.AddEnemyController(enemyModel, enemyView);
                EventService.Instance.InvokeEnemySpawnedAction();
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);

        Gizmos.color = Color.green;
        Vector3 center = transform.position + new Vector3(0f, 0.5f, 0f);
        //Gizmos.DrawWireCube(center, Vector3.one);
        //Gizmos.DrawLine(center, center + transform.forward * 2);

        Gizmos.DrawWireCube(spawnPoints[0].position, Vector3.one);
        Gizmos.DrawLine(spawnPoints[0].position, spawnPoints[0].position + transform.forward * 2);
    }
}
