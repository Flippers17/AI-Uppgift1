using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void Enter(EnemyBehaviour behaviour);

    public abstract void UpdateState(EnemyBehaviour behaviour, float deltaTime);

    public abstract void Exit(EnemyBehaviour behaviour);
}
