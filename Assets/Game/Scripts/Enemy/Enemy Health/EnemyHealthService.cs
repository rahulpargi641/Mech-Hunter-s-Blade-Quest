using UnityEngine;

public class EnemyHealthService : MonoSingletonGeneric<EnemyHealthService>
{
    [SerializeField] EnemyHealthView enemyHealthView;
    private EnemyHealthController enemyHealthController;

    void Start()
    {
        HealthModel healthModel = new HealthModel();
        enemyHealthController = new EnemyHealthController(healthModel, enemyHealthView);
    }

    internal void EnemyDead()
    {
        EventService.Instance.InvokeEnemyDeathAction();
    }
}
