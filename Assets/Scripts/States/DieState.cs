using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "StatesSO/Die")]

public class DieState : StatesSO
{
    public override void OnStateEnter(AEnemyBehaviour ec)
    {
        //espera medio segundo y se destruye
        //congela el gameobject
        Destroy(ec.gameObject, 0.5f);
        Debug.Log("Enemy is dead");
    }

    public override void OnStateExit(AEnemyBehaviour ec)
    {
    }

    public override void OnStateUpdate(AEnemyBehaviour ec)
    {
    }
}
