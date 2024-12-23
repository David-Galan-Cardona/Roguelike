using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockbackState", menuName = "StatesSO/KnockBack")]

public class KnockbackState : StatesSO
{
    public override void OnStateEnter(AEnemyBehaviour ec)
    {
        ec.StartCoroutine(Knockback(ec));
    }

    private IEnumerator Knockback(AEnemyBehaviour ec)
    {
        yield return new WaitForSeconds(0.5f);
        ec.rb.velocity = Vector2.zero;
        ec.GoToState<ChaseState>();
    }

    public override void OnStateExit(AEnemyBehaviour ec)
    {
    }

    public override void OnStateUpdate(AEnemyBehaviour ec)
    {
    }

}