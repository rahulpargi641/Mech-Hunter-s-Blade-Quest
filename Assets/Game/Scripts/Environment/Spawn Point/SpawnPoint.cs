using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] EnemyView enemyPrefab;
    public EnemyView EnemyPrefab => enemyPrefab;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position + new Vector3(0f, 0.5f, 0f);
        Gizmos.DrawWireCube(center, Vector3.one);
        Gizmos.DrawLine(center, center + transform.forward * 2);
    }
}
