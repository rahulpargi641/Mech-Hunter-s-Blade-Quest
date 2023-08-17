using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    private List<SpawnPoint> spawnPointList;
    private List<EnemyController> enemyController;

    void Awake()
    {
        var spawnPointArray = transform.parent.GetComponentsInChildren<SpawnPoint>();
        spawnPointList = new List<SpawnPoint>(spawnPointArray);

        //SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        foreach (SpawnPoint spawnPoint in spawnPointList)
        {
            if(spawnPoint.EnemyPrefab)
            {
                EnemyModel enemyModel = new();
                EnemyView enemyView = Instantiate(spawnPoint.EnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                Debug.Log("Enemy Spawned." + enemyView.gameObject.name);
                enemyController.Add(new EnemyController(enemyModel, enemyView));
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
        {
            SpawnEnemies();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);
    }
}
