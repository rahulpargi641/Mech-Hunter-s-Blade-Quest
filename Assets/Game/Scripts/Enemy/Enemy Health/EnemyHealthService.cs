using UnityEngine;

public class EnemyHealthService : MonoSingletonGeneric<EnemyHealthService>
{
    private EnemyHealthView enemyHealthView;
    private EnemyHealthController enemyHealthController;

    private void Start()
    {
        EventService.Instance.onEnemySpawned += CreateHealthComponent;
    }

    private void OnDisable()
    {
        EventService.Instance.onEnemySpawned -= CreateHealthComponent;
    }

    private void CreateHealthComponent()
    {
        enemyHealthView = FindAnyObjectByType<EnemyHealthView>();
        HealthModel healthModel = new HealthModel();
        enemyHealthController = new EnemyHealthController(healthModel, enemyHealthView);
    }

    internal void EnemyDead(EnemyView enemyView)
    {
        EventService.Instance.InvokeEnemyDeathAction(enemyView);
    }
}
