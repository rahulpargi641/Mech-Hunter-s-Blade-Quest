using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : EnemyState
{
    private float currentSpawnTime;

    public EnemySpawning(EnemyController controller) : base(controller)
    {
        state = EEnemyState.Spawning;

        currentSpawnTime = controller.SpawningDuration;
    }

    protected override void Enter()
    {
        base.Enter();

        controller.CharacterMaterial?.StartCharacterAppearingEffect();
    }

    protected override void Update()
    {
        base.Update();

        SwitchStateToIdleIf();
    }

    protected override void Exit()
    {
        base.Exit();
    }

    private void SwitchStateToIdleIf()
    {
        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime <= 0)
        {
            nextState = new EnemyIdle(controller);
            stage = EStage.Exit;
        }
    }
}
