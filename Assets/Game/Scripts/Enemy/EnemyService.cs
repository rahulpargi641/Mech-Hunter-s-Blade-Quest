using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] EnemyView enemyPrefab;
    private EnemyController enemyController;

    void Awake()
    {
        EnemyModel enemyModel = new();
        //PlayerView playerView = Instantiate(playerPrefab);
        enemyController = new EnemyController(enemyModel, enemyPrefab);
    }
}
