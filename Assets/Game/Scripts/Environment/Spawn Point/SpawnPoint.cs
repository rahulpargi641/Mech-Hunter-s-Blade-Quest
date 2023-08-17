using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] EnemyView enemyPrefab;
    public EnemyView EnemyPrefab => enemyPrefab;
}
