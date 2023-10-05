using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] EnemyView[] enemyViews;
    [SerializeField] BoxCollider boxCollider;

    private bool areEnemiesSpawned = false;

    //private void Awake()
    //{
    //    //var spawnPointArray = transform.parent.GetComponentsInChildren<EnemySpawner>();
    //    //spawnPointList = new List<EnemySpawner>(spawnPointArray);
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

        foreach (EnemyView enemyView in enemyViews)
        {
            if (enemyView)
            {
                enemyView.gameObject.SetActive(true);
                EnemyService.Instance.CreateEnemy(enemyView);
                Debug.Log("Enemy Spawned." + enemyView.gameObject.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCollider.transform.position, boxCollider.bounds.size);

        Gizmos.color = Color.green;
        Vector3 center = transform.position + new Vector3(0f, 0.5f, 0f);

        foreach(Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawWireCube(spawnPoint.position, Vector3.one);
            Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + transform.forward * 2);
        }

        //Gizmos.DrawWireCube(center, Vector3.one);
        //Gizmos.DrawLine(center, center + transform.forward * 2);
    }
}
