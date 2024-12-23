using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatesSO : ScriptableObject
{
    public List<StatesSO> StatesToGo;
    public abstract void OnStateEnter(AEnemyBehaviour ec);
    public abstract void OnStateUpdate(AEnemyBehaviour ec);
    public abstract void OnStateExit(AEnemyBehaviour ec);

}